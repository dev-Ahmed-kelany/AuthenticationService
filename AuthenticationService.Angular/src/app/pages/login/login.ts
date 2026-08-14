import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationService } from '../../services/authentication.service';
import { ProfileService } from '../../services/profile.service';
import { Session } from '../../models/authentication/session';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password = '';
  showPassword = false;

  constructor(
    private authenticationService: AuthenticationService,
    private profileService: ProfileService,
    private router: Router,
  ) {}

  login(): void {
    this.authenticationService
      .LoginAsync({
        username: this.username,
        password: this.password,
      })
      .subscribe({
        next: (result) => {
          if (result.isSuccess) {
            Session.Start(result.data!);

            this.profileService.GetByUserIDAsync(1).subscribe((profileResult) => {
              if (!profileResult.isSuccess) {
                return;
              }

              Session.User = profileResult.data;
            });

            this.router.navigate(['/dashboard']);

            return;
          }

          alert(result.error.description);
        },

        error: () => {
          alert('Unable to connect to the server.');
        },
      });
  }
}
