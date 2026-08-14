import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { AuditLogDetailsModel } from '../models/audit-logs/audit-log-details.model';
import { RefreshTokenRequestModel } from '../models/authentication/refresh-token-request.model';

import { AuthenticationService } from './authentication.service';
import { Session } from '../models/authentication/session';

@Injectable({
  providedIn: 'root',
})
export class AuditLogService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly authenticationService: AuthenticationService,
  ) {}

  GetByIDAsync(
    id: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<AuditLogDetailsModel | null>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<AuditLogDetailsModel>(`${this.apiUrl}/AuditLogs/${id}`, { headers }).pipe(
      map((data) => Result.Success<AuditLogDetailsModel | null>(data)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let jsonResponseError = '';

        if (typeof response.error === 'string') {
          jsonResponseError = response.error;
        } else if (response.error) {
          jsonResponseError = JSON.stringify(response.error);
        }

        if (!jsonResponseError.trim()) {
          if (statusCode === 401 && retryAfterRefresh) {
            const refreshResult = this.authenticationService.RefreshAsync({
              refreshToken: Session.RefreshToken ?? '',
            });

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<AuditLogDetailsModel | null>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetByIDAsync(id, false);
              }),
            );
          }

          return of(
            Result.Failure<AuditLogDetailsModel | null>(
              new AppError(
                'Forbidden',
                'You do not have permission to access this resource.',
                statusCode,
              ),
            ),
          );
        }

        const apiError = this.parseApiError(jsonResponseError);

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occurred.',
          statusCode,
        );

        return of(Result.Failure<AuditLogDetailsModel | null>(error));
      }),
    );
  }

  GetAllAsync(retryAfterRefresh: boolean = true): Observable<Result<AuditLogDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<AuditLogDetailsModel[]>(`${this.apiUrl}/AuditLogs`, { headers }).pipe(
      map((data) => Result.Success<AuditLogDetailsModel[]>(data)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let jsonResponseError = '';

        if (typeof response.error === 'string') {
          jsonResponseError = response.error;
        } else if (response.error) {
          jsonResponseError = JSON.stringify(response.error);
        }

        if (!jsonResponseError.trim()) {
          if (statusCode === 401 && retryAfterRefresh) {
            const refreshResult = this.authenticationService.RefreshAsync({
              refreshToken: Session.RefreshToken ?? '',
            });

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<AuditLogDetailsModel[]>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetAllAsync(false);
              }),
            );
          }

          return of(
            Result.Failure<AuditLogDetailsModel[]>(
              new AppError(
                'Forbidden',
                'You do not have permission to access this resource.',
                statusCode,
              ),
            ),
          );
        }

        const apiError = this.parseApiError(jsonResponseError);

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occurred.',
          statusCode,
        );

        return of(Result.Failure<AuditLogDetailsModel[]>(error));
      }),
    );
  }

  GetByUserIDAsync(
    userId: number,
    retryAfterRefresh: boolean,
  ): Observable<Result<AuditLogDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<AuditLogDetailsModel[]>(`${this.apiUrl}/AuditLogs/User/${userId}`, { headers })
      .pipe(
        map((data) => Result.Success<AuditLogDetailsModel[]>(data)),

        catchError((response: HttpErrorResponse) => {
          const statusCode = response.status;

          let jsonResponseError = '';

          if (typeof response.error === 'string') {
            jsonResponseError = response.error;
          } else if (response.error) {
            jsonResponseError = JSON.stringify(response.error);
          }

          if (!jsonResponseError.trim()) {
            if (statusCode === 401 && retryAfterRefresh) {
              const refreshResult = this.authenticationService.RefreshAsync({
                refreshToken: Session.RefreshToken ?? '',
              });

              return refreshResult.pipe(
                switchMap((result) => {
                  if (!result.isSuccess) {
                    return of(Result.Failure<AuditLogDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.GetByUserIDAsync(userId, false);
                }),
              );
            }

            return of(
              Result.Failure<AuditLogDetailsModel[]>(
                new AppError(
                  'Forbidden',
                  'You do not have permission to access this resource.',
                  statusCode,
                ),
              ),
            );
          }

          const apiError = this.parseApiError(jsonResponseError);

          const error = new AppError(
            apiError?.code ?? 'Unknown',
            apiError?.message ?? 'An unexpected error occurred.',
            statusCode,
          );

          return of(Result.Failure<AuditLogDetailsModel[]>(error));
        }),
      );
  }

  SearchAsync(
    searchText: string,
    retryAfterRefresh: boolean,
  ): Observable<Result<AuditLogDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<AuditLogDetailsModel[]>(`${this.apiUrl}/AuditLogs/Search`, {
        headers,
        params: {
          SearchText: searchText,
        },
      })
      .pipe(
        map((data) => Result.Success<AuditLogDetailsModel[]>(data)),

        catchError((response: HttpErrorResponse) => {
          const statusCode = response.status;

          let jsonResponseError = '';

          if (typeof response.error === 'string') {
            jsonResponseError = response.error;
          } else if (response.error) {
            jsonResponseError = JSON.stringify(response.error);
          }

          if (!jsonResponseError.trim()) {
            if (statusCode === 401 && retryAfterRefresh) {
              const refreshResult = this.authenticationService.RefreshAsync({
                refreshToken: Session.RefreshToken ?? '',
              });

              return refreshResult.pipe(
                switchMap((result) => {
                  if (!result.isSuccess) {
                    return of(Result.Failure<AuditLogDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.SearchAsync(searchText, false);
                }),
              );
            }

            return of(
              Result.Failure<AuditLogDetailsModel[]>(
                new AppError(
                  'Forbidden',
                  'You do not have permission to access this resource.',
                  statusCode,
                ),
              ),
            );
          }

          const apiError = this.parseApiError(jsonResponseError);

          const error = new AppError(
            apiError?.code ?? 'Unknown',
            apiError?.message ?? 'An unexpected error occurred.',
            statusCode,
          );

          return of(Result.Failure<AuditLogDetailsModel[]>(error));
        }),
      );
  }

  FilterAsync(
    entityId: number | null,
    operationTypeId: number | null,
    retryAfterRefresh: boolean,
  ): Observable<Result<AuditLogDetailsModel[]>> {
    let url = `${this.apiUrl}/AuditLogs/Filter?`;

    const params: string[] = [];

    if (entityId !== null) {
      params.push(`EntityID=${entityId}`);
    }

    if (operationTypeId !== null) {
      params.push(`OperationTypeID=${operationTypeId}`);
    }

    url += params.join('&');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<AuditLogDetailsModel[]>(url, { headers }).pipe(
      map((data) => Result.Success<AuditLogDetailsModel[]>(data)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let jsonResponseError = '';

        if (typeof response.error === 'string') {
          jsonResponseError = response.error;
        } else if (response.error) {
          jsonResponseError = JSON.stringify(response.error);
        }

        if (!jsonResponseError.trim()) {
          if (statusCode === 401 && retryAfterRefresh) {
            const refreshResult = this.authenticationService.RefreshAsync({
              refreshToken: Session.RefreshToken ?? '',
            });

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<AuditLogDetailsModel[]>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.FilterAsync(entityId, operationTypeId, false);
              }),
            );
          }

          return of(
            Result.Failure<AuditLogDetailsModel[]>(
              new AppError(
                'Forbidden',
                'You do not have permission to access this resource.',
                statusCode,
              ),
            ),
          );
        }

        const apiError = this.parseApiError(jsonResponseError);

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occurred.',
          statusCode,
        );

        return of(Result.Failure<AuditLogDetailsModel[]>(error));
      }),
    );
  }

  private parseApiError(jsonResponseError: string): ApiError | null {
    try {
      return JSON.parse(jsonResponseError) as ApiError;
    } catch {
      return null;
    }
  }
}
