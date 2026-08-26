# CRM Sprint 10 P44F - Approval Packet Hash Root Cause Analysis

ExpectedHash: 15c4f02bfb5f09824d6facb41629e262db2d7fa571458c548b4bb882c554ca12
ActualHash: 0a212d1d11c1a70a2b1019f04dc1607d776c0b2c4f7c67829fac1cdf584fdf44

CanonicalizationUsedInP44D: Markdown file hash excluding the FinalApprovalPacketHash line.
CanonicalizationUsedInP44E: Recomputed Markdown-derived hash using the same documented exclusion rule.
DifferenceFound: The P44D packet embedded a hash into the same mutable Markdown evidence file whose surrounding metadata changed after the first computation and merge. The packet identity depended on a free-form Markdown document instead of a stable structured object.
RootCause: Non-canonical source representation. Markdown formatting, appended hash metadata, line ending normalization and free-form evidence fields made the hash definition sensitive to non-semantic file changes.
MaterialImpact: FinalApprovalPacketIdentityMatched became false in P44E, producing ProductionApprovalDriftDetected: true and blocking P45.
CorrectiveAction: Create a new packet id CRM-S10-P44F-PACKET-V3 as deterministic JSON and hash only its canonical object representation using tools/approval-packet-hash.ps1.

FieldOrderingEvaluated: true
WhitespaceEvaluated: true
LineEndingsEvaluated: true
MarkdownFormattingEvaluated: true
GeneratedTimestampEvaluated: true
DynamicPathsEvaluated: true
EnvironmentDependentValuesEvaluated: true
HashSourceFileSelectionEvaluated: true
TrailingNewlineEvaluated: true
ScopeRepresentationEvaluated: true
CandidateImageRepresentationEvaluated: true
RollbackRepresentationEvaluated: true
