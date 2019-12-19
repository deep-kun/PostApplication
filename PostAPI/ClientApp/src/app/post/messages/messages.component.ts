import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MessageBody } from '../../model/messageBody';
import { Message } from '../../model/messsage';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html'
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  messageBody: MessageBody;

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

  onSelect(msg: Message): void {
    this.http.get<MessageBody>(this.baseUrl + 'api/Mail/' + msg.MessageId).subscribe(res => {
      this.messageBody = res;
    }, error => console.error(error));
  }
}
