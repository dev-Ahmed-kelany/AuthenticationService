import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

import { Session } from '../../models/authentication/session';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-dashboard',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './dashboard.html',
})
export class Dashboard {
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {}

  logout(): void {
    const refreshToken = Session.RefreshToken;

    if (!refreshToken) {
      Session.Clear();
      return;
    }

    const request = { refreshToken: refreshToken };

    this.authenticationService.LogoutAsync(request).subscribe({
      next: (result) => {
        if (result.isSuccess) {
          Session.Clear();
          this.router.navigate(['/auth/login']);
          return;
        }

        alert(result.error.description);
      },
      error: (error) => {
        console.error('Logout failed:', error);
      },
    });
  }
}
