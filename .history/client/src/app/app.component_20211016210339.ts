import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
  users: any;
  constructor(private accountService:AccountService){}

  //onInit comes after the constructor in the angular lifecycle
  ngOnInit(){
    //his.getUsers();
    this.setCurrentUser();
  }

  //lookup at the browser storage and see if there is any user
  setCurrentUser(){
      const user:User = JSON.parse(localStorage.getItem('user'));
      this.accountService.setCurrentUser(user);
  }
  //http requests to an API is naturally an asynchronous request
  /*getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      console.log(response);
      this.users = response;
    },error => {
      console.log(error);
    });
  }*/
}
