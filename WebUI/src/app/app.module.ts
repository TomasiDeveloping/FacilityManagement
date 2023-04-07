import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavigationComponent } from './navigation/navigation.component';
import { LoginComponent } from './pages/login/login.component';
import {AppRoutingModule} from "./app-routing.module";
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import { HomeComponent } from './pages/home/home.component';
import {retry} from "rxjs";
import {JwtModule} from "@auth0/angular-jwt";
import {FormsModule} from "@angular/forms";
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { AppointmentsComponent } from './pages/appointments/appointments.component';
import { AdminComponent } from './pages/admin/admin.component';

export function tokengetter() {
  return localStorage.getItem('FacilityToken');
}
@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    LoginComponent,
    HomeComponent,
    DashboardComponent,
    TasksComponent,
    AppointmentsComponent,
    AdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterOutlet,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokengetter
      }
    }),
    FormsModule,
    RouterLink,
    RouterLinkActive
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
