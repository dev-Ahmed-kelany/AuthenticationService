import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { RoleDetailsModel } from '../../models/roles/role-details.model';
import { AddNewRole } from '../../components/roles/add-new-role/add-new-role';

@Component({
  selector: 'app-roles',
  imports: [FormsModule, AddNewRole],
  templateUrl: './roles.html',
})
export class Roles {
  searchText = '';

  roles: RoleDetailsModel[] = [];

  contextMenuVisible = false;

  contextMenuX = 0;

  contextMenuY = 0;

  selectedRole: RoleDetailsModel | null = null;

  showAddRoleDialog = false;

  openAddRoleDialog(): void {
    this.showAddRoleDialog = true;
  }

  closeAddRoleDialog(): void {
    this.showAddRoleDialog = false;
  }

  searchRoles(): void {
    // TODO: Search roles by name
  }

  showContextMenu(event: MouseEvent, role: RoleDetailsModel): void {
    event.preventDefault();

    this.selectedRole = role;

    this.contextMenuX = event.clientX;

    this.contextMenuY = event.clientY;

    this.contextMenuVisible = true;
  }

  editRole(): void {
    if (!this.selectedRole) {
      return;
    }

    // TODO: Open Edit Role form

    this.closeContextMenu();
  }

  closeContextMenu(): void {
    this.contextMenuVisible = false;

    this.selectedRole = null;
  }

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
