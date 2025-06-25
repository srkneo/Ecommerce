import { Injectable } from '@angular/core';
import { HttpRequest,HttpHandler,HttpEvent,HttpInterceptor } from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { LoadingService } from '../services/loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private loadingService: LoadingService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loadingService.loading(); // Show loading spinner
    return next.handle(req).pipe(
      delay(500),
      finalize(()=>{
        this.loadingService.idle();
      })
    );
 }
}
