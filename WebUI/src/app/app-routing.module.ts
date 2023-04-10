import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {LoginComponent} from "./pages/login/login.component";
import {HomeComponent} from "./pages/home/home.component";
import {AuthGuard} from "./guards/auth.guard";
import {DashboardComponent} from "./pages/dashboard/dashboard.component";
import {TasksComponent} from "./pages/tasks/tasks.component";
import {AppointmentsComponent} from "./pages/appointments/appointments.component";
import {AdminComponent} from "./pages/admin/admin.component";
import {PasswordResetComponent} from "./pages/password-reset/password-reset.component";

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]},
  {path: 'aufgaben', component: TasksComponent, canActivate: [AuthGuard]},
  {path: 'termine', component: AppointmentsComponent, canActivate: [AuthGuard]},
  {path: 'admin', component: AdminComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'password-reset', component: PasswordResetComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full'}
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
