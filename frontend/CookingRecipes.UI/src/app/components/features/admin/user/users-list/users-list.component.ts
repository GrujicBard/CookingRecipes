import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  isDisplayed = true;
  buttonClicks: string[] = [];
  constructor() { }

  ngOnInit() {
  }
  onToggleDetails() {
    this.isDisplayed = !this.isDisplayed
    this.buttonClicks.push(new Date().toString());
  }

}
