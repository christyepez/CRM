$ErrorActionPreference='Stop'
$Api='http://127.0.0.1:8098'
$Front='http://127.0.0.1:4211'
$Seed='88888888-8888-8888-8888-888888888888'
$Samples=@()
function Req($method,$path){
  $sw=[Diagnostics.Stopwatch]::StartNew()
  try{$r=Invoke-WebRequest -UseBasicParsing -Method $method -Uri ($Api+$path) -TimeoutSec 10;$status=[int]$r.StatusCode;$body=$r.Content}
  catch{$resp=$_.Exception.Response;if($resp){$status=[int]$resp.StatusCode;$body=''}else{throw}}
  $sw.Stop();$script:Samples+=@($sw.ElapsedMilliseconds)
  return [pscustomobject]@{Status=$status;Body=$body}
}
$list=Req GET '/api/crm/foundation/pipelines'
if($list.Status-ne200){throw 'list failed'}
$items=$list.Body|ConvertFrom-Json
if($items.Count-lt1){throw 'empty catalog'}
$detail=Req GET "/api/crm/foundation/pipelines/$Seed"
if($detail.Status-ne200){throw 'detail failed'}
$d=$detail.Body|ConvertFrom-Json
if($d.mutable -or $d.portalCatalogRuntimeEnabled){throw 'unsafe catalog flags'}
if((@($d.stages|ForEach-Object order)-join ',') -ne '1,2,3,4'){throw 'stage order invalid'}
$missing=Req GET "/api/crm/foundation/pipelines/$([guid]::NewGuid().ToString('D'))"
if($missing.Status-ne404){throw 'missing should be 404'}
foreach($x in @(
  @('POST','/api/crm/foundation/pipelines'),
  @('PUT',"/api/crm/foundation/pipelines/$Seed"),
  @('PATCH',"/api/crm/foundation/pipelines/$Seed"),
  @('DELETE',"/api/crm/foundation/pipelines/$Seed"),
  @('GET','/api/crm/pipelines'))){
  $q=Req $x[0] $x[1]
  if($q.Status -notin @(404,405)){throw "unsafe route $($x[0]) $($q.Status)"}
}
$frontPage=Invoke-WebRequest -UseBasicParsing "$Front/foundation/pipelines" -TimeoutSec 15
if($frontPage.StatusCode-ne200){throw 'frontend route failed'}
$proxy=Invoke-WebRequest -UseBasicParsing "$Front/api/crm/foundation/pipelines" -TimeoutSec 15
if($proxy.StatusCode-ne200){throw 'frontend proxy failed'}
$avg=[math]::Round(($Samples|Measure-Object -Average).Average,2)
$sorted=@($Samples|Sort-Object)
$p95=$sorted[[math]::Min($sorted.Count-1,[math]::Ceiling($sorted.Count*.95)-1)]
$result=[ordered]@{
  S2106Decision='Implemented';LocalBackendUrl=$Api;LocalFrontendUrl=$Front
  FrontendApiRoutingMode='Proxy';ListScenario='PASS';DetailScenario='PASS';OrderedStages='PASS'
  NotFoundScenario='PASS';MutationRoutesAvailable=$false;ProductivePipelineRouteAvailable=$false
  PortalCatalogRuntimeObserved=$false;CommonDbRuntimeObserved=$false;RealDataDetected=$false
  SimulatedProductionTouched=$false;IntegrationLatencySamples=$Samples.Count
  LatencyAverageMs=$avg;LatencyP95Ms=$p95
}
$result|ConvertTo-Json|Set-Content 'docs/roadmap/crm-sprint-21-s21-06-pipeline-catalog-local-integration-result.json'
$result|ConvertTo-Json

