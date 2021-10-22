import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error  => {
        if(error) {
          switch (error.status) {
            case 400:
              if(error.error.errors) {
                const modalStateErrors = [];
                for(const key in error.error.errors) {
                  if(error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                console.log(modalStateErrors.flat());
                throw modalStateErrors.flat();
              }
              break;
            case 401:
              this.router.navigate(['/login']);
              break;
            case 403:
              this.router.navigate(['/']);
              this.toastr.error("Acesso negado!");
              break;
            case 404:
              this.router.navigate(['/']);
              this.toastr.error("404 - Solicitação não encotrada!");
              break;
            case 500:
              this.toastr.error("Algum erro inesperado aconteceu, Contacte a área Técnica. Erro-500");
              break;
            default:
              this.toastr.error("Algum erro inesperado aconteceu, Contacte a área Técnica!");
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
