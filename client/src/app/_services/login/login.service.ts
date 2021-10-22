import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/_models/login';
import { Usuario } from 'src/app/_models/produto/usuario';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  baseUrl = environment.apiUrl;
  private userSubject: BehaviorSubject<Usuario>;
  usuario: Observable<Usuario>;

  constructor(private http: HttpClient, private route: Router) {
    this.userSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('usuario')!));
    this.usuario = this.userSubject.asObservable();
   }

  login(login: Login) {
    return this.http.post<any>(this.baseUrl + "login", login).pipe(
      map((usuario: any) => {
        this.setCurrentUser(usuario);
      })
    )
  }

  setCurrentUser(usuario: any) {
    localStorage.setItem('accessToken', usuario.data.accessToken);
    localStorage.setItem('usuario', JSON.stringify(usuario.data));

    this.userSubject.next(usuario.data);
    this.usuario = this.userSubject.asObservable();
  }

  roleMatch(allowedRole: any): boolean{
    let isMatch = false;
    let payLoad = JSON.parse(window.atob(localStorage.getItem('accessToken')!.split('.')[1]));
    let userRole = payLoad.role;

    // allowedRole.forEach(element => {
    //   if(userRole == element) {
    //     isMatch = true;
    //     return true;
    //   }
    // });
    for (let i = 0; i <= allowedRole.length; i++) {
      if(allowedRole[i] == userRole) {
        isMatch = true;
        return true;
      }
    }

    return isMatch;
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('usuario');
    this.route.navigate(['/login']);
  }
}
