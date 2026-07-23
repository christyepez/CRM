import { existsSync, readFileSync, readdirSync, statSync } from 'node:fs';
import { join } from 'node:path';

const root = process.cwd();
const required = [
  'package.json',
  'angular.json',
  'src/main.ts',
  'src/index.html',
  'src/styles.css'
];
const forbidden = ['localStorage', 'sessionStorage', 'access_token', 'refresh_token', 'AddIdentity', 'JwtBearer', 'CookieAuthentication', '<form', 'FormGroup', 'ngSubmit', 'CreateLead', 'UpdateLead', 'DeleteLead'];
const failures = [];

for (const file of required) {
  if (!existsSync(join(root, file))) {
    failures.push(`Missing ${file}`);
  }
}

function files(dir) {
  if (!existsSync(dir)) return [];
  return readdirSync(dir).flatMap(name => {
    const full = join(dir, name);
    if (name === 'node_modules' || name === 'dist' || name === '.angular') return [];
    return statSync(full).isDirectory() ? files(full) : [full];
  });
}

for (const file of files(join(root, 'src'))) {
  const text = readFileSync(file, 'utf8');
  for (const token of forbidden) {
    if (text.includes(token)) {
      failures.push(`Forbidden token '${token}' in ${file}`);
    }
  }
}

const main = readFileSync(join(root, 'src/main.ts'), 'utf8');
for (const expected of ['CRM Domain Catalog: Draft', 'Leads Foundation: PreviewOnly', 'Accounts Foundation: PreviewOnly', 'Contacts Foundation: PreviewOnly', 'Read Models: PreviewOnly', 'Persistence Strategy: Draft', 'Persistence: None', 'Portal Integration Planned', 'Portal Connected: false', 'Capability Owner: PortalCorporativo', 'Auth/Menu/Permissions/Audit/Notification/Configuration: External', 'Financial Integration Planned', 'Financial Connected: false', 'Financial Capability Owner: Financiero', 'Integration Pattern: API + Events + NoSharedDatabase', 'SRI/ATS/RIDE/XAdES: NotImplementedInCRM', 'Reporting Integration: Planned', 'Reporting Connected: false', 'Analytics Mode: Planned', 'KPI Catalog: Foundation', 'Dashboard Catalog: Foundation', 'Power BI Embed: NotConfigured', 'Sprint 1 Foundation: Closed', 'Productization: NotReady', 'Next Gate: Sprint2Planning', 'Runtime: NonProduction', 'Docker External Registry Note: BLOCKED_EXTERNAL_REGISTRY documented when MCR times out', 'Persistence Design Review: Active', 'Persistence Seam: Active', 'Persistence Mode: NonProductionSeam', 'Foundation Store: Enabled', 'Database Configured: false', 'DbContext Configured: false', 'Migration Ready: false', 'Durable Persistence: false', 'Productive CRUD: false', 'Persistence Next Gate: Sprint2P3PortalAuthorizationAdapterSimulation', 'Portal Authorization Simulation: Active', 'Portal Runtime Connected: false', 'Auth Owned By: PortalCorporativo', 'CRM Owns Auth: false', 'Token Storage: false', 'Productive Authorization: false', 'Authorization Next Gate: Sprint2P4ControlledCrudBehindFoundationFlag', 'Foundation CRUD: Enabled', 'Lead Foundation CRUD: Enabled', 'Account Foundation CRUD: Enabled', 'Contact Foundation CRUD: Enabled', 'Authorization Mode: FoundationSimulation', 'CRUD Next Gate: Sprint2P5IntegrationReadinessReview', 'Sprint 2 P5 Readiness Review: Active', 'Database Ready: false', 'Auth Ready: false', 'Productive CRUD Ready: false', 'Recommended Decision: ContinueReview', 'Productization Next Gate: Sprint2P6ProductizationGateDecision', 'Sprint 2: Closed', 'Productization Status: NotReady', 'Overall Decision: NoGoForProductiveActivation', 'Foundation CRUD Decision: GoFoundationOnly', 'Durable Persistence Decision: NoGo', 'Real Database Decision: NoGo', 'Portal Auth Runtime Decision: NoGo', 'Productive CRUD API Decision: NoGo', 'Sprint 3 Planning: Go', 'Next Gate: Sprint3P1DurablePersistenceSetupDesign', 'Sprint 3 P1 Durable Persistence Setup: DesignOnly', 'Real Database Configured: false', 'EF Runtime Enabled: false', 'Migrations Created: false', 'Connection Strings Configured: false', 'SQL Server Owned By CRM: false', 'Secret Strategy: PlannedOnly', 'Migration Strategy: PlannedOnly', 'Productive Activation: NoGo', 'Next Gate: Sprint3P2CommonDbConnectionContractAndSecretStrategy', 'Sprint 3 P2 Common DB Strategy: ContractOnly', 'Logical Database Name: CrmDb', 'Logical DB Placeholder: true', 'Secret Provider Configured: false', 'Secret Provider Runtime Connected: false', 'Next Gate: Sprint3P3EfDbContextPrototypeBehindDisabledFlag', 'Sprint 3 P3 EF Prototype: Exists', 'EF Prototype Exists: true', 'DbContext Runtime Active: false', 'Provider Configured: false', 'UseSqlServer Configured: false', 'Foundation Stores Remain Active: true', 'Productive CRUD Enabled: false', 'EF Prototype Warning: EF/DbContext prototype only; runtime disabled and no database configured', 'Next Gate: Sprint3P4PortalAuthRuntimeContractValidation', 'Sprint 3 P4 Portal Auth Runtime Contract: ContractOnly', 'Auth Runtime Enabled: false', 'Token Storage Enabled: false', 'Login Implemented By CRM: false', 'Identity Implemented By CRM: false', 'Permissions Persisted In CRM: false', 'Foundation Simulation Active: true', 'Productive Authorization Enabled: false', 'Portal Auth Runtime Warning: Portal Auth runtime contract validation only; no real Auth runtime configured', 'Next Gate: Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag', 'Non-Production']) {
  if (!main.includes(expected)) {
    failures.push(`Missing readiness label '${expected}'`);
  }
}

if (failures.length > 0) {
  console.error(failures.join('\n'));
  process.exit(1);
}

console.log('CRM frontend foundation checks passed.');
