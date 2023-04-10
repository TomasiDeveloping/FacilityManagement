import {Component, inject, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ResetPassword} from "../../models/resetPassword.model";
import {AuthenticationService} from "../../services/authentication.service";
import {ToastrService} from "ngx-toastr";
import {PasswordValidators} from "../../helpers/password-validators";

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {

  public isInputText: boolean = false;
  public resetForm: FormGroup = new FormGroup({
    confirmPassword: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', Validators.compose([
      Validators.required,
      PasswordValidators.patternValidator(new RegExp("(?=.*[0-9])"), {hasNumber: true}),
      PasswordValidators.patternValidator(new RegExp("(?=.*[A-Z])"), {hasCapitalCase: true}),
      PasswordValidators.patternValidator(new RegExp("(?=.*[a-z])"), {hasSmallCase: true}),
      PasswordValidators.patternValidator(new RegExp("(?=.*[$@^!%*?&+])"), {hasSpecialCharacters: true}),
      Validators.minLength(8)
    ]))
  }, {
    validators: PasswordValidators.passwordMatch('password', 'confirmPassword')
  });

  private _token: string | undefined;
  private _email: string | undefined;

  private readonly _route = inject(ActivatedRoute);
  private readonly _authService = inject(AuthenticationService);
  private readonly _toastr = inject(ToastrService);
  private readonly _router = inject(Router);

  get password() {
    return this.resetForm.get('password')!;
  }

  get confirmPassword() {
    return this.resetForm.get('confirmPassword')!;
  }

  ngOnInit() {

    this._token = this._route.snapshot.queryParams['token'];
    this._email = this._route.snapshot.queryParams['email'];
  }

  onSubmit(): void {
    if (this.resetForm.invalid) {
      return;
    }
    if (!this._email || !this._token) {
      this._toastr.error('Es gibt ein Fehler. Bitte Password erneut zurücksetzen', 'Fehler');
      return;
    }
    const resetPassword: ResetPassword = {
      password: this.password.value,
      email: this._email,
      token: this._token
    };

    this._authService.resetPassword(resetPassword).subscribe({
      next: ((response) => {
        if (response.isSuccessful) {
          this._router.navigate(['/login']).then(() => {
            this._toastr.info('Passwort erfolgreich zurückgesetzt', 'Passwort zurücksetzen');
          })
        }
      }),
      error: error => {
        this._toastr.error(error.error.errors[0] ?? 'Something went wrong', 'Passwort zurücksetzen');
      }
    });
  }

}
