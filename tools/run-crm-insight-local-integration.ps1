param([Parameter(Mandatory=$true)][string]$Key,[string]$BaseUrl='http://127.0.0.1:8103',[string]$FrontendUrl='http://127.0.0.1:4216',[string]$Sprint='26')
$ErrorActionPreference='Stop'
function Req([string]$Method,[string]$Path){$sw=[Diagnostics.Stopwatch]::StartNew();try{$r=Invoke-WebRequest -UseBasicParsing -Method $Method -Uri "$BaseUrl$Path" -TimeoutSec 10;$sw.Stop();[pscustomobject]@{Method=$Method;Path=$Path;Status=[int]$r.StatusCode;Ms=$sw.ElapsedMilliseconds;Body=$r.Content}}catch{$sw.Stop();$s=0;if($_.Exception.Response){$s=[int]$_.Exception.Response.StatusCode};[pscustomobject]@{Method=$Method;Path=$Path;Status=$s;Ms=$sw.ElapsedMilliseconds;Body=$_.ErrorDetails.Message}}}
function Ok($r,[int[]]$Expected,[string]$Name){if($r.Status -notin $Expected){throw "$Name expected $($Expected -join '/') got $($r.Status)"}}
$all=New-Object System.Collections.Generic.List[object]
foreach($p in @('/health','/health/live','/health/ready')){$r=Req GET $p;Ok $r @(200) "health $p";$all.Add($r)}
$list=Req GET '/api/crm/foundation/insights';Ok $list @(200) 'list';$all.Add($list)
$detail=Req GET "/api/crm/foundation/insights/$Key";Ok $detail @(200) 'detail';$all.Add($detail);$item=$detail.Body|ConvertFrom-Json
$missing=Req GET '/api/crm/foundation/insights/not-present';Ok $missing @(404) 'missing';$all.Add($missing)
foreach($m in @('POST','PUT','PATCH','DELETE')){$r=Req $m "/api/crm/foundation/insights/$Key";Ok $r @(404,405) "mutation $m";$all.Add($r)}
$prod=Req GET '/api/crm/insights';Ok $prod @(404) 'productive';$all.Add($prod)
$front=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/foundation/insights" -TimeoutSec 10;if($front.StatusCode-ne200){throw 'frontend route failed'}
$proxy=Invoke-WebRequest -UseBasicParsing "$FrontendUrl/api/crm/foundation/insights/$Key" -TimeoutSec 10;if($proxy.StatusCode-ne200){throw 'frontend proxy failed'}
$lat=@($all|Where-Object{$_.Path-like'/api/crm/foundation/insights*'});$sorted=@($lat|Sort-Object Ms);$avg=[math]::Round(($lat|Measure-Object Ms -Average).Average,2);$p95=$sorted[[math]::Min($sorted.Count-1,[math]::Ceiling($sorted.Count*.95)-1)].Ms
[pscustomobject]@{Sprint=$Sprint;Decision='Implemented';InsightKey=$Key;Title=$item.title;SourceMode=$item.sourceMode;ProductiveRuntimeEnabled=$item.productiveRuntimeEnabled;PortalRuntimeEnabled=$item.portalRuntimeEnabled;CommonDbRuntimeEnabled=$item.commonDbRuntimeEnabled;MutationRoutesAvailable=$false;ProductiveRouteAvailable=$false;FrontendRoute='PASS';FrontendProxy='PASS';IntegrationLatencySamples=$lat.Count;LatencyAverageMs=$avg;LatencyP95Ms=$p95;RunId=[guid]::NewGuid().ToString('N').Substring(0,8)}|ConvertTo-Json -Depth 4
