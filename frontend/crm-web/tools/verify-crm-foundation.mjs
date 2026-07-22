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
const forbidden = ['localStorage', 'sessionStorage', 'token', 'login', 'Identity', 'roles', '<form', 'FormGroup', 'ngSubmit', 'CreateLead', 'UpdateLead', 'DeleteLead'];
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
for (const expected of ['CRM Domain Catalog: Draft', 'Leads Foundation: PreviewOnly', 'Accounts Foundation: PreviewOnly', 'Contacts Foundation: PreviewOnly', 'Read Models: PreviewOnly', 'Persistence Strategy: Draft', 'Persistence: None', 'Portal Integration Planned', 'Portal Connected: false', 'Capability Owner: PortalCorporativo', 'Auth/Menu/Permissions/Audit/Notification/Configuration: External', 'Financial Integration Planned', 'Financial Connected: false', 'Financial Capability Owner: Financiero', 'Integration Pattern: API + Events + NoSharedDatabase', 'SRI/ATS/RIDE/XAdES: NotImplementedInCRM', 'Non-Production']) {
  if (!main.includes(expected)) {
    failures.push(`Missing readiness label '${expected}'`);
  }
}

if (failures.length > 0) {
  console.error(failures.join('\n'));
  process.exit(1);
}

console.log('CRM frontend foundation checks passed.');
