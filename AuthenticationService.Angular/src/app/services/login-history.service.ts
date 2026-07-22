import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { LoginHistory } from '../models/login-history';

@Injectable({
  providedIn: 'root',
})
export class LoginHistoryService {
  private readonly apiUrl = `${environment.apiUrl}/LoginHistory`;

  constructor(private http: HttpClient) {}

  addNewLoginHistory(loginHistory: LoginHistory): Observable<number> {
    return this.http.post<number>(this.apiUrl, loginHistory);
  }

  getLoginHistoryByID(id: number): Observable<LoginHistory> {
    return this.http.get<LoginHistory>(`${this.apiUrl}/${id}`);
  }

  getAllLoginHistory(): Observable<LoginHistory[]> {
    return this.http.get<LoginHistory[]>(this.apiUrl);
  }

  getLoginHistoryByUserID(userID: number): Observable<LoginHistory[]> {
    return this.http.get<LoginHistory[]>(`${this.apiUrl}/User/${userID}`);
  }

  searchLoginHistory(searchText: string): Observable<LoginHistory[]> {
    return this.http.get<LoginHistory[]>(
      `${this.apiUrl}/Search?SearchText=${encodeURIComponent(searchText)}`,
    );
  }

  filterLoginHistoryByStatus(status: number): Observable<LoginHistory[]> {
    return this.http.get<LoginHistory[]>(`${this.apiUrl}/Status/${status}`);
  }
}
