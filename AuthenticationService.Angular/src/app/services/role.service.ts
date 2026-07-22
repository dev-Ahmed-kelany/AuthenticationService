import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { Role } from '../models/role';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private readonly apiUrl = `${environment.apiUrl}/Roles`;

  constructor(private http: HttpClient) {}

  addNewRole(name: string, permissionsMask: number): Observable<number> {
    return this.http.post<number>(
      `${this.apiUrl}?Name=${encodeURIComponent(name)}&PermissionsMask=${permissionsMask}`,
      {},
    );
  }

  updateRoleByID(id: number, name: string, permissionsMask: number): Observable<boolean> {
    return this.http.put<boolean>(
      `${this.apiUrl}/${id}?Name=${encodeURIComponent(name)}&PermissionsMask=${permissionsMask}`,
      {},
    );
  }

  searchRolesByName(searchText: string): Observable<Role[]> {
    return this.http.get<Role[]>(
      `${this.apiUrl}/Search?SearchText=${encodeURIComponent(searchText)}`,
    );
  }

  getRoleByID(id: number): Observable<Role> {
    return this.http.get<Role>(`${this.apiUrl}/${id}`);
  }

  getAllRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(this.apiUrl);
  }
}
