import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewPermission } from './add-new-permission';

describe('AddNewPermission', () => {
  let component: AddNewPermission;
  let fixture: ComponentFixture<AddNewPermission>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddNewPermission],
    }).compileComponents();

    fixture = TestBed.createComponent(AddNewPermission);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
