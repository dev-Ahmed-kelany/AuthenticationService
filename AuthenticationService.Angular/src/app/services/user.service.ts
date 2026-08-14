import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { switchMap, map, catchError } from 'rxjs/operators';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { environment } from '../../environments/environment';

import { CreateUserModel } from '../models/users/create-user.model';
import { UpdateUserModel } from '../models/users/update-user.model';
import { UserDetailsModel } from '../models/users/user-details.model';

import { AuthenticationService } from './authentication.service';
import { RefreshTokenRequestModel } from '../models/authentication/refresh-token-request.model';
import { Session } from '../models/authentication/session';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly authenticationService: AuthenticationService,
  ) {}

  AddAsync(user: CreateUserModel, retryAfterRefresh: boolean = true): Observable<Result<number>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.post<number>(`${this.apiUrl}/Users`, user, { headers }).pipe(
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
            } as RefreshTokenRequestModel);

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<number>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.AddAsync(user, false);
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
    user: UpdateUserModel,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<boolean>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.put<void>(`${this.apiUrl}/Users/${id}`, user, { headers }).pipe(
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
            } as RefreshTokenRequestModel);

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<boolean>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.UpdateByIDAsync(id, user, false);
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

  DeleteByIDAsync(id: number, retryAfterRefresh: boolean = true): Observable<Result<boolean>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.delete<void>(`${this.apiUrl}/Users/${id}`, { headers }).pipe(
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
            } as RefreshTokenRequestModel);

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<boolean>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.DeleteByIDAsync(id, false);
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
  ): Observable<Result<UserDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<UserDetailsModel[]>(`${this.apiUrl}/Users/Search`, {
        headers,
        params: {
          SearchText: searchText,
        },
      })
      .pipe(
        map((data) => Result.Success<UserDetailsModel[]>(data)),

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
              } as RefreshTokenRequestModel);

              return refreshResult.pipe(
                switchMap((result) => {
                  if (!result.isSuccess) {
                    return of(Result.Failure<UserDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.SearchAsync(searchText, false);
                }),
              );
            }

            return of(
              Result.Failure<UserDetailsModel[]>(
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

          return of(Result.Failure<UserDetailsModel[]>(error));
        }),
      );
  }

  FilterByRoleIDAsync(
    roleId: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<UserDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<UserDetailsModel[]>(`${this.apiUrl}/Users/Filter/Role/${roleId}`, { headers })
      .pipe(
        map((data) => Result.Success<UserDetailsModel[]>(data)),

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
              } as RefreshTokenRequestModel);

              return refreshResult.pipe(
                switchMap((result) => {
                  if (!result.isSuccess) {
                    return of(Result.Failure<UserDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.FilterByRoleIDAsync(roleId, false);
                }),
              );
            }

            return of(
              Result.Failure<UserDetailsModel[]>(
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

          return of(Result.Failure<UserDetailsModel[]>(error));
        }),
      );
  }

  FilterByStatusIDAsync(
    statusId: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<UserDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http
      .get<UserDetailsModel[]>(`${this.apiUrl}/Users/Filter/Status/${statusId}`, { headers })
      .pipe(
        map((data) => Result.Success<UserDetailsModel[]>(data)),

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
              } as RefreshTokenRequestModel);

              return refreshResult.pipe(
                switchMap((result) => {
                  if (!result.isSuccess) {
                    return of(Result.Failure<UserDetailsModel[]>(result.error));
                  }

                  Session.RefreshTokens(result.data ?? null);

                  return this.FilterByStatusIDAsync(statusId, false);
                }),
              );
            }

            return of(
              Result.Failure<UserDetailsModel[]>(
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

          return of(Result.Failure<UserDetailsModel[]>(error));
        }),
      );
  }

  GetAllAsync(retryAfterRefresh: boolean = true): Observable<Result<UserDetailsModel[]>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<UserDetailsModel[]>(`${this.apiUrl}/Users`, { headers }).pipe(
      map((data) => Result.Success<UserDetailsModel[]>(data)),

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
            } as RefreshTokenRequestModel);

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<UserDetailsModel[]>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetAllAsync(false);
              }),
            );
          }

          return of(
            Result.Failure<UserDetailsModel[]>(
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

        return of(Result.Failure<UserDetailsModel[]>(error));
      }),
    );
  }

  GetByIDAsync(
    id: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<UserDetailsModel | null>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<UserDetailsModel>(`${this.apiUrl}/Users/${id}`, { headers }).pipe(
      map((data) => Result.Success<UserDetailsModel | null>(data)),

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
            } as RefreshTokenRequestModel);

            return refreshResult.pipe(
              switchMap((result) => {
                if (!result.isSuccess) {
                  return of(Result.Failure<UserDetailsModel | null>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetByIDAsync(id, false);
              }),
            );
          }

          return of(
            Result.Failure<UserDetailsModel | null>(
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

        return of(Result.Failure<UserDetailsModel | null>(error));
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
