import { Component, OnInit, Input } from '@angular/core';
import { MessageBody } from '../../model/messageBody';

@Component({
  selector: 'app-message-details',
  templateUrl: './message-details.component.html',
  styleUrls: ['./message-details.component.css']
})
export class MessageDetailsComponent implements OnInit {
  @Input() mes: MessageBody;

  constructor() { }

  ngOnInit() {
  }

}
