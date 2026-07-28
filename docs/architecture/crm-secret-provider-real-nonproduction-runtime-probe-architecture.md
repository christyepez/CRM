# CRM Secret Provider Real NonProduction Runtime Probe Architecture

CRM keeps Secret Provider ownership outside the CRM bounded context. P2 only prepares an abstraction and a placeholder runtime probe that returns safe metadata.

Architecture rules:
- no CRM-owned secret storage;
- no Portal capability duplication;
- no DB runtime activation;
- no Portal Auth runtime activation;
- no productive CRM routes;
- no DELETE endpoints;
- no production activation.

The next gate is `Sprint7P3CommonDbRealConnectivityNonProductionProbe`, which remains blocked until secret provider runtime approval is granted.
