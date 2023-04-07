import {Component, inject} from '@angular/core';
import {AuthenticationService} from "../../services/authentication.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  private readonly _authService = inject(AuthenticationService);
  onSubmit() {
    this._authService.login();
  }
}
