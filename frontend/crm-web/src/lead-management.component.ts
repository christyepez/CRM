import { Component, signal } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

interface Lead {
  id: string;
  firstName?: string | null;
  lastName?: string | null;
  email?: string | null;
  companyName?: string | null;
  title?: string | null;
  phone?: string | null;
  status: string;
}

interface CrudResult<T> {
  allowed: boolean;
  data?: T | null;
  warning?: string;
}

interface QualificationResult {
  leadId: string;
  currentStatus: string;
  changed: boolean;
  message: string;
}

@Component({
  standalone: true,
  selector: 'crm-lead-management-page',
  imports: [ReactiveFormsModule],
  template: `
    <section class="workflow-shell" aria-labelledby="leadManagementTitle">
      <div class="workflow-hero">
        <div>
          <p class="eyebrow">CRM · Leads</p>
          <h1 id="leadManagementTitle">Gestión de Leads</h1>
          <p class="lede">Alta, edición, búsqueda y calificación de Leads dentro del Portal.</p>
        </div>
        <span class="scope-pill">Portal integrado</span>
      </div>

      <div class="lead-kpis">
        <article><strong>{{ leads().length }}</strong><span>Total</span></article>
        <article><strong>{{ countByStatus('Qualified') }}</strong><span>Calificados</span></article>
        <article><strong>{{ countByStatus('Disqualified') }}</strong><span>Descartados</span></article>
        <article><strong>{{ activePipelineCount() }}</strong><span>En seguimiento</span></article>
      </div>

      <section class="panel">
        <div class="panel-heading">
          <div>
            <h2>Leads</h2>
            <p class="muted">Selecciona un registro para editarlo o gestionar su calificación.</p>
          </div>
          <button type="button" class="secondary-action" (click)="startCreate()">Nuevo Lead</button>
        </div>

        <label for="leadSearch">Buscar</label>
        <input id="leadSearch" type="search" [value]="search()"
               (input)="search.set($any($event.target).value)"
               placeholder="Nombre, email, empresa, cargo">

        @if (loading()) {
          <p class="feedback neutral">Cargando Leads…</p>
        } @else {
          <div class="crm-grid-scroll">
            <table class="crm-grid">
              <thead>
                <tr><th>Lead</th><th>Empresa</th><th>Email / Teléfono</th><th>Cargo</th><th>Estado</th><th>Acciones</th></tr>
              </thead>
              <tbody>
                @for (lead of filteredLeads(); track lead.id) {
                  <tr [class.selected-row]="selectedId() === lead.id">
                    <td><strong>{{ leadName(lead) }}</strong></td>
                    <td>{{ lead.companyName || '—' }}</td>
                    <td>{{ lead.email || lead.phone || '—' }}</td>
                    <td>{{ lead.title || '—' }}</td>
                    <td><span class="contact-status">{{ lead.status }}</span></td>
                    <td class="crm-row-actions">
                      <button type="button" class="secondary-action" (click)="edit(lead)">Editar</button>
                      <button type="button" class="secondary-action" (click)="selectForQualification(lead)">Calificar</button>
                    </td>
                  </tr>
                } @empty {
                  <tr><td colspan="6">No hay Leads para el filtro actual.</td></tr>
                }
              </tbody>
            </table>
          </div>
        }
      </section>

      <div class="workflow-grid">
        <section class="panel">
          <div class="panel-heading">
            <div><h2>{{ createMode() ? 'Nuevo Lead' : 'Editar Lead' }}</h2><p class="muted">Datos comerciales básicos.</p></div>
            @if (!createMode()) { <button type="button" class="secondary-action" (click)="startCreate()">Cancelar edición</button> }
          </div>

          <form [formGroup]="leadForm" (ngSubmit)="saveLead()">
            <label>Nombres<input formControlName="firstName" maxlength="120"></label>
            <label>Apellidos<input formControlName="lastName" maxlength="120"></label>
            <label>Email<input type="email" formControlName="email" maxlength="120"></label>
            <label>Empresa<input formControlName="companyName" maxlength="120"></label>
            <label>Cargo<input formControlName="title" maxlength="120"></label>
            <label>Teléfono<input formControlName="phone" maxlength="120"></label>
            @if (!createMode()) {
              <label>Estado<input formControlName="status" maxlength="40"></label>
            }
            <button type="submit" [disabled]="saving() || leadForm.invalid">
              {{ saving() ? 'Guardando…' : (createMode() ? 'Crear Lead' : 'Guardar cambios') }}
            </button>
          </form>
        </section>

        <section class="panel">
          <div class="panel-heading">
            <div><h2>Calificación</h2><p class="muted">{{ selectedLead() ? leadName(selectedLead()!) : 'Selecciona un Lead' }}</p></div>
          </div>

          <form [formGroup]="qualificationForm" (ngSubmit)="qualify()">
            <label>Decisión
              <select formControlName="decision">
                <option value="Qualify">Calificar</option>
                <option value="Disqualify">Descartar</option>
              </select>
            </label>
            @if (qualificationForm.controls.decision.value === 'Disqualify') {
              <label>Motivo
                <select formControlName="disqualificationReason">
                  <option value="InvalidContactInformation">Datos de contacto inválidos</option>
                  <option value="Duplicate">Duplicado</option>
                  <option value="NoInterest">Sin interés</option>
                  <option value="OutOfTarget">Fuera del perfil</option>
                  <option value="Unreachable">No localizable</option>
                  <option value="Other">Otro</option>
                </select>
              </label>
            }
            <label>Comentario<textarea rows="4" maxlength="500" formControlName="comment"></textarea></label>
            <button type="submit" [disabled]="saving() || !selectedLead()">Aplicar calificación</button>
          </form>
        </section>
      </div>

      @if (message()) { <section class="panel result-panel"><p>{{ message() }}</p></section> }
      @if (error()) { <section class="panel error-panel" role="alert"><p>{{ error() }}</p></section> }
    </section>
  `
})
export class LeadManagementPageComponent {
  private readonly base = '/api/crm/foundation/leads';

