import { createReadStream, existsSync, statSync } from 'node:fs';
import { createServer, request as httpRequest } from 'node:http';
import { extname, join, normalize, resolve } from 'node:path';

const port = Number(process.env.CRM_WEB_PORT ?? 4200);
const apiTarget = process.env.CRM_API_BASE_URL ?? 'http://localhost:8093';
const root = resolve('dist/crm-web/browser');

const contentTypes = new Map([
  ['.html', 'text/html; charset=utf-8'],
  ['.js', 'text/javascript; charset=utf-8'],
  ['.css', 'text/css; charset=utf-8'],
  ['.json', 'application/json; charset=utf-8'],
  ['.svg', 'image/svg+xml'],
  ['.ico', 'image/x-icon']
]);

function proxyApi(clientRequest, clientResponse) {
  const target = new URL(clientRequest.url ?? '/', apiTarget);
  const proxyRequest = httpRequest(
    target,
    {
      method: clientRequest.method,
      headers: {
        ...clientRequest.headers,
        host: target.host
      }
    },
    proxyResponse => {
      clientResponse.writeHead(proxyResponse.statusCode ?? 502, proxyResponse.headers);
      proxyResponse.pipe(clientResponse);
    }
  );

  proxyRequest.on('error', error => {
    clientResponse.writeHead(502, { 'content-type': 'application/json; charset=utf-8' });
    clientResponse.end(JSON.stringify({ error: 'ApiProxyUnavailable', message: error.message }));
  });

  clientRequest.pipe(proxyRequest);
}

function serveStatic(clientRequest, clientResponse) {
  const requestPath = decodeURIComponent((clientRequest.url ?? '/').split('?')[0]);
  const normalizedPath = normalize(requestPath).replace(/^(\.\.[/\\])+/, '');
  let filePath = join(root, normalizedPath);

  if (!existsSync(filePath) || statSync(filePath).isDirectory()) {
    filePath = join(root, 'index.html');
  }

  if (!filePath.startsWith(root)) {
    clientResponse.writeHead(403);
    clientResponse.end('Forbidden');
    return;
  }

  clientResponse.writeHead(200, {
    'content-type': contentTypes.get(extname(filePath)) ?? 'application/octet-stream',
    'cache-control': 'no-store'
  });
  createReadStream(filePath).pipe(clientResponse);
}

createServer((clientRequest, clientResponse) => {
  if ((clientRequest.url ?? '').startsWith('/api/')) {
    proxyApi(clientRequest, clientResponse);
    return;
  }

  serveStatic(clientRequest, clientResponse);
}).listen(port, '127.0.0.1', () => {
  console.log(`CRM local integration frontend listening on http://127.0.0.1:${port}`);
  console.log(`CRM local integration API proxy target ${apiTarget}`);
});
