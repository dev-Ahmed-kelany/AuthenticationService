import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AuditLogDetailsModel } from '../../models/audit-logs/audit-log-details.model';

@Component({
  selector: 'app-audit-logs',
  imports: [FormsModule],
  templateUrl: './audit-logs.html',
})
export class AuditLogs {
  searchText = '';

  selectedUserId: number | null = null;

  selectedEntityId: number | null = null;

  selectedOperationTypeId: number | null = null;

  auditLogs: AuditLogDetailsModel[] = [];

  searchAuditLogs(): void {
    // TODO: Search and filter audit logs
  }
}
