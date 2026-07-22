import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { Permission } from '../models/permission';

@Injectable({
  providedIn: 'root',
})
export class PermissionService {
  private readonly apiUrl = `${environment.apiUrl}/Permissions`;

  constructor(private http: HttpClient) {}

  addNewPermission(name: string, bitValue: number): Observable<number> {
    return this.http.post<number>(
      `${this.apiUrl}?Name=${encodeURIComponent(name)}&BitValue=${bitValue}`,
      {},
    );
  }

  updatePermissionByID(id: number, name: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}?Name=${encodeURIComponent(name)}`, {});
  }

  searchPermissionsByName(searchText: string): Observable<Permission[]> {
    return this.http.get<Permission[]>(
      `${this.apiUrl}/Search?SearchText=${encodeURIComponent(searchText)}`,
    );
  }

  getPermissionByID(id: number): Observable<Permission> {
    return this.http.get<Permission>(`${this.apiUrl}/${id}`);
  }

  getAllPermissions(): Observable<Permission[]> {
    return this.http.get<Permission[]>(this.apiUrl);
  }
}
