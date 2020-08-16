import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { RolePermissonService } from '../services/role-permisson.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  userName = ''; 
  isExpanded = false;
  showLogin = true;
  canManageUsers = false;

  constructor(private authenticationService: AuthenticationService, private router: Router, permission: RolePermissonService) {
    this.authenticationService.currentUserSubject.subscribe(data => {
      if(data != null){
        this.showLogin = false;
        this.canManageUsers = permission.canSeeUserList(data.role);
        this.userName = data.name;
      }
      else
      {
        this.showLogin = true;
        this.canManageUsers = false;
        this.userName = '';
      } 
  });
  }

  logout() {  
    this.authenticationService.logout();
    this.router.navigate(['']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
