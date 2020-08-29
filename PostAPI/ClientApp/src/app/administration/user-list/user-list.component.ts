import { Component, OnInit } from '@angular/core';
import { User } from '../../model/user'
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private http: HttpClient) { }
  users: User[];

  ngOnInit(): void {
    this.initData();
  }

  initData() {
    this.http.get<User[]>(environment.apiUrl + 'api/user/').subscribe(result => {
      this.users = result;
      console.log(this.users);
    }, error => console.error(error));
  }

}
