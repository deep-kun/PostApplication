import { Component, OnInit, Inject } from '@angular/core';
import { SendedMessage } from 'src/app/model/sendedMessage';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-message-create',
  templateUrl: './message-create.component.html',
  styleUrls: ['./message-create.component.css']
})
export class MessageCreateComponent implements OnInit {
  model = new SendedMessage('', '', '');
  error = '';

  constructor( private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
  }

  onSubmit() {
    this.error = '';

    this.http.post<any>(this.baseUrl + 'api/Mail/send', this.model).subscribe(res => {
      this.router.navigate(['messages']);
    }, error => {
      this.error = error.error;
      console.error(error);
    });
}
}
