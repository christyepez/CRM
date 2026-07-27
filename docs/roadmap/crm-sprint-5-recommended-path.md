# CRM Sprint 5 Recommended Path

Recommended next package: `Sprint5P1ControlledRuntimeProbeActivationPlan`.

Sprint 5 should first design how runtime probes may be activated in non-production without introducing secrets, real customer data, productive routes, DELETE, Portal Auth production dependency or CRM-owned SQL Server.

Recommended sequence: P1 plan, P2 secret provider contract, P3 optional DB probe, P4 optional Portal Auth probe, P5 locked stub trial, P6 gate decision.
