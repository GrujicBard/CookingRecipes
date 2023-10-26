import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import User from 'src/app/models/user';
import { NotificationService } from 'src/app/services/core/notifications/notification.service';
import { UserService } from 'src/app/services/user/user.service';
import { matchValidator } from 'src/helpers/matching.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerUser: User = {
    userName: "",
    email: "",
    password: "",
  }
  hide = true;
  registerForm = new FormGroup({
    userName: new FormControl("",
      [
        Validators.required,
        Validators.minLength(4),
        Validators.pattern("^[a-zA-Z0-9]*$"),
      ]),
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("",
      [
        Validators.required,
        //Validators.minLength(8),
        //Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])/),
        matchValidator("confirmPass", true),
      ]),
    confirmPass: new FormControl("", [
      Validators.required,
      matchValidator("password"),
    ]),
  });
  constructor(
    private _userService: UserService,
    private _notificationService: NotificationService,
    private _router: Router,
  ) {
  }
  ngOnInit() {
  }

  register(user: User) {
    if (this.registerForm.status === "VALID") {
      this._userService.register(user).subscribe({
        next: (res) => {
          this._notificationService.openSnackBar(res, "Done");
          this._router.navigate(["login"]);
        },
        error: (err) => {
          console.log(err);
        }
      })
    }
  }

  getUsernameErrorMessage() {
    var username = this.registerForm.controls.userName;
    if (username.hasError("required")) {
      return "Username is required";
    }
    else if (username.hasError("minlength")) {
      return "Min length is 4 characters"
    }
    else {
      return username.hasError("pattern") ? "Can only contain numbers and letters" : "";
    }
  }

  getEmailErrorMessage() {
    var email = this.registerForm.controls.email;
    if (email.hasError("required")) {
      return "E-mail is required";
    }
    else {
      return email.hasError("email") ? "E-mail is invalid" : "";
    }
  }

  getPassErrorMessage() {
    var pass = this.registerForm.controls.password;
    if (pass.hasError("required")) {
      return "Password is required";
    }
    else if (pass.hasError("minlength")) {
      return "Min length is 8 characters"
    }
    else {
      return pass.hasError("pattern") ? "Must contain at least 1 lowercase letter, 1 uppercase and 1 number" : "";
    }
  }

  getPassConfirmErrorMessage() {
    var confirmPass = this.registerForm.controls.confirmPass;
    if (confirmPass.hasError("required")) {
      return "Password confirmation is required";
    } else if (confirmPass.hasError("matching")) {
      return "Password must match confirmation password";
    }
    return "";
  }
}
