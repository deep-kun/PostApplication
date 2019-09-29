import { Component, OnInit } from '@angular/core';
import {User} from '../model/user';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  model = new User('', '');

  constructor() { }

  ngOnInit() {
  }

}
