import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private readonly apiUrl = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<void> {
    return this.http.post<void>(
      `${this.apiUrl}/Login?Username=${encodeURIComponent(username)}&Password=${encodeURIComponent(password)}`,
      {},
    );
  }

  verifyCredentials(username: string, password: string): Observable<boolean> {
    return this.http.post<boolean>(
      `${this.apiUrl}/Verify-Credentials?Username=${encodeURIComponent(username)}&Password=${encodeURIComponent(password)}`,
      {},
    );
  }

  changePassword(
    username: string,
    currentPassword: string,
    newPassword: string,
  ): Observable<boolean> {
    return this.http.post<boolean>(
      `${this.apiUrl}/Change-Password?Username=${encodeURIComponent(username)}&CurrentPassword=${encodeURIComponent(currentPassword)}&NewPassword=${encodeURIComponent(newPassword)}`,
      {},
    );
  }
}
