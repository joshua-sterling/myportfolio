import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RowingDataComponent } from './rowing-data/rowing-data.component';
import { AboutMeComponent } from './about-me/about-me.component';


import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    RowingDataComponent,
    AboutMeComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, ReactiveFormsModule, FormsModule,
    BrowserAnimationsModule, ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-center-center',
      preventDuplicates: true,
})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
