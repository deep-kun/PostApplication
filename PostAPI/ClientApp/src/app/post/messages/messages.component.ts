import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MessageBody } from '../../model/messageBody';
import { Message } from '../../model/messsage';
import { Router } from '@angular/router';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html'
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  messageBody: MessageBody;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
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

  newMsg() {
    this.router.navigate(['/newmsg']);
  }

  onSelect(msg: Message): void {
    console.log('on select');
    console.log(msg);
    console.log(this.baseUrl + 'api/Mail/' + msg.messageId);
    this.http.get<MessageBody>(this.baseUrl + 'api/Mail/' + msg.messageId).subscribe(res => {
      this.messageBody = res;
      this.initData();
    }, error => console.error(error));
  }
}
