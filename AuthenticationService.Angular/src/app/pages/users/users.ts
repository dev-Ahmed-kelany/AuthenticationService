import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';

import { UserDetailsModel } from '../../models/users/user-details.model';
import { UserService } from '../../services/user.service';
import { AddNewUser } from '../../components/users/add-new-user/add-new-user';

@Component({
  selector: 'app-users',
  imports: [FormsModule, AddNewUser],
  templateUrl: './users.html',
})
export class Users implements OnInit, OnDestroy {
  private searchSubject = new Subject<string>();

  private destroySubject = new Subject<void>();

  searchText = '';

  selectedRoleId: number | null = null;

  selectedStatusId: number | null = null;

  users: UserDetailsModel[] = [];

  contextMenuVisible = false;

  contextMenuX = 0;

  contextMenuY = 0;

  selectedUser: UserDetailsModel | null = null;

  showAddUserDialog = false;

  constructor(private readonly userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();

    this.searchSubject
      .pipe(debounceTime(500), distinctUntilChanged(), takeUntil(this.destroySubject))
      .subscribe((searchText) => {
        this.searchUsers(searchText);
      });
  }

  ngOnDestroy(): void {
    this.destroySubject.next();

    this.destroySubject.complete();

    this.searchSubject.complete();
  }

  onSearchTextChanged(value: string): void {
    this.searchSubject.next(value);
  }

  loadUsers(): void {
    this.userService.GetAllAsync().subscribe((result) => {
      if (!result.isSuccess) {
        return;
      }

      this.users = result.data ?? [];
    });
  }

  openAddUserDialog(): void {
    this.showAddUserDialog = true;
  }

  closeAddUserDialog(): void {
    this.showAddUserDialog = false;
  }

  searchUsers(searchText: string = this.searchText): void {
    this.userService.SearchAsync(searchText).subscribe((result) => {
      if (!result.isSuccess) {
        // TODO: Display error

        return;
      }

      this.users = result.data ?? [];
    });
  }

  showContextMenu(event: MouseEvent, user: UserDetailsModel): void {
    event.preventDefault();

    this.selectedUser = user;

    this.contextMenuX = event.clientX;

    this.contextMenuY = event.clientY;

    this.contextMenuVisible = true;
  }

  editUser(): void {
    if (!this.selectedUser) {
      return;
    }

    // TODO: Open Edit User form

    this.closeContextMenu();
  }

  deleteUser(): void {
    if (!this.selectedUser) {
      return;
    }

    // TODO: Delete selected user

    this.closeContextMenu();
  }

  closeContextMenu(): void {
    this.contextMenuVisible = false;

    this.selectedUser = null;
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
