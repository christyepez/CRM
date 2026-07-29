# CRM Secret Provider Controlled Read Rollback

Rollback for P2:

1. Disable controlled read flag.
2. Remove provider access grant outside repository.
3. Restart CRM API if needed.
4. Verify foundation endpoint reports no read enabled.
5. Verify logs contain no secret values.

No data rollback is required for P1 because no read occurs.
