import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { LoginService } from '../_services/login/login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private loginService: LoginService, 
              private router: Router, private toastr: ToastrService){}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) : boolean{
    let accessToken = localStorage.getItem('accessToken');
    if(accessToken != null) {
      let role = next.data['permittedRole'] as Array<string>;
      if(role) {
        if(this.loginService.roleMatch(role)) return true;
        else {
          this.router.navigate(['/']);
          this.toastr.error('Acesso negado!');
          return false;
        }
      }
      return true;
    }
    
    this.router.navigate(['/login']);
    return false;
  }
  
}
