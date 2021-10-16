import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
  users: any;
  constructor(private http:HttpClient){}

  //onInit comes after the constructor in the angular lifecycle
  ngOnInit(){
    this.getUsers();
  }
  //http requests to an API is naturally an asynchronous request
  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      console.log(response);
      this.users = response;
    },error => {
      console.log(error);
    });
  }

  login(){
    console.log("Pedro is handsome!");
  }
}
