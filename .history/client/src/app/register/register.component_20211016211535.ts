import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComp:any;
  @Output() cancelRegister = new EventEmitter();
  model:any;
  constructor() { }

  ngOnInit(): void {
  }

  register(){
    console.log(this.model);
  }

  cancel(){
    //when cancel button is clicked an event emitter will be fired and it will alert home
    this.cancelRegister.emit(false);
  }

}
