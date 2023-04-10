import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {NavigationComponent} from './navigation/navigation.component';
import {LoginComponent} from './pages/login/login.component';
import {AppRoutingModule} from "./app-routing.module";
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {HomeComponent} from './pages/home/home.component';
import {JwtModule} from "@auth0/angular-jwt";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {DashboardComponent} from './pages/dashboard/dashboard.component';
import {TasksComponent} from './pages/tasks/tasks.component';
import {AppointmentsComponent} from './pages/appointments/appointments.component';
import {AdminComponent} from './pages/admin/admin.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NgxSpinnerModule} from "ngx-spinner";
import {SpinnerInterceptor} from "./interceptors/spinner.interceptor";
import { ForgotPasswordDialogComponent } from './dialogs/forgot-password-dialog/forgot-password-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import { PasswordResetComponent } from './pages/password-reset/password-reset.component';
import { AssignmentAddOrEditComponent } from './dialogs/assignment-add-or-edit/assignment-add-or-edit.component';

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
    AdminComponent,
    ForgotPasswordDialogComponent,
    PasswordResetComponent,
    AssignmentAddOrEditComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterOutlet,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokengetter
      }
    }),
    FormsModule,
    RouterLink,
    RouterLinkActive,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule,
    MatDialogModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: SpinnerInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
