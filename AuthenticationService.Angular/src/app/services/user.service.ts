import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { User } from '../models/user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly apiUrl = `${environment.apiUrl}/Users`;

  constructor(private http: HttpClient) {}

  addNewUser(
    name: string,
    username: string,
    email: string,
    passwordHash: string,
    roleID: number,
    statusID: number,
  ): Observable<number> {
    return this.http.post<number>(
      `${this.apiUrl}?Name=${encodeURIComponent(name)}
&Username=${encodeURIComponent(username)}
&Email=${encodeURIComponent(email)}
&PasswordHash=${encodeURIComponent(passwordHash)}
&RoleID=${roleID}
&StatusID=${statusID}`.replace(/\n/g, ''),
      {},
    );
  }

  updateUserByID(
    id: number,
    name: string,
    username: string,
    email: string,
    roleID: number,
    statusID: number,
  ): Observable<boolean> {
    return this.http.put<boolean>(
      `${this.apiUrl}/${id}?Name=${encodeURIComponent(name)}
&Username=${encodeURIComponent(username)}
&Email=${encodeURIComponent(email)}
&RoleID=${roleID}
&StatusID=${statusID}`.replace(/\n/g, ''),
      {},
    );
  }

  deleteUserByID(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }

  searchUsers(searchText: string): Observable<User[]> {
    return this.http.get<User[]>(
      `${this.apiUrl}/Search?SearchText=${encodeURIComponent(searchText)}`,
    );
  }

  filterUsersByRoleID(roleID: number): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/Filter/Role/${roleID}`);
  }

  filterUsersByStatusID(statusID: number): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/Filter/Status/${statusID}`);
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getUserByID(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }
}
