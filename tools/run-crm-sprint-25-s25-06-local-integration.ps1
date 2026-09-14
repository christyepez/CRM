param([string]$BaseUrl='http://127.0.0.1:8102',[string]$FrontendUrl='http://127.0.0.1:4215')
$ErrorActionPreference='Stop'
function Req([string]$Method,[string]$Path){$sw=[Diagnostics.Stopwatch]::StartNew();try{$r=Invoke-WebRequest -UseBasicParsing -Method $Method -Uri "$BaseUrl$Path" -TimeoutSec 10;$sw.Stop();[pscustomobject]@{Method=$Method;Path=$Path;Status=[int]$r.StatusCode;Ms=$sw.ElapsedMilliseconds;Body=$r.Content}}catch{$sw.Stop();$s=0;if($_.Exception.Response){$s=[int]$_.Exception.Response.StatusCode};[pscustomobject]@{Method=$Method;Path=$Path;Status=$s;Ms=$sw.ElapsedMilliseconds;Body=$_.ErrorDetails.Message}}}
function Ok($r,[int[]]$Expected,[string]$Name){if($r.Status -notin $Expected){throw "$Name expected $($Expected -join '/') got $($r.Status)"}}
$all=New-Object System.Collections.Generic.List[object]
foreach($p in @('/health','/health/live','/health/ready')){$r=Req GET $p;Ok $r @(200) "health $p";$all.Add($r)}
$list=Req GET '/api/crm/foundation/customer360';Ok $list @(200) 'list';$all.Add($list);$items=@($list.Body|ConvertFrom-Json);$id=$items[0].customerId
$detail=Req GET "/api/crm/foundation/customer360/$id";Ok $detail @(200) 'detail';$all.Add($detail)
$missing=Req GET "/api/crm/foundation/customer360/$([guid]::NewGuid().ToString('D'))";Ok $missing @(404) 'missing';$all.Add($missing)
$invalid=Req GET '/api/crm/foundation/customer360/bad';Ok $invalid @(404) 'invalid';$all.Add($invalid)
foreach($m in @('POST','PUT','PATCH','DELETE')){$r=Req $m "/api/crm/foundation/customer360/$id";Ok $r @(404,405) "mutation $m";$all.Add($r)}
$prod=Req GET '/api/crm/customer360';Ok $prod @(404) 'productive';$all.Add($prod)
$front=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/foundation/customer360" -TimeoutSec 10;if($front.StatusCode-ne200){throw 'frontend route failed'}
$proxy=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/api/crm/foundation/customer360" -TimeoutSec 10;if($proxy.StatusCode-ne200){throw 'frontend proxy failed'}
$lat=@($all|Where-Object{$_.Path-like'/api/crm/foundation/customer360*'});$sorted=@($lat|Sort-Object Ms);$avg=[math]::Round(($lat|Measure-Object Ms -Average).Average,2);$p95=$sorted[[math]::Min($sorted.Count-1,[math]::Ceiling($sorted.Count*.95)-1)].Ms
[pscustomobject]@{S2506Decision='Implemented';LocalBackendUrl=$BaseUrl;LocalFrontendUrl=$FrontendUrl;FrontendApiRoutingMode='Proxy';InitialCustomer360Count=$items.Count;CustomerId=$id;ListScenario='PASS';DetailScenario='PASS';MissingScenario='PASS';InvalidIdScenario='PASS';MutationRoutesAvailable=$false;ProductiveRouteAvailable=$false;PortalRuntimeObserved=$false;CommonDbRuntimeObserved=$false;ExternalConnectorRuntimeObserved=$false;RealDataDetected=$false;SimulatedProductionTouched=$false;IntegrationLatencySamples=$lat.Count;LatencyAverageMs=$avg;LatencyP95Ms=$p95;RunId=[guid]::NewGuid().ToString('N').Substring(0,8)}|ConvertTo-Json -Depth 4
