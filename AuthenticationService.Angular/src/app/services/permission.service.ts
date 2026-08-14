import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { CreatePermissionModel } from '../models/permissions/create-permission.model';
import { UpdatePermissionModel } from '../models/permissions/update-permission.model';
import { PermissionDetailsModel } from '../models/permissions/permission-details.model';

import { Session } from '../models/authentication/session';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class PermissionService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly authenticationService: AuthenticationService,
  ) {}

  AddAsync(
    permission: CreatePermissionModel,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<number>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.post<number>(`${this.apiUrl}/Permissions`, permission, { headers }).pipe(
      map((data) => Result.Success<number>(data)),

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
                  return of(Result.Failure<number>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.AddAsync(permission, false);
              }),
            );
          }

          return of(
            Result.Failure<number>(
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

        return of(Result.Failure<number>(error));
      }),
    );
  }

  UpdateByIDAsync(
    id: number,
    permission: UpdatePermissionModel,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<boolean>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.put<void>(`${this.apiUrl}/Permissions/${id}`, permission, { headers }).pipe(
      map(() => Result.Success<boolean>(true)),

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
                  return of(Result.Failure<boolean>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.UpdateByIDAsync(id, permission, false);
              }),
            );
          }

          return of(
            Result.Failure<boolean>(
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

        return of(Result.Failure<boolean>(error));
      }),
    );
  }

  SearchAsync(
    searchText: string,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<PermissionDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<PermissionDetailsModel[]>(`${this.apiUrl}/Permissions/Search`, {
        headers,
        params: {
          SearchText: searchText,
        },
      })
      .pipe(
        map((data) => Result.Success<PermissionDetailsModel[]>(data)),

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
                    return of(Result.Failure<PermissionDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.SearchAsync(searchText, false);
                }),
              );
            }

            return of(
              Result.Failure<PermissionDetailsModel[]>(
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

          return of(Result.Failure<PermissionDetailsModel[]>(error));
        }),
      );
  }

  GetByIDAsync(
    id: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<PermissionDetailsModel | null>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<PermissionDetailsModel>(`${this.apiUrl}/Permissions/${id}`, { headers })
      .pipe(
        map((data) => Result.Success<PermissionDetailsModel | null>(data)),

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
                    return of(Result.Failure<PermissionDetailsModel | null>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.GetByIDAsync(id, false);
                }),
              );
            }

            return of(
              Result.Failure<PermissionDetailsModel | null>(
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

          return of(Result.Failure<PermissionDetailsModel | null>(error));
        }),
      );
  }

  GetAllAsync(retryAfterRefresh: boolean = true): Observable<Result<PermissionDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<PermissionDetailsModel[]>(`${this.apiUrl}/Permissions`, { headers }).pipe(
      map((data) => Result.Success<PermissionDetailsModel[]>(data)),

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
                  return of(Result.Failure<PermissionDetailsModel[]>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetAllAsync(false);
              }),
            );
          }

          return of(
            Result.Failure<PermissionDetailsModel[]>(
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

        return of(Result.Failure<PermissionDetailsModel[]>(error));
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
