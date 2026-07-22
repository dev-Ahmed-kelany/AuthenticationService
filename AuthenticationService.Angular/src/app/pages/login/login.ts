import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password = '';

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {}

  login(): void {
    this.authenticationService.login(this.username, this.password).subscribe({
      next: () => {
        this.router.navigate(['/dashboard']);
      },

      error: () => {
        alert('Unable to connect to the server.');
      },
    });
  }
}
