import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}
  loggedIn:boolean = false;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.model).subscribe(response => {
      console.log(this.model);
      console.log(response);
      this.loggedIn = true;
    },error => {
      console.log(error);
    })
  }

  logOut(){
    this.accountService.logOut();
    this.loggedIn = false;
  }

  getCurrentUser(){
    //this will be listening whenever a user has already logged in
    //information is being captured from localstorage -> passed to account service
    this.accountService.currentUser$.subscribe(user => {
      //!! it turns object into a boolean
      this.loggedIn = !!user;
    })
  }
}
