import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { SaveRoleModel } from '../models/roles/save-role.model';
import { RoleDetailsModel } from '../models/roles/role-details.model';

import { Session } from '../models/authentication/session';

import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly authenticationService: AuthenticationService,
  ) {}

  AddAsync(role: SaveRoleModel, retryAfterRefresh: boolean = true): Observable<Result<number>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.post<number>(`${this.apiUrl}/Roles`, role, { headers }).pipe(
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

                return this.AddAsync(role, false);
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
    role: SaveRoleModel,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<boolean>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.put<void>(`${this.apiUrl}/Roles/${id}`, role, { headers }).pipe(
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

                return this.UpdateByIDAsync(id, role, false);
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
  ): Observable<Result<RoleDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<RoleDetailsModel[]>(`${this.apiUrl}/Roles/Search`, {
        headers,
        params: {
          SearchText: searchText,
        },
      })
      .pipe(
        map((data) => Result.Success<RoleDetailsModel[]>(data)),

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
                    return of(Result.Failure<RoleDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.SearchAsync(searchText, false);
                }),
              );
            }

            return of(
              Result.Failure<RoleDetailsModel[]>(
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

          return of(Result.Failure<RoleDetailsModel[]>(error));
        }),
      );
  }

  GetByIDAsync(
    id: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<RoleDetailsModel | null>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<RoleDetailsModel>(`${this.apiUrl}/Roles/${id}`, { headers }).pipe(
      map((data) => Result.Success<RoleDetailsModel | null>(data)),

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
                  return of(Result.Failure<RoleDetailsModel | null>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetByIDAsync(id, false);
              }),
            );
          }

          return of(
            Result.Failure<RoleDetailsModel | null>(
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

        return of(Result.Failure<RoleDetailsModel | null>(error));
      }),
    );
  }

  GetAllAsync(retryAfterRefresh: boolean = true): Observable<Result<RoleDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<RoleDetailsModel[]>(`${this.apiUrl}/Roles`, { headers }).pipe(
      map((data) => Result.Success<RoleDetailsModel[]>(data)),

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
                  return of(Result.Failure<RoleDetailsModel[]>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetAllAsync(false);
              }),
            );
          }

          return of(
            Result.Failure<RoleDetailsModel[]>(
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

        return of(Result.Failure<RoleDetailsModel[]>(error));
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
