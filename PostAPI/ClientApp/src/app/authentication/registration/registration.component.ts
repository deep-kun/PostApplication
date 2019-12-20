import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  model = new User('', '', '');
  submitted = false;
  error = '';

  ngOnInit() {
  }

  onSubmit() {
    console.log('pressed');
    this.error = '';

    this.authenticationService.register(this.model.login, this.model.password, this.model.name)
  .subscribe(
      data => {
          this.router.navigate([`messages`]);
      },
      error => {
          this.error = error;
      });
    }

}
