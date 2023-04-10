import {inject, Injectable} from '@angular/core';
import {Observable, Subject} from "rxjs";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {LoginRequest} from "../models/loginRequest.model";
import {AuthResponse} from "../models/authResponse.model";
import {ToastrService} from "ngx-toastr";
import {ForgotPassword} from "../models/forgotPassword.model";
import {ResetPassword} from "../models/resetPassword.model";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private _authChangeSubscription$ = new Subject<boolean>();
  public authChanged = this._authChangeSubscription$.asObservable();

  private readonly _serviceUrl = environment.apiBaseUrl + '/accounts/';
  private readonly _jwtService = inject(JwtHelperService);
  private readonly _router = inject(Router);
  private readonly _httpClient = inject(HttpClient);
  private readonly _toastr = inject(ToastrService);

  constructor() {
  }

  public sendAuthStateChangeNotification(isAuthenticated: boolean): void {
    this._authChangeSubscription$.next(isAuthenticated);
  }

  public isUserAuthenticated(): boolean {
    const token = localStorage.getItem('FacilityToken');
    if (token) {
      return !this._jwtService.isTokenExpired(token);
    }
    return false;
  }

  public login(loginRequest: LoginRequest): void {
    this._httpClient.post<AuthResponse>(this._serviceUrl + 'login', loginRequest).subscribe({
      next: ((response) => {
        if (response.isSuccessful) {
          localStorage.setItem('FacilityToken', response.token);
          this.sendAuthStateChangeNotification(response.isSuccessful);
          this._router.navigate(['/dashboard']).then();
        }
      }),
      error: error => {
        this._toastr.error(error.error.errorMessage ?? 'Something went wrong', 'Login');
      }
    });

  }

  public logout(): void {
    this.sendAuthStateChangeNotification(false);
    localStorage.removeItem('FacilityToken');
    this._router.navigate(['']).then();
  }

  public forgotPassword(forgotPassword: ForgotPassword): void {
    this._httpClient.post<{
      isSuccessful: boolean,
      errorMessage: string
    }>(this._serviceUrl + 'forgotPassword', forgotPassword).subscribe({
      next: ((result) => {
        if (result.isSuccessful) {
          this._toastr.info('Der Link wurde gesendet. Bitte überprüfen Sie Ihre E-Mail (Spam), um Ihr Passwort zurückzusetzen.', 'Neuses Passwort');
        }
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Somethine went wrong', 'Neuse Passwort');
      }
    });
  }

  public resetPassword(resetPassword: ResetPassword): Observable<{ isSuccessful: boolean, errorMessage: string }> {
    return this._httpClient.post<{
      isSuccessful: boolean,
      errorMessage: string
    }>(this._serviceUrl + 'resetPassword', resetPassword);
  }
}
