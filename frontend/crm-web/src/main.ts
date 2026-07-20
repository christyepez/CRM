import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, RouterOutlet, Routes } from '@angular/router';
import { Component, Injectable, signal } from '@angular/core';
import { JsonPipe } from '@angular/common';

@Injectable({ providedIn: 'root' })
class CrmReadinessService {
  readonly apiBaseUrl = '/api';

  getReadiness() {
    return {
      module: 'CRM',
      status: 'ReadyForFoundationOnly',
      domainCatalog: 'Draft',
      leadsFoundation: 'PreviewOnly',
      accountsFoundation: 'PreviewOnly',
      contactsFoundation: 'PreviewOnly',
      persistence: 'None',
      portalIntegration: 'Planned',
      financialIntegration: 'Planned',
      runtimeMode: 'NonProduction',
      apiBaseUrl: this.apiBaseUrl
    };
  }
}

@Component({
  standalone: true,
  selector: 'crm-home',
  template: `
    <section class="card">
      <h1>CRM Foundation</h1>
      <p>CRM Domain Catalog: Draft</p>
      <p>Leads Foundation: PreviewOnly</p>
      <p>Accounts Foundation: PreviewOnly</p>
      <p>Contacts Foundation: PreviewOnly</p>
      <p>Persistence: None</p>
      <p>Portal Integration Planned</p>
      <p>Financial Integration Planned</p>
      <p>Non-Production</p>
    </section>
  `
})
class HomeComponent {
}

@Component({
  standalone: true,
  selector: 'crm-readiness',
  template: `
    <section class="card">
      <h1>CRM Readiness</h1>
      <pre>{{ readiness() | json }}</pre>
    </section>
  `,
  imports: [JsonPipe]
})
class ReadinessComponent {
  readonly readiness = signal(this.readinessService.getReadiness());

  constructor(private readonly readinessService: CrmReadinessService) {
  }
}

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'readiness', component: ReadinessComponent }
];

@Component({
  standalone: true,
  selector: 'crm-root',
  template: '<main><router-outlet /></main>',
  imports: [RouterOutlet]
})
class AppComponent {
}

bootstrapApplication(AppComponent, {
  providers: [provideRouter(routes)]
}).catch(error => console.error(error));
