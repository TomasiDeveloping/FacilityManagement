import {Component, inject, OnInit} from '@angular/core';
import {AuthenticationService} from "../services/authentication.service";

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit{

  public readonly _authService = inject(AuthenticationService);
  public isUserLoggedIn: boolean = false;
  ngOnInit(): void {
    this.isUserLoggedIn = this._authService.isUserAuthenticated();
    this._authService.authChanged.subscribe({
      next: ((isAuthenticated) => {
        this.isUserLoggedIn = isAuthenticated;
      })
    });
  }

  onLogout() {
    this._authService.logout();
  }
}
