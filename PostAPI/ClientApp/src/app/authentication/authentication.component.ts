import { Component, OnInit, Inject } from '@angular/core';
import {User} from '../model/user';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  model = new User('', '');

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

   }

  ngOnInit() {
  }

  // onSumbit() {
  //   this.http.get<WeatherForecast[]>(this.baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
  //     this.forecasts = result;
  //   }, error => console.error(error));
  // }

}
