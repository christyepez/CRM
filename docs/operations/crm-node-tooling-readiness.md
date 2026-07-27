# CRM Node Tooling Readiness

Node in PATH is helpful but not required for the frontend verifier.

Preferred:

```powershell
node --version
pnpm install --frozen-lockfile
pnpm run build
pnpm test
```

Fallback when Node is not on PATH:

```powershell
C:\Users\ChristianYepez\.cache\codex-runtimes\codex-primary-runtime\dependencies\node\bin\node.exe frontend\crm-web\tools\verify-crm-foundation.mjs
```

If Angular build cannot resolve workspace paths due local access restrictions, keep the failure as an environment warning and run the foundation verifier.
