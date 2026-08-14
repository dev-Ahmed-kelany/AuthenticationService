import { Component, output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-new-permission',
  imports: [FormsModule],
  templateUrl: './add-new-permission.html',
})
export class AddNewPermission {
  closed = output<void>();
  name = '';

  bitValue = 0;

  addPermission(): void {
    // TODO:
    // Build request model
    // Call PermissionService.AddAsync(...)
    // Handle Result
    // Close dialog after successful operation
  }

  close(): void {
    this.closed.emit();
  }
}
