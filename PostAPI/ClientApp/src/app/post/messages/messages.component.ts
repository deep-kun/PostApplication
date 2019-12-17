import { Component, OnInit, Inject } from '@angular/core';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html'
})
export class MessagesComponent implements OnInit {
  messages: Message[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.initData();
  }
  ngOnInit() {
  }

  initData() {
    this.http.get<Message[]>(this.baseUrl + 'api/Mail/').subscribe(result => {
      this.messages = result;
      console.log(this.messages);
    }, error => console.error(error));
  }

}
