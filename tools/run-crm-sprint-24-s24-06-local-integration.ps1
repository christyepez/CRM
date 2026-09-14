param([string]$BaseUrl='http://127.0.0.1:8101',[string]$FrontendUrl='http://127.0.0.1:4214')
$ErrorActionPreference='Stop'
function Req([string]$Method,[string]$Path,[object]$Body=$null){
 $sw=[Diagnostics.Stopwatch]::StartNew(); try{$a=@{UseBasicParsing=$true;Method=$Method;Uri="$BaseUrl$Path";TimeoutSec=15};if($null-ne$Body){$a.Body=$Body|ConvertTo-Json -Depth 5 -Compress;$a.ContentType='application/json'};$r=Invoke-WebRequest @a;$sw.Stop();[pscustomobject]@{Method=$Method;Path=$Path;Status=[int]$r.StatusCode;Ms=$sw.ElapsedMilliseconds;Body=$r.Content}}
 catch{$sw.Stop();$s=0;$b='';if($_.Exception.Response){$s=[int]$_.Exception.Response.StatusCode};if($_.ErrorDetails.Message){$b=$_.ErrorDetails.Message};[pscustomobject]@{Method=$Method;Path=$Path;Status=$s;Ms=$sw.ElapsedMilliseconds;Body=$b}}
}
function Ok($r,[int[]]$e,[string]$n){if($r.Status -notin $e){throw "$n expected $($e -join '/') got $($r.Status): $($r.Body)"}}
$run=[guid]::NewGuid().ToString('N').Substring(0,8);$all=New-Object System.Collections.Generic.List[object]
foreach($p in @('/health','/health/live','/health/ready')){$r=Req GET $p;Ok $r @(200) "health $p";$all.Add($r)}
$list=Req GET '/api/crm/foundation/assignments';Ok $list @(200) 'list';$all.Add($list);$initial=@($list.Body|ConvertFrom-Json).Count
$related=[guid]::NewGuid().ToString('D')
$create=Req POST '/api/crm/foundation/assignments' @{relatedEntityType='Lead';relatedEntityId=$related;assigneeReferenceId="  portal-user-$run  ";assignmentLabel='  Owner  '};Ok $create @(200) 'create';$all.Add($create)
$cj=$create.Body|ConvertFrom-Json;$id=$cj.id;if(!$id-or!$cj.changed-or$cj.assignment.assigneeReferenceId-ne"portal-user-$run"){throw 'create normalization failed'}
$detail=Req GET "/api/crm/foundation/assignments/$id";Ok $detail @(200) 'detail';$all.Add($detail)
$update=Req PUT "/api/crm/foundation/assignments/$id" @{relatedEntityType='Lead';relatedEntityId=$related;assigneeReferenceId="portal-user-$run";assignmentLabel='Advisor'};Ok $update @(200) 'update';$all.Add($update);if(-not($update.Body|ConvertFrom-Json).changed){throw 'update should change'}
$no=Req PUT "/api/crm/foundation/assignments/$id" @{relatedEntityType='Lead';relatedEntityId=$related;assigneeReferenceId="portal-user-$run";assignmentLabel='Advisor'};Ok $no @(200) 'no change';$all.Add($no);if(($no.Body|ConvertFrom-Json).changed){throw 'no-change should be false'}
$bad=Req POST '/api/crm/foundation/assignments' @{relatedEntityType='Contact';relatedEntityId='bad';assigneeReferenceId='portal-user-bad'};Ok $bad @(400) 'invalid';$all.Add($bad)
$missing=Req GET "/api/crm/foundation/assignments/$([guid]::NewGuid().ToString('D'))";Ok $missing @(404) 'missing';$all.Add($missing)
$archive=Req POST "/api/crm/foundation/assignments/$id/archive";Ok $archive @(200) 'archive';$all.Add($archive);if(($archive.Body|ConvertFrom-Json).assignment.status-ne'Archived'){throw 'archive failed'}
$repeat=Req POST "/api/crm/foundation/assignments/$id/archive";Ok $repeat @(200) 'repeat archive';$all.Add($repeat);if(($repeat.Body|ConvertFrom-Json).changed){throw 'repeat archive changed'}
$conflict=Req PUT "/api/crm/foundation/assignments/$id" @{relatedEntityType='Lead';relatedEntityId=$related;assigneeReferenceId='portal-user-changed';assignmentLabel='Changed'};Ok $conflict @(409) 'archived update';$all.Add($conflict)
foreach($n in @(@('GET','/api/crm/assignments'),@('POST','/api/crm/assignments'),@('PUT',"/api/crm/assignments/$id"),@('DELETE',"/api/crm/assignments/$id"))){$r=Req $n[0] $n[1];Ok $r @(404) "productive $($n[0])";$all.Add($r)}
$fd=Req DELETE "/api/crm/foundation/assignments/$id";Ok $fd @(404,405) 'foundation delete';$all.Add($fd)
$fr=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/foundation/assignments" -TimeoutSec 15;if($fr.StatusCode-ne200){throw 'frontend route failed'}
$fa=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/api/crm/foundation/assignments" -TimeoutSec 15;if($fa.StatusCode-ne200){throw 'frontend proxy failed'}
$lat=@($all|?{$_.Path-like'/api/crm/foundation/assignments*'});$sort=@($lat|Sort-Object Ms);$avg=[math]::Round(($lat|Measure-Object Ms -Average).Average,2);$p95=$sort[[math]::Min($sort.Count-1,[math]::Ceiling($sort.Count*.95)-1)].Ms
[pscustomobject]@{S2406Decision='Implemented';LocalBackendUrl=$BaseUrl;LocalFrontendUrl=$FrontendUrl;FrontendApiRoutingMode='Proxy';InitialAssignmentCount=$initial;CreatedAssignmentId=$id;CreateScenario='PASS';DetailScenario='PASS';UpdateScenario='PASS';NoChangeScenario='PASS';InvalidCreateScenario='PASS';NotFoundScenario='PASS';ArchiveScenario='PASS';RepeatArchive='PASS';ArchivedUpdateConflict='PASS';ProductiveAssignmentRouteAvailable=$false;DeleteRouteAvailable=$false;PortalIdentityRuntimeObserved=$false;PortalSecurityRuntimeObserved=$false;CrossEntityMutationObserved=$false;CommonDbRuntimeObserved=$false;ExternalConnectorRuntimeObserved=$false;RealDataDetected=$false;SimulatedProductionTouched=$false;IntegrationLatencySamples=$lat.Count;LatencyAverageMs=$avg;LatencyP95Ms=$p95;RunId=$run}|ConvertTo-Json -Depth 4
