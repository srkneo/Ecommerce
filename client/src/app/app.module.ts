import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'; // Importing BrowserAnimationsModule is optional, depending on your needs
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule, 
    AppRoutingModule, 
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule],
  providers: [
    {provide:HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}, // Registering the error interceptor
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true} // Registering the loading interceptor
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}  
