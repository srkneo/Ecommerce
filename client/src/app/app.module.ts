import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'; // Importing BrowserAnimationsModule is optional, depending on your needs
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [AppComponent    
  ],
  imports: [BrowserModule, 
    AppRoutingModule, 
    BrowserAnimationsModule,NavbarComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}  
