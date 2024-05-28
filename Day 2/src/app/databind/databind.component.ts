import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-databind',
  templateUrl: './databind.component.html',
  styleUrls: ['./databind.component.css']
})
export class DatabindComponent implements OnInit {
  @Input() myName: string;

  username  = '';
  message : string = '';
  msg: string = "Hello, world!";
  isdisable = true;
  
  constructor() { 
    this.myName = '';
    this.username = 'Ruchit';
  }

  ngOnInit(): void {
    alert(this.myName); // This will alert the value of myName passed from the parent
  }

  onButtonClick() {
    this.message = `Button clicked! Username: ${this.username}`;
  }
}
