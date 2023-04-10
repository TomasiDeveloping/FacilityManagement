import {Component, inject, OnInit} from '@angular/core';
import {AssignmentService} from "../../services/assignment.service";
import {Assignment} from "../../models/assignment.model";
import {ToastrService} from "ngx-toastr";
import {MatDialog} from "@angular/material/dialog";
import {AssignmentAddOrEditComponent} from "../../dialogs/assignment-add-or-edit/assignment-add-or-edit.component";
import Swal from "sweetalert2";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit{
  public assignments: Assignment[] = [];

  private readonly _assignmentService = inject(AssignmentService);
  private readonly _toastr = inject(ToastrService);
  private readonly _dialog = inject(MatDialog);
  ngOnInit() {
    this.getAssignments();
  }

  getAssignments(){
    this._assignmentService.getAssignments().subscribe({
      next: ((response) => {
        if (response) {
          this.assignments = response;
        }
      }),
      error: error => {
        this._toastr.error(error.error ?? 'Something went wrong', 'Get Assignments');
      }
    });
  }

  onEditAssignment(assignment: Assignment) {
    this.openAssignmentDialog(assignment);
  }

  onAddNewAssignment() {
    const assignment: Assignment = {
      id: undefined,
      isCompleted: false,
      description: '',
      name: '',
      userId: '',
      userFullName: '',
      createDate: new Date()
    }
    this.openAssignmentDialog(assignment);
  }
  openAssignmentDialog(assignment: Assignment) {
    const dialogRef = this._dialog.open(AssignmentAddOrEditComponent, {
      width: '90%',
      height: 'auto',
      disableClose: true,
      autoFocus: 'dialog',
      data: assignment
    });
    dialogRef.afterClosed().subscribe((reload: boolean) => {
      if (reload) {
        this.getAssignments();
      }
    });
  }

  onDeleteAssignment(assignment: Assignment) {
    Swal.fire({
      title: 'Aufgabe löschen?',
      text: `Soll die Aufgabe ${assignment.name} wirklich gelöscht werden ?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Ja, bitte löschen'
    }).then((result) => {
      if (result.isConfirmed) {
        this._assignmentService.deleteAssignment(assignment.id!).subscribe({
          next: ((response) => {
            if (response){
              this._toastr.success('Aufgabe erfolgreich gelöscht', 'Aufgabe löschen');
              this.getAssignments();
            }
          }),
          error: _ => {
            this._toastr.error('Aufgabe konnte nicht gelöscht werden', 'Aufgabe löschen');
          }
        });
      }
    })
  }
}
