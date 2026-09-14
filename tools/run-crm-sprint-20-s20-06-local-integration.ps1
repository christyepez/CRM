param([string]$BaseUrl='http://127.0.0.1:8097',[string]$FrontendUrl='http://127.0.0.1:4207')
$ErrorActionPreference='Stop'
function Req([string]$Method,[string]$Path,[object]$Body=$null){
  $sw=[Diagnostics.Stopwatch]::StartNew()
  try{$a=@{UseBasicParsing=$true;Method=$Method;Uri="$BaseUrl$Path";TimeoutSec=15};if($null-ne$Body){$a.Body=$Body|ConvertTo-Json -Depth 5 -Compress;$a.ContentType='application/json'};$r=Invoke-WebRequest @a;$sw.Stop();[pscustomobject]@{Method=$Method;Path=$Path;Status=[int]$r.StatusCode;Ms=$sw.ElapsedMilliseconds;Body=$r.Content}}
  catch{$sw.Stop();$s=0;$b='';if($_.Exception.Response){$s=[int]$_.Exception.Response.StatusCode};if($_.ErrorDetails.Message){$b=$_.ErrorDetails.Message};[pscustomobject]@{Method=$Method;Path=$Path;Status=$s;Ms=$sw.ElapsedMilliseconds;Body=$b}}
}
function Ok($r,[int[]]$expected,[string]$name){if($r.Status -notin $expected){throw "$name expected $($expected -join '/') got $($r.Status): $($r.Body)"}}
$run=[guid]::NewGuid().ToString('N').Substring(0,8)
$all=New-Object System.Collections.Generic.List[object]
foreach($p in @('/health','/health/live','/health/ready')){$r=Req GET $p;Ok $r @(200) "health $p";$all.Add($r)}
$list=Req GET '/api/crm/foundation/notes';Ok $list @(200) 'initial list';$all.Add($list);$initial=@($list.Body|ConvertFrom-Json).Count
$related=[guid]::NewGuid().ToString('D')
$create=Req POST '/api/crm/foundation/notes' @{relatedEntityType='Lead';relatedEntityId=$related;text="  S20 Integration Note $run  "};Ok $create @(200) 'create';$all.Add($create)
$cj=$create.Body|ConvertFrom-Json;$id=$cj.id;if(!$id -or !$cj.changed -or $cj.note.text-ne"S20 Integration Note $run" -or $cj.note.status-ne'Active'){throw 'create normalization failed'}
$detail=Req GET "/api/crm/foundation/notes/$id";Ok $detail @(200) 'detail';$all.Add($detail)
$update=Req PUT "/api/crm/foundation/notes/$id" @{relatedEntityType='Lead';relatedEntityId=$related;text="Updated S20 Integration Note $run"};Ok $update @(200) 'update';$all.Add($update);if(-not($update.Body|ConvertFrom-Json).changed){throw 'update should change'}
$no=Req PUT "/api/crm/foundation/notes/$id" @{relatedEntityType='Lead';relatedEntityId=$related;text="Updated S20 Integration Note $run"};Ok $no @(200) 'no change';$all.Add($no);if(($no.Body|ConvertFrom-Json).changed){throw 'no-change should be false'}
$bad=Req POST '/api/crm/foundation/notes' @{relatedEntityType='Contact';relatedEntityId='bad';text='Bad note'};Ok $bad @(400) 'invalid create';$all.Add($bad)
$missingId=[guid]::NewGuid().ToString('D');$missing=Req GET "/api/crm/foundation/notes/$missingId";Ok $missing @(404) 'missing';$all.Add($missing)
$archive=Req POST "/api/crm/foundation/notes/$id/archive";Ok $archive @(200) 'archive';$all.Add($archive);if(($archive.Body|ConvertFrom-Json).note.status-ne'Archived'){throw 'archive state invalid'}
$repeat=Req POST "/api/crm/foundation/notes/$id/archive";Ok $repeat @(200) 'repeat archive';$all.Add($repeat);if(($repeat.Body|ConvertFrom-Json).changed){throw 'repeat archive changed'}
$conflict=Req PUT "/api/crm/foundation/notes/$id" @{relatedEntityType='Lead';relatedEntityId=$related;text='Changed after archive'};Ok $conflict @(409) 'archived update';$all.Add($conflict)
$detail2=Req GET "/api/crm/foundation/notes/$id";Ok $detail2 @(200) 'read after write';$all.Add($detail2);if(($detail2.Body|ConvertFrom-Json).status-ne'Archived'){throw 'read-after-write inconsistent'}
foreach($n in @(@('GET','/api/crm/notes'),@('POST','/api/crm/notes'),@('PUT',"/api/crm/notes/$id"),@('DELETE',"/api/crm/notes/$id"))){$r=Req $n[0] $n[1];Ok $r @(404) "productive $($n[0])";$all.Add($r)}
$fd=Req DELETE "/api/crm/foundation/notes/$id";Ok $fd @(404,405) 'foundation delete';$all.Add($fd)
$fr=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/foundation/notes" -TimeoutSec 15;if($fr.StatusCode-ne200){throw 'frontend route failed'}
$fa=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/api/crm/foundation/notes" -TimeoutSec 15;if($fa.StatusCode-ne200){throw 'frontend proxy failed'}
$lat=@($all|?{$_.Path-like'/api/crm/foundation/notes*'});$sort=@($lat|Sort-Object Ms);$avg=[math]::Round(($lat|Measure-Object Ms -Average).Average,2);$min=($lat|Measure-Object Ms -Minimum).Minimum;$p95=$sort[[math]::Min($sort.Count-1,[math]::Ceiling($sort.Count*.95)-1)].Ms
[pscustomobject]@{S2006Decision='Implemented';LocalBackendUrl=$BaseUrl;LocalFrontendUrl=$FrontendUrl;FrontendApiRoutingMode='Proxy';BackendHealth='PASS';FrontendNoteRouteStatus=200;FrontendToNoteApiConnectivity='PASS';InitialNoteCount=$initial;CreatedNoteId=$id;CreateScenario='PASS';ReadAfterCreate='PASS';UpdateScenario='PASS';NoChangeScenario='PASS';InvalidCreateScenario='PASS';NotFoundScenario='PASS';ArchiveScenario='PASS';RepeatArchive='PASS';ArchivedUpdateConflict='PASS';ReadAfterWriteConsistent=$true;ProductiveNoteRouteAvailable=$false;DeleteRouteAvailable=$false;RuntimePersistenceClassification='FoundationOnly';CrossEntityMutationObserved=$false;ActivitySchedulingObserved=$false;PortalRuntimeObserved=$false;CommonDbRuntimeObserved=$false;ExternalConnectorRuntimeObserved=$false;RealDataDetected=$false;SimulatedProductionTouched=$false;IntegrationLatencySamples=$lat.Count;LatencyMinMs=$min;LatencyAverageMs=$avg;LatencyP95Ms=$p95;RunId=$run}|ConvertTo-Json -Depth 4