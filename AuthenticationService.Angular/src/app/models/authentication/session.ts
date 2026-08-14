import { LoginResponseModel } from './login-response.model';
import { ProfileDetailsModel } from '../profile/profile-details.model';

export class Session {
  static AccessToken: string | null = null;

  static RefreshToken: string | null = null;

  static AccessTokenExpiration: Date | null = null;

  static User?: ProfileDetailsModel | null = null;

  static Start(loginResponse: LoginResponseModel): void {
    Session.AccessToken = loginResponse.accessToken;

    Session.RefreshToken = loginResponse.refreshToken;

    Session.AccessTokenExpiration = loginResponse.accessTokenExpiresAt;
  }

  static RefreshTokens(response: LoginResponseModel | null): void {
    Session.AccessToken = response?.accessToken ?? null;

    Session.RefreshToken = response?.refreshToken ?? null;

    Session.AccessTokenExpiration = response?.accessTokenExpiresAt ?? null;
  }

  static Clear(): void {
    Session.AccessToken = null;

    Session.RefreshToken = null;

    Session.AccessTokenExpiration = null;

    Session.User = null;
  }
}
