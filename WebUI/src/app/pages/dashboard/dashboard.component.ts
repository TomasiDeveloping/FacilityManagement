import {Component, inject, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {User} from "../../models/user.model";
import {ToastrService} from "ngx-toastr";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Assignment} from "../../models/assignment.model";
import {AssignmentService} from "../../services/assignment.service";
import {Maintenance} from "../../models/maintenance.model";
import {MaintenanceService} from "../../services/maintenance.service";
import {MatDialog} from "@angular/material/dialog";
import {MaintenanceDescriptionComponent} from "../../dialogs/maintenance-description/maintenance-description.component";
import Swal from "sweetalert2";
import {Appointment} from "../../models/appointment.model";
import {AppointmentService} from "../../services/appointment.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  private readonly _userService = inject(UserService);
  private readonly _toastr = inject(ToastrService);
  private readonly _jwtHelper = inject(JwtHelperService);
  private readonly _assignmentService = inject(AssignmentService);
  public readonly _maintenanceService = inject(MaintenanceService);
  private readonly _appointmentService = inject(AppointmentService);
  private readonly _dialog = inject(MatDialog);

  public currentUser: User | undefined;
  public userAssignments: Assignment[] = [];
  public maintenances: Maintenance[] = [];
  public userAppointments: Appointment[] = [];
  public currentMonth: number = new Date().getMonth() + 1;
  public currentYear: number = new Date().getFullYear();

  ngOnInit() {
    const token = this._jwtHelper.tokenGetter().toString();
    const userId = this._jwtHelper.decodeToken(token).userId;
    this.getUser(userId);
    this.getGreeting();
    this.getUserAssignments(userId);
    this.getMaintenances();
    this.getUserAppointments(userId);
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

  getMaintenances() {
    this._maintenanceService.getFilteredMaintenancesByMonth(this.currentMonth).subscribe({
      next: ((response) => {
        this.maintenances = response;
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Something went wrong', 'Get Maintenances');
      }
    });
  }

  getUserAppointments(userId: string) {
    this._appointmentService.getUserAppointments(userId).subscribe({
      next: ((response) => {
        if (response) {
          this.userAppointments = response;
        }
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Something went wrong', 'Get User Appointments');
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

  isOlderThanCurrentMonth(date: Date): boolean {
    const dt = new Date(date);
    return dt.getMonth() + 1 < this.currentMonth;
  }

  onMaintenanceDescription(description: string) {
    this._dialog.open(MaintenanceDescriptionComponent, {
      data: description
    })
  }

  onCloseMaintenance(maintenance: Maintenance) {
    Swal.fire({
      title: 'Wartungsarbeit Abschliessen?',
      text: `Soll die Wartungsarbeit ${maintenance.name} Abgeschlossen werden ?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Ja, abschliessen'
    }).then((result) => {
      if (result.isConfirmed) {
        this._maintenanceService.closeMaintenance(maintenance).subscribe({
          next: ((response) => {
            if (response) {
              this._toastr.success('Wartungsarbeit erfolgreich abgeschlossen', 'Wartungsarbeit Abschliessen');
              this.getMaintenances();
            }
          }),
          error: _ => {
            this._toastr.error('Wartungsarbeit konnte nicht abgeschlossen werden', 'Wartungsarbeit Abschliessen');
          }
        });
      }
    })
  }
}
