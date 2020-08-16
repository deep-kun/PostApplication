import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RolePermissonService {

  constructor() { }
   private readonly adminRoleId:number = 2;
  
  public canSeeUserList(roleId: number): boolean{
    console.log(`canSeeUserList returns = ${roleId}`);
    return roleId >= this.adminRoleId;
  }
}
