import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import {AuthenticationService} from "../services/authentication.service";
import {UserService} from "../services/user.service";
import {User} from "../models/user.model";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit, OnDestroy {

  public isUserLoggedIn: boolean = false;
  public currentUser: User | undefined;
  private readonly _userService = inject(UserService);
  private readonly _authService = inject(AuthenticationService);
  private _userChange$: Subscription | undefined;

  ngOnInit(): void {
    this.isUserLoggedIn = this._authService.isUserAuthenticated();
    this._authService.authChanged.subscribe({
      next: ((isAuthenticated) => {
        this.isUserLoggedIn = isAuthenticated;
      })
    });
    this._userChange$ = this._userService.getCurrentUser().subscribe({
      next: ((response) => {
        if (response) {
          this.currentUser = response;
        }
      })
    });
  }

  onLogout() {
    this._authService.logout();
  }

  ngOnDestroy() {
    if (this._userChange$) {
      this._userChange$.unsubscribe();
    }
  }
}
