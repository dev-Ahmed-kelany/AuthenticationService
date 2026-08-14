import { Component } from '@angular/core';

import { Session } from '../../models/authentication/session';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
})
export class Profile {
  profile = Session.User;

  getInitials(name: string): string {
    if (!name) {
      return '';
    }

    return name
      .split(' ')
      .filter((x) => x.length > 0)
      .slice(0, 2)
      .map((x) => x[0].toUpperCase())
      .join('');
  }
}
