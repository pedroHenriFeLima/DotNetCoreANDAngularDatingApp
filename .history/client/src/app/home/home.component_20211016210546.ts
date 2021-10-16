import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  constructor(private http:HttpClient) { }
  users:any;
  ngOnInit(): void {
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe(user => this.users = user);
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }
}
