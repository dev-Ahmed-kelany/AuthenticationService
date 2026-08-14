import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { PermissionDetailsModel } from '../../models/permissions/permission-details.model';
import { AddNewPermission } from '../../components/permissions/add-new-permission/add-new-permission';

@Component({
  selector: 'app-permissions',
  imports: [FormsModule, AddNewPermission],
  templateUrl: './permissions.html',
})
export class Permissions {
  searchText = '';

  permissions: PermissionDetailsModel[] = [];

  contextMenuVisible = false;

  contextMenuX = 0;

  contextMenuY = 0;

  selectedPermission: PermissionDetailsModel | null = null;

  showAddPermissionDialog = false;

  openAddPermissionDialog(): void {
    this.showAddPermissionDialog = true;
  }

  closeAddPermissionDialog(): void {
    this.showAddPermissionDialog = false;
  }

  searchPermissions(): void {
    // TODO: Search permissions by name
  }

  showContextMenu(event: MouseEvent, permission: PermissionDetailsModel): void {
    event.preventDefault();

    this.selectedPermission = permission;

    this.contextMenuX = event.clientX;

    this.contextMenuY = event.clientY;

    this.contextMenuVisible = true;
  }

  editPermission(): void {
    if (!this.selectedPermission) {
      return;
    }

    // TODO: Open Edit Permission form

    this.closeContextMenu();
  }

  closeContextMenu(): void {
    this.contextMenuVisible = false;

    this.selectedPermission = null;
  }
}
