import { Component, OnInit } from '@angular/core';
import { CurrentUserViewModel } from 'src/app/shared/models/current-user.model';
import { RoleService } from 'src/app/core/services/role.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  currentUser: CurrentUserViewModel = new CurrentUserViewModel();
  constructor(private readonly roleService: RoleService) {
    this.getCurrentUser();
  }

  ngOnInit(): void {
  }
  getCurrentUser() {
    this.currentUser = this.roleService.getCurrentUser();
    localStorage.setItem('currentId', this.currentUser.id.toString());
  }
}
