# CRM Portal Auth Real Runtime Probe Safety Boundary

The P4 boundary keeps CRM outside real Portal Auth runtime.

Safe behavior:

- return metadata from application services
- use synthetic references only
- document approvals and blocked items
- keep health/readiness available without Portal

Unsafe behavior:

- reading headers or tokens
- validating real tokens
- creating Portal HTTP clients
- resolving real Portal base URLs
- logging or returning secrets, URLs or tokens
- enabling Auth middleware or productive routes
