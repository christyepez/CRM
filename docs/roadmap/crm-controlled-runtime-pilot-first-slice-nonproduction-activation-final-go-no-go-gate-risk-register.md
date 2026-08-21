# P36 Risk Register

- Final gate mistaken as GO: mitigated by `NonProductionActivationFinalGoNoGoDecision: NoGo`.
- Approval bypass: mitigated by `NonProductionActivationFinalGoApproved: false`.
- Runtime coupling drift: mitigated by disabled Portal runtime calls and compose isolation.
