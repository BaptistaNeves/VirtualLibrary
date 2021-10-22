import { Component, OnInit } from '@angular/core';
import { LoginService } from '../_services/login/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './layout.component.html'
})
export class LayoutComponent implements OnInit {

  constructor(public loginService: LoginService) { }

  ngOnInit(): void {
  }

  logOut() {
    this.loginService.logout();
  }
}
