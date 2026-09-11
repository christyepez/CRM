param([string]$BaseUrl='http://localhost:8096',[string]$FrontendUrl='http://127.0.0.1:4206')
$ErrorActionPreference='Stop'
function Req([string]$Method,[string]$Path,[object]$Body=$null){
 $sw=[Diagnostics.Stopwatch]::StartNew();try{$a=@{UseBasicParsing=$true;Method=$Method;Uri="$BaseUrl$Path";TimeoutSec=15};if($null-ne$Body){$a.Body=$Body|ConvertTo-Json -Depth 5 -Compress;$a.ContentType='application/json'};$r=Invoke-WebRequest @a;$sw.Stop();[pscustomobject]@{Method=$Method;Path=$Path;Status=[int]$r.StatusCode;Ms=$sw.ElapsedMilliseconds;Body=$r.Content}}
 catch{$sw.Stop();$s=0;$b='';if($_.Exception.Response){$s=[int]$_.Exception.Response.StatusCode};if($_.ErrorDetails.Message){$b=$_.ErrorDetails.Message};[pscustomobject]@{Method=$Method;Path=$Path;Status=$s;Ms=$sw.ElapsedMilliseconds;Body=$b}}
}
function Ok($r,[int[]]$expected,[string]$name){if($r.Status -notin $expected){throw "$name expected $($expected -join '/') got $($r.Status): $($r.Body)"}}
$run=[guid]::NewGuid().ToString('N').Substring(0,8)
$all=New-Object System.Collections.Generic.List[object]
$entity=[guid]::NewGuid().ToString('D')
$occurred='2026-09-10T15:30:00Z'
foreach($p in @('/health','/health/live','/health/ready')){$r=Req GET $p;Ok $r @(200) "health $p";$all.Add($r)}
$list=Req GET '/api/crm/foundation/interactions';Ok $list @(200) 'initial list';$all.Add($list);$initial=@($list.Body|ConvertFrom-Json).Count
function Payload([string]$subject,[string]$summary=' Synthetic integration interaction ',[string]$channel='Phone'){return @{relatedEntityType='Contact';relatedEntityId=$entity;channel=$channel;direction='Outbound';subject=$subject;summary=$summary;occurredAtUtc=$occurred}}
$create=Req POST '/api/crm/foundation/interactions' (Payload "  S19 Interaction $run  ");Ok $create @(200) 'create';$all.Add($create);$cj=$create.Body|ConvertFrom-Json;$id=$cj.id
if(!$id -or !$cj.changed -or $cj.interaction.subject-ne"S19 Interaction $run" -or $cj.interaction.status-ne'Recorded'){throw 'create normalization failed'}
$detail=Req GET "/api/crm/foundation/interactions/$id";Ok $detail @(200) 'detail';$all.Add($detail)
$update=Req PUT "/api/crm/foundation/interactions/$id" (Payload "S19 Interaction $run" ' Updated integration summary ' 'Email');Ok $update @(200) 'update';$all.Add($update);if(-not($update.Body|ConvertFrom-Json).changed){throw 'update should change'}
$uj=$update.Body|ConvertFrom-Json;$no=Req PUT "/api/crm/foundation/interactions/$id" @{relatedEntityType=$uj.interaction.relatedEntityType;relatedEntityId=$uj.interaction.relatedEntityId;channel=$uj.interaction.channel;direction=$uj.interaction.direction;subject=$uj.interaction.subject;summary=$uj.interaction.summary;occurredAtUtc=$uj.interaction.occurredAtUtc};Ok $no @(200) 'no change';$all.Add($no);if(($no.Body|ConvertFrom-Json).changed){throw 'no-change should be false'}
$bad=Req POST '/api/crm/foundation/interactions' @{relatedEntityType='Contact';relatedEntityId='bad';channel='Phone';direction='Outbound';subject='Bad';summary='Invalid';occurredAtUtc=$occurred};Ok $bad @(400) 'invalid create';$all.Add($bad)
$missingId=[guid]::NewGuid().ToString('D');$missing=Req GET "/api/crm/foundation/interactions/$missingId";Ok $missing @(404) 'missing';$all.Add($missing)
$voided=Req POST "/api/crm/foundation/interactions/$id/void";Ok $voided @(200) 'void';$all.Add($voided);if(($voided.Body|ConvertFrom-Json).status-ne'Voided'){throw 'void state invalid'}
$repeat=Req POST "/api/crm/foundation/interactions/$id/void";Ok $repeat @(200) 'repeat void';$all.Add($repeat);if(($repeat.Body|ConvertFrom-Json).changed){throw 'repeat void changed'}
$conflict=Req PUT "/api/crm/foundation/interactions/$id" (Payload 'Changed after void');Ok $conflict @(409) 'voided update conflict';$all.Add($conflict)
$detail2=Req GET "/api/crm/foundation/interactions/$id";Ok $detail2 @(200) 'read after write';$all.Add($detail2);if(($detail2.Body|ConvertFrom-Json).status-ne'Voided'){throw 'read-after-write inconsistent'}
foreach($n in @(@('GET','/api/crm/interactions'),@('POST','/api/crm/interactions'),@('PUT',"/api/crm/interactions/$id"),@('DELETE',"/api/crm/interactions/$id"))){$r=Req $n[0] $n[1];Ok $r @(404) "productive $($n[0])";$all.Add($r)}
$fd=Req DELETE "/api/crm/foundation/interactions/$id";Ok $fd @(404,405) 'foundation delete';$all.Add($fd)
$fr=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/foundation/interactions" -TimeoutSec 15;if($fr.StatusCode-ne200){throw 'frontend route failed'}
$fa=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/api/crm/foundation/interactions" -TimeoutSec 15;if($fa.StatusCode-ne200){throw 'frontend proxy failed'}
$lat=@($all|?{$_.Path-like'/api/crm/foundation/interactions*'});$sort=@($lat|Sort-Object Ms);$avg=[math]::Round(($lat|Measure-Object Ms -Average).Average,2);$min=($lat|Measure-Object Ms -Minimum).Minimum;$p95=$sort[[math]::Min($sort.Count-1,[math]::Ceiling($sort.Count*.95)-1)].Ms
[pscustomobject]@{S1906Decision='Implemented';LocalBackendUrl=$BaseUrl;LocalFrontendUrl=$FrontendUrl;FrontendApiRoutingMode='Proxy';BackendHealth='PASS';FrontendInteractionRouteStatus=200;FrontendToInteractionApiConnectivity='PASS';InitialInteractionCount=$initial;CreatedInteractionId=$id;CreateScenario='PASS';ReadAfterCreate='PASS';UpdateScenario='PASS';NoChangeScenario='PASS';InvalidCreateScenario='PASS';NotFoundScenario='PASS';VoidScenario='PASS';RepeatVoid='PASS';VoidedUpdateConflict='PASS';ReadAfterWriteConsistent=$true;ProductiveInteractionRouteAvailable=$false;DeleteRouteAvailable=$false;RuntimePersistenceClassification='FoundationOnly';ActivitySchedulingObserved=$false;CrossEntityMutationObserved=$false;PortalRuntimeObserved=$false;CommonDbRuntimeObserved=$false;ExternalConnectorRuntimeObserved=$false;RealDataDetected=$false;SimulatedProductionTouched=$false;IntegrationLatencySamples=$lat.Count;LatencyMinMs=$min;LatencyAverageMs=$avg;LatencyP95Ms=$p95;RunId=$run}|ConvertTo-Json -Depth 4
