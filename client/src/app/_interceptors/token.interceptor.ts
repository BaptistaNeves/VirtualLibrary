import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler){
    let token = localStorage.getItem('accessToken');
    let tokenizedReq = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    
    return next.handle(tokenizedReq);
  }
}
