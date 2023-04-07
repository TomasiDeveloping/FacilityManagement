import {inject, Injectable} from '@angular/core';
import {Subject} from "rxjs";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {LoginRequest} from "../models/loginRequest.model";
import {AuthResponse} from "../models/authResponse.model";
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private _authChangeSubscription = new Subject<boolean>();
  public authChanged = this._authChangeSubscription.asObservable();

  private readonly _serviceUrl = environment.apiBaseUrl + '/accounts/';
  private readonly _jwtService = inject(JwtHelperService);
  private readonly _router = inject(Router);
  private readonly _httpClient = inject(HttpClient);
  private readonly _toastr = inject(ToastrService);

  constructor() {
  }

  public sendAuthStateChangeNotification(isAuthenticated: boolean): void {
    this._authChangeSubscription.next(isAuthenticated);
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
}
