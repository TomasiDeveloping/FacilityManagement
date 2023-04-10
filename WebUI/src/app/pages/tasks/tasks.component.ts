import {Component, inject, OnInit} from '@angular/core';
import {JwtHelperService} from "@auth0/angular-jwt";
import {AssignmentService} from "../../services/assignment.service";
import {Assignment} from "../../models/assignment.model";
import {ToastrService} from "ngx-toastr";
import Swal from "sweetalert2";

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  public userAssignments: Assignment[] = [];

  private readonly _jwtService = inject(JwtHelperService);
  private readonly _assignmentService = inject(AssignmentService);
  private readonly _toastr = inject(ToastrService);

  ngOnInit() {
    const token = this._jwtService.tokenGetter().toString();
    const userId = this._jwtService.decodeToken(token).userId;
    this.getUserAssignments(userId);
  }

  getUserAssignments(userId: string) {
    this._assignmentService.getUserAssignments(userId).subscribe({
      next: ((response) => {
        if (response) {
          this.userAssignments = response;
        }
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Something went wrong', 'Get User Assignment');
      }
    });
  }


  onCloseAssignment(assignment: Assignment) {
    Swal.fire({
      title: 'Aufgabe Abschliessen?',
      text: `Soll die Aufgabe ${assignment.name} Abgeschlossen werden ?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Ja, ist erledigt'
    }).then((result) => {
      if (result.isConfirmed) {
        assignment.isCompleted = true;
        this._assignmentService.updateAssignment(assignment.id!, assignment).subscribe({
          next: ((response) => {
            if (response) {
              this._toastr.success('Aufgabe erfolgreich abgeschlossen', 'Aufgabe Abschliessen');
              this.getUserAssignments(assignment.userId);
            }
          }),
          error: _ => {
            this._toastr.error('Aufgabe konnte nicht abgeschlossen werden', 'Aufgabe Abschliessen');
          }
        });
      }
    })
  }
}
