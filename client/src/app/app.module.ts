import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'; // Importing BrowserAnimationsModule is optional, depending on your needs
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, 
    AppRoutingModule, 
    BrowserAnimationsModule,
    HttpClientModule,CoreModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}  
