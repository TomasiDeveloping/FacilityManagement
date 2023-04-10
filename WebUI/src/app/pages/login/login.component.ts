import {Component, inject} from '@angular/core';
import {AuthenticationService} from "../../services/authentication.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {LoginRequest} from "../../models/loginRequest.model";
import {ForgotPasswordDialogComponent} from "../../dialogs/forgot-password-dialog/forgot-password-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {ForgotPassword} from "../../models/forgotPassword.model";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  isInputText: boolean = false;

  private readonly _dialog = inject(MatDialog);

  public loginForm: FormGroup = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required])
  });

  private readonly _authService = inject(AuthenticationService);

  get email() {
    return this.loginForm.get('email')!;
  }

  get password() {
    return this.loginForm.get('password')!;
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return
    }
    const loginRequest: LoginRequest = this.loginForm.value;
    this._authService.login(loginRequest);
  }

  onForgotPassword(): void {
    const dialogRef = this._dialog.open(ForgotPasswordDialogComponent, {
      height: 'auto',
      width: '90%',
      maxWidth: '800px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result){
        const forgotPassword: ForgotPassword = {
          email: result,
          clientUri: 'http://localhost:4200/password-reset'
        };
        this._authService.forgotPassword(forgotPassword);
      }
    });
  }

}
