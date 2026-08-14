import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { environment } from '../../environments/environment';

import { AuthenticationRequestModel } from '../models/authentication/authentication-request.model';
import { LoginResponseModel } from '../models/authentication/login-response.model';
import { RefreshTokenRequestModel } from '../models/authentication/refresh-token-request.model';
import { ChangePasswordModel } from '../models/authentication/change-password.model';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private readonly http: HttpClient) {}

  LoginAsync(request: AuthenticationRequestModel): Observable<Result<LoginResponseModel>> {
    return this.http.post<LoginResponseModel>(`${this.apiUrl}/Auth/Login`, request).pipe(
      map((data) => Result.Success<LoginResponseModel>(data)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let apiError: ApiError | null = null;

        if (response.error) {
          if (typeof response.error === 'string') {
            try {
              apiError = JSON.parse(response.error);
            } catch {
              apiError = null;
            }
          } else {
            apiError = response.error as ApiError;
          }
        }

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occured',
          statusCode,
        );

        return of(Result.Failure<LoginResponseModel>(error));
      }),
    );
  }

  RefreshAsync(request: RefreshTokenRequestModel): Observable<Result<LoginResponseModel>> {
    return this.http.post<LoginResponseModel>(`${this.apiUrl}/Auth/Refresh`, request).pipe(
      map((data) => Result.Success<LoginResponseModel>(data)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let apiError: ApiError | null = null;

        if (response.error) {
          if (typeof response.error === 'string') {
            try {
              apiError = JSON.parse(response.error);
            } catch {
              apiError = null;
            }
          } else {
            apiError = response.error as ApiError;
          }
        }

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occured',
          statusCode,
        );

        return of(Result.Failure<LoginResponseModel>(error));
      }),
    );
  }

  VerifyCredentialsAsync(request: AuthenticationRequestModel): Observable<Result<boolean>> {
    return this.http.post<void>(`${this.apiUrl}/Auth/verify-credentials`, request).pipe(
      map(() => Result.Success<boolean>(true)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let apiError: ApiError | null = null;

        if (response.error) {
          if (typeof response.error === 'string') {
            try {
              apiError = JSON.parse(response.error);
            } catch {
              apiError = null;
            }
          } else {
            apiError = response.error as ApiError;
          }
        }

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occured',
          statusCode,
        );

        return of(Result.Failure<boolean>(error));
      }),
    );
  }

  ChangePasswordAsync(request: ChangePasswordModel): Observable<Result<boolean>> {
    return this.http.post<void>(`${this.apiUrl}/Auth/change-password`, request).pipe(
      map(() => Result.Success<boolean>(true)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let apiError: ApiError | null = null;

        if (response.error) {
          if (typeof response.error === 'string') {
            try {
              apiError = JSON.parse(response.error);
            } catch {
              apiError = null;
            }
          } else {
            apiError = response.error as ApiError;
          }
        }

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occured',
          statusCode,
        );

        return of(Result.Failure<boolean>(error));
      }),
    );
  }

  LogoutAsync(request: RefreshTokenRequestModel): Observable<Result<boolean>> {
    return this.http.post<void>(`${this.apiUrl}/Auth/Logout`, request).pipe(
      map(() => Result.Success<boolean>(true)),

      catchError((response: HttpErrorResponse) => {
        const statusCode = response.status;

        let apiError: ApiError | null = null;

        if (response.error) {
          if (typeof response.error === 'string') {
            try {
              apiError = JSON.parse(response.error);
            } catch {
              apiError = null;
            }
          } else {
            apiError = response.error as ApiError;
          }
        }

        const error = new AppError(
          apiError?.code ?? 'Unknown',
          apiError?.message ?? 'An unexpected error occured',
          statusCode,
        );

        return of(Result.Failure<boolean>(error));
      }),
    );
  }
}
