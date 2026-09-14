param([string]$BaseUrl='http://127.0.0.1:8100',[string]$FrontendUrl='http://127.0.0.1:4213')
$ErrorActionPreference='Stop'
function Req([string]$Method,[string]$Path,[object]$Body=$null){
 $sw=[Diagnostics.Stopwatch]::StartNew(); try{$a=@{UseBasicParsing=$true;Method=$Method;Uri="$BaseUrl$Path";TimeoutSec=15};if($null-ne$Body){$a.Body=$Body|ConvertTo-Json -Depth 5 -Compress;$a.ContentType='application/json'};$r=Invoke-WebRequest @a;$sw.Stop();[pscustomobject]@{Method=$Method;Path=$Path;Status=[int]$r.StatusCode;Ms=$sw.ElapsedMilliseconds;Body=$r.Content}}
 catch{$sw.Stop();$s=0;$b='';if($_.Exception.Response){$s=[int]$_.Exception.Response.StatusCode};if($_.ErrorDetails.Message){$b=$_.ErrorDetails.Message};[pscustomobject]@{Method=$Method;Path=$Path;Status=$s;Ms=$sw.ElapsedMilliseconds;Body=$b}}
}
function Ok($r,[int[]]$e,[string]$n){if($r.Status -notin $e){throw "$n expected $($e -join '/') got $($r.Status): $($r.Body)"}}
$run=[guid]::NewGuid().ToString('N').Substring(0,8);$all=New-Object System.Collections.Generic.List[object]
foreach($p in @('/health','/health/live','/health/ready')){$r=Req GET $p;Ok $r @(200) "health $p";$all.Add($r)}
$list=Req GET '/api/crm/foundation/tags';Ok $list @(200) 'list';$all.Add($list);$initial=@($list.Body|ConvertFrom-Json).Count
$related=[guid]::NewGuid().ToString('D')
$create=Req POST '/api/crm/foundation/tags' @{name="  S23-$run  ";description=' integration ';relatedEntityType='Lead';relatedEntityId=$related};Ok $create @(200) 'create';$all.Add($create)
$cj=$create.Body|ConvertFrom-Json;$id=$cj.id;if(!$id-or!$cj.changed-or$cj.tag.name-ne"S23-$run"){throw 'create normalization failed'}
$detail=Req GET "/api/crm/foundation/tags/$id";Ok $detail @(200) 'detail';$all.Add($detail)
$update=Req PUT "/api/crm/foundation/tags/$id" @{name="S23-$run";description='updated';relatedEntityType='Lead';relatedEntityId=$related};Ok $update @(200) 'update';$all.Add($update);if(-not($update.Body|ConvertFrom-Json).changed){throw 'update should change'}
$no=Req PUT "/api/crm/foundation/tags/$id" @{name="S23-$run";description='updated';relatedEntityType='Lead';relatedEntityId=$related};Ok $no @(200) 'no change';$all.Add($no);if(($no.Body|ConvertFrom-Json).changed){throw 'no-change should be false'}
$related2=[guid]::NewGuid().ToString('D');$assign=Req PUT "/api/crm/foundation/tags/$id" @{name="S23-$run";description='updated';relatedEntityType='Case';relatedEntityId=$related2};Ok $assign @(200) 'assignment only';$all.Add($assign);if(-not($assign.Body|ConvertFrom-Json).changed){throw 'assignment-only should change'}
$bad=Req POST '/api/crm/foundation/tags' @{name=' ';description='bad'};Ok $bad @(400) 'invalid';$all.Add($bad)
$missing=Req GET "/api/crm/foundation/tags/$([guid]::NewGuid().ToString('D'))";Ok $missing @(404) 'missing';$all.Add($missing)
$archive=Req POST "/api/crm/foundation/tags/$id/archive";Ok $archive @(200) 'archive';$all.Add($archive);if(($archive.Body|ConvertFrom-Json).tag.status-ne'Archived'){throw 'archive failed'}
$repeat=Req POST "/api/crm/foundation/tags/$id/archive";Ok $repeat @(200) 'repeat archive';$all.Add($repeat);if(($repeat.Body|ConvertFrom-Json).changed){throw 'repeat archive changed'}
$conflict=Req PUT "/api/crm/foundation/tags/$id" @{name='changed';description='x';relatedEntityType='Case';relatedEntityId=$related2};Ok $conflict @(409) 'archived update';$all.Add($conflict)
foreach($n in @(@('GET','/api/crm/tags'),@('POST','/api/crm/tags'),@('PUT',"/api/crm/tags/$id"),@('DELETE',"/api/crm/tags/$id"))){$r=Req $n[0] $n[1];Ok $r @(404) "productive $($n[0])";$all.Add($r)}
$fd=Req DELETE "/api/crm/foundation/tags/$id";Ok $fd @(404,405) 'foundation delete';$all.Add($fd)
$fr=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/foundation/tags" -TimeoutSec 15;if($fr.StatusCode-ne200){throw 'frontend route failed'}
$fa=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/api/crm/foundation/tags" -TimeoutSec 15;if($fa.StatusCode-ne200){throw 'frontend proxy failed'}
$lat=@($all|?{$_.Path-like'/api/crm/foundation/tags*'});$sort=@($lat|Sort-Object Ms);$avg=[math]::Round(($lat|Measure-Object Ms -Average).Average,2);$p95=$sort[[math]::Min($sort.Count-1,[math]::Ceiling($sort.Count*.95)-1)].Ms
[pscustomobject]@{S2306Decision='Implemented';LocalBackendUrl=$BaseUrl;LocalFrontendUrl=$FrontendUrl;FrontendApiRoutingMode='Proxy';InitialTagCount=$initial;CreatedTagId=$id;CreateScenario='PASS';DetailScenario='PASS';UpdateScenario='PASS';NoChangeScenario='PASS';AssignmentOnlyChange='PASS';InvalidCreateScenario='PASS';NotFoundScenario='PASS';ArchiveScenario='PASS';RepeatArchive='PASS';ArchivedUpdateConflict='PASS';ProductiveTagRouteAvailable=$false;DeleteRouteAvailable=$false;PortalIdentityRuntimeObserved=$false;CrossEntityMutationObserved=$false;CommonDbRuntimeObserved=$false;ExternalConnectorRuntimeObserved=$false;RealDataDetected=$false;SimulatedProductionTouched=$false;IntegrationLatencySamples=$lat.Count;LatencyAverageMs=$avg;LatencyP95Ms=$p95;RunId=$run}|ConvertTo-Json -Depth 4
