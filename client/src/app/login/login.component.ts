import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../_services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  modelStateErrors: any;

  constructor(private formBuilder: FormBuilder, 
              private loginService: LoginService, private route: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  get f() {return this.loginForm.controls;}

  login() {
    this.loginService.login(this.loginForm.value).subscribe(response => {
        this.route.navigate(['/']);
    }, error => {
      this.modelStateErrors = error;
    })
  }

}
