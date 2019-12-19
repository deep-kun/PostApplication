import { Component, OnInit, Inject } from '@angular/core';
import {User} from '../model/user';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';


@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService:  AuthenticationService
  ) {}

  model = new User('', '');
  loading = false;
  submitted = false;
  returnUrl = '/m' ;
  error = '';

  ngOnInit() {
    console.log('on init');
  }

  onSubmit() {
    console.log('pressed');
    this.error = '';

    this.authenticationService.login(this.model.login, this.model.password)
//  .pipe(first())
  .subscribe(
      data => {
          this.router.navigate([this.returnUrl]);
      },
      error => {
          this.error = error;
          this.loading = false;
      });
    }
}
