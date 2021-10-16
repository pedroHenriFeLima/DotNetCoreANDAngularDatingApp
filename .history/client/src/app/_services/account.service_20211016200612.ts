import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User|null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http:HttpClient) { }

  login(model:any) {
    //a user DTO is sent back from server
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response:any) => {
        const user = response;
        const userObject:User = user;
        if(user) {
          localStorage.setItem('user',JSON.stringify(userObject));
          this.currentUserSource.next(userObject);
        }
      })
    )
  }

  setCurrentUser(user:User) {
    this.currentUserSource.next(user);
  }

  logOut(){
    this.currentUserSource.next(null);
    localStorage.removeItem('user');
  }
}
