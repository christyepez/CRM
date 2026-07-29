# CRM Controlled Runtime Activation Runbook

P1 operational status:
- RuntimeTrialsEnabledNow: false
- ProductionRuntimeEnabledNow: false
- ProductiveRoutesEnabledNow: false

Operators must not enable any trial from P1. Future P2-P5 trials require a separate PR, explicit NonProduction flag, rollback and evidence capture.

Health endpoints remain the only required runtime validation for P1.
