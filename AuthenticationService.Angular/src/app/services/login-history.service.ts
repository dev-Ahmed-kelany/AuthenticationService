import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { LoginHistoryDetailsModel } from '../models/login-history/login-history-details.model';
import { Session } from '../models/authentication/session';

import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class LoginHistoryService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly authenticationService: AuthenticationService,
  ) {}

  GetByIDAsync(
    id: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<LoginHistoryDetailsModel | null>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<LoginHistoryDetailsModel>(`${this.apiUrl}/LoginHistory/${id}`, { headers })
      .pipe(
        map((data) => Result.Success<LoginHistoryDetailsModel | null>(data)),

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
                    return of(Result.Failure<LoginHistoryDetailsModel | null>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.GetByIDAsync(id, false);
                }),
              );
            }

            return of(
              Result.Failure<LoginHistoryDetailsModel | null>(
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

          return of(Result.Failure<LoginHistoryDetailsModel | null>(error));
        }),
      );
  }

  GetAllAsync(retryAfterRefresh: boolean = true): Observable<Result<LoginHistoryDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<LoginHistoryDetailsModel[]>(`${this.apiUrl}/LoginHistory`, { headers })
      .pipe(
        map((data) => Result.Success<LoginHistoryDetailsModel[]>(data)),

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
                    return of(Result.Failure<LoginHistoryDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.GetAllAsync(false);
                }),
              );
            }

            return of(
              Result.Failure<LoginHistoryDetailsModel[]>(
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

          return of(Result.Failure<LoginHistoryDetailsModel[]>(error));
        }),
      );
  }

  GetByUserIDAsync(
    userId: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<LoginHistoryDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<LoginHistoryDetailsModel[]>(`${this.apiUrl}/LoginHistory/User/${userId}`, { headers })
      .pipe(
        map((data) => Result.Success<LoginHistoryDetailsModel[]>(data)),

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
                    return of(Result.Failure<LoginHistoryDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.GetByUserIDAsync(userId, false);
                }),
              );
            }

            return of(
              Result.Failure<LoginHistoryDetailsModel[]>(
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

          return of(Result.Failure<LoginHistoryDetailsModel[]>(error));
        }),
      );
  }

  SearchAsync(
    searchText: string,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<LoginHistoryDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<LoginHistoryDetailsModel[]>(`${this.apiUrl}/LoginHistory/Search`, {
        headers,
        params: {
          SearchText: searchText,
        },
      })
      .pipe(
        map((data) => Result.Success<LoginHistoryDetailsModel[]>(data)),

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
                    return of(Result.Failure<LoginHistoryDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.SearchAsync(searchText, false);
                }),
              );
            }

            return of(
              Result.Failure<LoginHistoryDetailsModel[]>(
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

          return of(Result.Failure<LoginHistoryDetailsModel[]>(error));
        }),
      );
  }

  FilterByStatusAsync(
    status: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<LoginHistoryDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<LoginHistoryDetailsModel[]>(`${this.apiUrl}/LoginHistory/Status/${status}`, { headers })
      .pipe(
        map((data) => Result.Success<LoginHistoryDetailsModel[]>(data)),

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
                    return of(Result.Failure<LoginHistoryDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.FilterByStatusAsync(status, false);
                }),
              );
            }

            return of(
              Result.Failure<LoginHistoryDetailsModel[]>(
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

          return of(Result.Failure<LoginHistoryDetailsModel[]>(error));
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
