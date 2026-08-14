import { Component, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-new-role',
  imports: [FormsModule],
  templateUrl: './add-new-role.html',
})
export class AddNewRole {
  closed = output<void>();

  name = '';

  permissionsMask = 0;

  addRole(): void {
    // TODO:
    // Build request model
    // Call RoleService.AddAsync(...)
    // Handle Result
    // Close dialog after successful operation
  }

  close(): void {
    this.closed.emit();
  }
}
