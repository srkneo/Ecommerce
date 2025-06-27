import { inject, Injectable } from '@angular/core';
import { HttpInterceptorFn,HttpRequest,HttpHandler,HttpEvent,HttpInterceptor } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router:Router) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
     catchError((error) => {              
      const isApiUrl = req.url.includes('/Basket') || req.url.includes('/api');
        if (error.status === 401 || error.status === 403) {
          // Redirect to login page or show an error message
          this.router.navigate(['/un-authenticated']);
        } else if (error.status === 404 && !isApiUrl) {
          // Redirect to not found page
          this.router.navigate(['/not-found']);
        } else if (error.status === 500) {
          // Redirect to server error page or show an error message
          this.router.navigate(['/server-error']);
        }
      
       return throwError(()=>new Error(error));
    })  
  )
 }
}
