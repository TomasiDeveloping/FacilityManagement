import {inject, Injectable} from '@angular/core';
import {Router, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthenticationService} from "../services/authentication.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {

  private readonly _authService = inject(AuthenticationService);
  private readonly _router = inject(Router);

  canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this._authService.isUserAuthenticated()) {
      return true;
    }
    this._router.navigate(['/login']).then();
    return false;
  }

}
