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
      readModels: 'PreviewOnly',
      persistenceStrategy: 'Draft',
      persistence: 'None',
      portalIntegration: 'Planned',
      portalConnected: false,
      capabilityOwner: 'PortalCorporativo',
      portalCapabilities: 'Auth/Menu/Permissions/Audit/Notification/Configuration: External',
      financialIntegration: 'Planned',
      financialConnected: false,
      financialCapabilityOwner: 'Financiero',
      financialIntegrationPattern: 'API + Events + NoSharedDatabase',
      taxArtifacts: 'SRI/ATS/RIDE/XAdES: NotImplementedInCRM',
      reportingIntegration: 'Planned',
      reportingConnected: false,
      analyticsMode: 'Planned',
      kpiCatalog: 'Foundation',
      dashboardCatalog: 'Foundation',
      powerBiEmbed: 'NotConfigured',
      sprint1Foundation: 'Closed',
      productization: 'NotReady',
      nextGate: 'Sprint2Planning',
      dockerExternalRegistryNote: 'BLOCKED_EXTERNAL_REGISTRY documented when MCR times out',
      persistenceDesignReview: 'Active',
      persistenceMode: 'DesignOnly',
      databaseConfigured: false,
      migrationReady: false,
      persistenceNextGate: 'Sprint2P2PersistenceSeam',
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
      <p>Read Models: PreviewOnly</p>
      <p>Persistence Strategy: Draft</p>
      <p>Persistence: None</p>
      <p>Portal Integration Planned</p>
      <p>Portal Connected: false</p>
      <p>Capability Owner: PortalCorporativo</p>
      <p>Auth/Menu/Permissions/Audit/Notification/Configuration: External</p>
      <p>Financial Integration Planned</p>
      <p>Financial Connected: false</p>
      <p>Financial Capability Owner: Financiero</p>
      <p>Integration Pattern: API + Events + NoSharedDatabase</p>
      <p>SRI/ATS/RIDE/XAdES: NotImplementedInCRM</p>
      <p>Reporting Integration: Planned</p>
      <p>Reporting Connected: false</p>
      <p>Analytics Mode: Planned</p>
      <p>KPI Catalog: Foundation</p>
      <p>Dashboard Catalog: Foundation</p>
      <p>Power BI Embed: NotConfigured</p>
      <p>Sprint 1 Foundation: Closed</p>
      <p>Productization: NotReady</p>
      <p>Next Gate: Sprint2Planning</p>
      <p>Runtime: NonProduction</p>
      <p>Docker External Registry Note: BLOCKED_EXTERNAL_REGISTRY documented when MCR times out</p>
      <p>Persistence Design Review: Active</p>
      <p>Persistence Mode: DesignOnly</p>
      <p>Database Configured: false</p>
      <p>Migration Ready: false</p>
      <p>Persistence Next Gate: Sprint2P2PersistenceSeam</p>
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
