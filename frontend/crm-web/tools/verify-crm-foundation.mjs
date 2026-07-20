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
const forbidden = ['localStorage', 'sessionStorage', 'token', 'login', 'Identity', 'roles'];
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

if (failures.length > 0) {
  console.error(failures.join('\n'));
  process.exit(1);
}

console.log('CRM frontend foundation checks passed.');