  readonly leads = signal<Lead[]>([]);
  readonly search = signal('');
  readonly loading = signal(false);
  readonly saving = signal(false);
  readonly selectedId = signal<string | null>(null);
  readonly createMode = signal(true);
  readonly message = signal<string | null>(null);
  readonly error = signal<string | null>(null);

  readonly leadForm = this.fb.group({
    firstName: ['', Validators.maxLength(120)],
    lastName: ['', Validators.maxLength(120)],
    email: ['', Validators.email],
    companyName: ['', Validators.maxLength(120)],
    title: ['', Validators.maxLength(120)],
    phone: ['', Validators.maxLength(120)],
    status: ['PreviewOnly']
  });

  readonly qualificationForm = this.fb.nonNullable.group({
    decision: ['Qualify'],
    disqualificationReason: ['NoInterest'],
    comment: ['', Validators.maxLength(500)]
  });

  constructor(private readonly http: HttpClient, private readonly fb: FormBuilder) {
    this.load();
  }

  filteredLeads(): Lead[] {
    const term = this.search().trim().toLowerCase();
    if (!term) return this.leads();
    return this.leads().filter(lead =>
      [lead.firstName, lead.lastName, lead.email, lead.companyName, lead.title, lead.phone, lead.status]
        .filter(Boolean).some(value => String(value).toLowerCase().includes(term)));
  }

  selectedLead(): Lead | undefined {
    return this.leads().find(lead => lead.id === this.selectedId());
  }

  leadName(lead: Lead): string {
    return `${lead.firstName ?? ''} ${lead.lastName ?? ''}`.trim() || lead.email || lead.id;
  }

