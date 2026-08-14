import { Component, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-new-user',
  imports: [FormsModule],
  templateUrl: './add-new-user.html',
})
export class AddNewUser {
  closed = output<void>();

  name = '';

  username = '';

  email = '';

  password = '';

  roleId: number | null = null;

  statusId: number | null = null;

  addUser(): void {
    // TODO:
    // Call UserService later
  }

  close(): void {
    this.closed.emit();
  }
}
