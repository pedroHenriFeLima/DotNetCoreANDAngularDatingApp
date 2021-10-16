import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}
  //creating an observable
  currentUser$:Observable<User>;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  login(){
    this.accountService.login(this.model).subscribe(response => {
      console.log(this.model);
      console.log(response);
    },error => {
      console.log(error);
    })
  }

  logOut(){
    this.accountService.logOut();
  }

/*  getCurrentUser(){
    //it is not a good practice to subscribe to an observable and not unsubscribe from it. It can cause a memory leak
    //the async pipe can be used instead of unsubscribe
    //this will be listening whenever a user has already logged in
    //information is being captured from localstorage -> passed to account service
    this.accountService.currentUser$.subscribe(user => {
      this.loggedIn = !!user;
    },error => {
      console.log(error);
    })
  }*/
}
