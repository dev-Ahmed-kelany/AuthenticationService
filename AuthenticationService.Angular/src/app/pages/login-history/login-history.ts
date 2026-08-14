import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { LoginHistoryDetailsModel } from '../../models/login-history/login-history-details.model';

@Component({
  selector: 'app-login-history',
  imports: [FormsModule],
  templateUrl: './login-history.html',
})
export class LoginHistory {
  searchText = '';

  selectedUserId: number | null = null;

  selectedStatus: boolean | null = null;

  loginHistory: LoginHistoryDetailsModel[] = [];

  searchLoginHistory(): void {
    // TODO: Search and filter login history
  }
}