  countByStatus(status: string): number {
    return this.leads().filter(lead => lead.status.toLowerCase() === status.toLowerCase()).length;
  }

  activePipelineCount(): number {
    return this.leads().filter(lead => !['Qualified', 'Disqualified'].includes(lead.status)).length;
  }

  startCreate(): void {
    this.createMode.set(true);
    this.selectedId.set(null);
    this.message.set(null);
    this.error.set(null);
    this.leadForm.reset({ firstName: '', lastName: '', email: '', companyName: '', title: '', phone: '', status: 'PreviewOnly' });
  }

  edit(lead: Lead): void {
    this.createMode.set(false);
    this.selectedId.set(lead.id);
    this.message.set(null);
    this.error.set(null);
    this.leadForm.reset({
      firstName: lead.firstName ?? '',
      lastName: lead.lastName ?? '',
      email: lead.email ?? '',
      companyName: lead.companyName ?? '',
      title: lead.title ?? '',
      phone: lead.phone ?? '',
      status: lead.status
    });
  }

  selectForQualification(lead: Lead): void {
    this.selectedId.set(lead.id);
    this.message.set(null);
    this.error.set(null);
  }

  saveLead(): void {
    if (this.leadForm.invalid || this.saving()) return;
    const raw = this.leadForm.getRawValue();
    const body = {
      firstName: clean(raw.firstName),
      lastName: clean(raw.lastName),
      email: clean(raw.email),
      companyName: clean(raw.companyName),
      title: clean(raw.title),
      phone: clean(raw.phone),
      ...(this.createMode() ? {} : { status: clean(raw.status) || 'PreviewOnly' })
    };

    this.saving.set(true);
    this.error.set(null);
    const request = this.createMode()
      ? this.http.post<CrudResult<Lead>>(this.base, body)
      : this.http.put<CrudResult<Lead>>(`${this.base}/${encodeURIComponent(this.selectedId()!)}`, body);

    request.subscribe({
      next: response => {
        this.saving.set(false);
        if (!response.allowed) {
          this.error.set(response.warning || 'La operación fue denegada por la política CRM.');
          return;
        }
        this.message.set(this.createMode() ? 'Lead creado correctamente.' : 'Lead actualizado correctamente.');
        this.startCreate();
        this.load();
      },
      error: error => this.fail(error, 'No fue posible guardar el Lead.')
    });
  }

  qualify(): void {
    const lead = this.selectedLead();
    if (!lead || this.saving()) return;
    const raw = this.qualificationForm.getRawValue();
    const disqualify = raw.decision === 'Disqualify';
    const body = {
      decision: raw.decision,
      disqualificationReason: disqualify ? raw.disqualificationReason : null,
      otherReason: null,
      comment: clean(raw.comment)
    };

    this.saving.set(true);
    this.error.set(null);
    this.http.post<QualificationResult>(`${this.base}/${encodeURIComponent(lead.id)}/qualification`, body).subscribe({
      next: response => {
        this.saving.set(false);
        this.message.set(response.message || 'Calificación actualizada.');
        this.leads.update(items => items.map(item => item.id === lead.id ? { ...item, status: response.currentStatus } : item));
      },
      error: error => this.fail(error, 'No fue posible calificar el Lead.')
    });
  }

  private load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.http.get<CrudResult<Lead[]>>(this.base).subscribe({
      next: response => {
        this.leads.set(response.data ?? []);
        this.loading.set(false);
      },
      error: error => {
        this.loading.set(false);
        this.fail(error, 'No fue posible cargar los Leads.');
      }
    });
  }

  private fail(error: unknown, fallback: string): void {
    this.saving.set(false);
    this.loading.set(false);
    this.error.set(error instanceof HttpErrorResponse && error.status === 403
      ? 'No tienes permiso para ejecutar esta operación.'
      : fallback);
  }
}

function clean(value: string | null | undefined): string | null {
  const normalized = value?.trim() ?? '';
  return normalized || null;
}
