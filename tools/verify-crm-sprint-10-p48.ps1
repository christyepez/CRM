$ErrorActionPreference = 'Stop'

function Assert-FileContains {
    param(
        [string] $Path,
        [string] $Pattern,
        [string] $Message
    )

    if (-not (Test-Path $Path)) {
        throw "Missing required file: $Path"
    }

    $content = Get-Content $Path -Raw
    if ($content -notmatch [regex]::Escape($Pattern)) {
        throw $Message
    }
}

$approval = 'docs/roadmap/crm-sprint-10-p48-local-simulated-production-human-approval.md'
$entry = 'docs/roadmap/crm-sprint-10-p49-entry-conditions-p48.md'

Assert-FileContains $approval 'ApprovalDecision: APPROVE' 'P48 approval decision is not APPROVE.'
Assert-FileContains $approval 'EnvironmentClassification: SimulatedProduction' 'P48 environment classification mismatch.'
Assert-FileContains $approval 'RealProduction: false' 'P48 must not authorize real Production.'
Assert-FileContains $approval 'FinalApprovalPacketHash: f33a6af176066e90dbc674ae9393318dd934646cc6a747ef5ffd31ca988593a9' 'P48 packet hash mismatch.'
Assert-FileContains $approval 'ProductionTargetManifestHash: 075b67f6bf492e446908b21f365523252d91c76c5cc62e70faa62831313b61b5' 'P48 target hash mismatch.'
Assert-FileContains $approval 'RollbackBaselineHash: 9d4e5a95f5be179516f7fac160f855adb8595e7b8012acc9270fe6f6a93edf1d' 'P48 rollback hash mismatch.'
Assert-FileContains $approval 'CandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37' 'P48 image id mismatch.'
Assert-FileContains $approval 'ResidualRisksAccepted: true' 'P48 residual risks were not accepted.'
Assert-FileContains $approval 'SimulatedProductionExecutionAuthorized: true' 'P48 must authorize simulated Production execution.'
Assert-FileContains $approval 'RealProductionAuthorized: false' 'P48 must not authorize real Production.'
Assert-FileContains $approval 'P49Authorized: true' 'P49 must be authorized by P48.'
Assert-FileContains $approval 'ApprovalConsumed: false' 'P48 approval must remain unconsumed.'
Assert-FileContains $approval 'ProductionActivated: false' 'P48 must not activate Production.'

Assert-FileContains $entry 'P49Authorized: true' 'P49 entry conditions must be authorized.'
Assert-FileContains $entry 'RealProductionAuthorized: false' 'P49 entry conditions must not authorize real Production.'
Assert-FileContains $entry 'ApprovalConsumed: false' 'P49 entry conditions must keep approval unconsumed.'

& powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools\verify-crm-sprint-10-p47w.ps1

Write-Output 'PASS CRM P48 verifier confirmed explicit human approval for local simulated Production only.'
