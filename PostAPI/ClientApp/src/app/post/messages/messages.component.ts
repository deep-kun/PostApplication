import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MessageBody } from '../../model/messageBody';
import { Message } from '../../model/messsage';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html'
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  messageBody: MessageBody;

  constructor(private http: HttpClient, private router: Router) {
    this.initData();
  }
  ngOnInit() {
  }

  initData() {
    this.http.get<Message[]>(environment.apiUrl + 'api/Mail/').subscribe(result => {
      this.messages = result;
      console.log(this.messages);
    }, error => console.error(error));
  }

  newMsg() {
    this.router.navigate(['/newmsg']);
  }

  delete(msg: Message) {
    console.log('delete');
    this.messageBody = null;
    this.http.delete<any>(environment.apiUrl + 'api/Mail/' + msg.messageId).subscribe(res => {
      this.initData();
    }, error => console.error(error));
    }

  onSelect(msg: Message): void {
    console.log('on select');
    this.http.get<MessageBody>(environment.apiUrl + 'api/Mail/' + msg.messageId).subscribe(res => {
      this.messageBody = res;
      this.initData();
    }, error => console.error(error));
  }
}
