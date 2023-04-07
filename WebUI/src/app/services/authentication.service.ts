import {inject, Injectable} from '@angular/core';
import {Subject} from "rxjs";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private _authChangeSubscription = new Subject<boolean>();
  public authChanged = this._authChangeSubscription.asObservable();

  private readonly _jwtService = inject(JwtHelperService);
  private readonly _router = inject(Router);
  constructor() { }

  public sendAuthStateChangeNotification(isAuthenticated: boolean): void {
    this._authChangeSubscription.next(isAuthenticated);
  }
  public isUserAuthenticated(): boolean{
    const token = localStorage.getItem('FacilityToken');
    if (token) {
      return true;
      //return this._jwtService.isTokenExpired(token);
    }
    return false;
  }

  public login(): void{
    localStorage.setItem('FacilityToken', 'TEST');
    this.sendAuthStateChangeNotification(true);
    this._router.navigate(['/dashboard']).then();
  }

  public logout(): void{
    this.sendAuthStateChangeNotification(false);
    localStorage.removeItem('FacilityToken');
    this._router.navigate(['']).then();
  }
}
