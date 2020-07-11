import { Component, OnInit, Inject } from '@angular/core';
import { SendedMessage } from 'src/app/model/sendedMessage';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-message-create',
  templateUrl: './message-create.component.html',
  styleUrls: ['./message-create.component.css']
})
export class MessageCreateComponent implements OnInit {
  model = new SendedMessage('', '', '');
  error = '';

  constructor( private router: Router, private http: HttpClient) { }

  ngOnInit() {
  }

  onSubmit() {
    this.error = '';

    this.http.post<any>(environment.apiUrl + 'api/Mail/send', this.model).subscribe(res => {
      this.router.navigate(['messages']);
    },
     error => {
       this.error = error;
    });
}
}
