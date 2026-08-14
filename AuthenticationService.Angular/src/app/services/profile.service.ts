import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { Result } from '../models/result/result.model';
import { AppError } from '../models/result/app-error.model';
import { ApiError } from '../models/result/api-error.model';

import { ProfileDetailsModel } from '../models/profile/profile-details.model';
import { Session } from '../models/authentication/session';

import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient,
    private readonly authenticationService: AuthenticationService,
  ) {}

  GetByUserIDAsync(
    id: number,
    retryAfterRefresh: boolean = true,
  ): Observable<Result<ProfileDetailsModel | null>> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${Session.AccessToken}`,
    });

    return this.http.get<ProfileDetailsModel>(`${this.apiUrl}/Profile/${id}`, { headers }).pipe(
      map((data) => Result.Success<ProfileDetailsModel | null>(data)),

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
                  return of(Result.Failure<ProfileDetailsModel | null>(result.error));
                }

                Session.RefreshTokens(result.data ?? null);

                return this.GetByUserIDAsync(id, false);
              }),
            );
          }

          return of(
            Result.Failure<ProfileDetailsModel | null>(
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

        return of(Result.Failure<ProfileDetailsModel | null>(error));
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
