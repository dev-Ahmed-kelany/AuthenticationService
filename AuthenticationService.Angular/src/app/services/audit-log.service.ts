import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { AuditLog } from '../models/audit-log';

@Injectable({
  providedIn: 'root',
})
export class AuditLogService {
  private readonly apiUrl = `${environment.apiUrl}/AuditLogs`;

  constructor(private http: HttpClient) {}

  addNewAuditLog(auditLog: AuditLog): Observable<number> {
    return this.http.post<number>(this.apiUrl, auditLog);
  }

  getAuditLogByID(id: number): Observable<AuditLog> {
    return this.http.get<AuditLog>(`${this.apiUrl}/${id}`);
  }

  getAllAuditLogs(): Observable<AuditLog[]> {
    return this.http.get<AuditLog[]>(this.apiUrl);
  }

  getAuditLogsByUserID(userID: number): Observable<AuditLog[]> {
    return this.http.get<AuditLog[]>(`${this.apiUrl}/User/${userID}`);
  }

  searchAuditLogs(searchText: string): Observable<AuditLog[]> {
    return this.http.get<AuditLog[]>(
      `${this.apiUrl}/Search?SearchText=${encodeURIComponent(searchText)}`,
    );
  }

  filterAuditLogs(entityID?: number, operationTypeID?: number): Observable<AuditLog[]> {
    let params = new HttpParams();

    if (entityID !== undefined) {
      params = params.set('EntityID', entityID);
    }

    if (operationTypeID !== undefined) {
      params = params.set('OperationTypeID', operationTypeID);
    }

    return this.http.get<AuditLog[]>(`${this.apiUrl}/Filter`, { params });
  }
}
