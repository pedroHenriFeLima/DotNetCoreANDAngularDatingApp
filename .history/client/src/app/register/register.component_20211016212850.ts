import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Input() usersFromHomeComp:any;
  @Output() cancelRegister = new EventEmitter();
  model:any;
  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
  }

  register(){
    console.log(this.model);
    this.accountService.register(this.model);
  }

  cancel(){
    //when cancel button is clicked an event emitter will be fired and it will alert home
    this.cancelRegister.emit(false);
  }
}
