import {Component, inject, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../models/user.model";
import {ToastrService} from "ngx-toastr";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Assignment} from "../../models/assignment.model";
import {AssignmentService} from "../../services/assignment.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public currentUser: User | undefined;
  public userAssignments: Assignment[] = [];
  private readonly _userService = inject(UserService);
  private readonly _toastr = inject(ToastrService);
  private readonly _jwtHelper = inject(JwtHelperService);
  private readonly _assignmentService = inject(AssignmentService);

  ngOnInit() {
    const token = this._jwtHelper.tokenGetter().toString();
    const userId = this._jwtHelper.decodeToken(token).userId;
    this.getUser(userId);
    this.getGreeting();
    this.getUserAssignments(userId);
  }

  getUser(userId: string) {
    this._userService.getUserById(userId).subscribe({
      next: ((response) => {
        this.currentUser = response;
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Something went wrong', 'Get User');
      }
    });
  }

  getUserAssignments(userId: string) {
    this._assignmentService.getUserAssignments(userId).subscribe({
      next: ((response) => {
        if (response) {
          this.userAssignments = response;
        }
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Something went wrong', 'Get Assignments');
      }
    });
  }

  getGreeting(): string {
    const hour = new Date().getHours();
    if (hour >= 5 && hour <= 11) {
      return 'Guten Morgen,';
    }
    if (hour >= 11 && hour <= 18) {
      return 'Guten Tag,';
    }
    if (hour >= 18 && hour <= 22) {
      return 'Guten Abend,';
    }
    return 'Hy,'
  }
}
