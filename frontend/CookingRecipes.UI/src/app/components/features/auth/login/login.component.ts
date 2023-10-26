import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import LoginDto from 'src/app/dtos/login.dto';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginUser: LoginDto;
  hide = true;
  loginForm = new FormGroup({
    email: new FormControl("", [Validators.required]),
    password: new FormControl("",
      [
        Validators.required,
        //Validators.minLength(8),
        //Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])/),
      ]),
  });

  constructor(
    private _userService: UserService,
    private _router: Router,
  ) {
    this.loginUser = {
      email: "",
      password: ""
    }
  }

  ngOnInit() {
  }

  login(user: LoginDto) {
    this._userService.login(user)
    .subscribe({
      next: (res) =>{
        console.log(res);
        localStorage.setItem("authToken", res);
        this._router.navigate(["recipes"]);
      },
      error: (err)=>{
        console.log(err);
      }
    })
  }

}
