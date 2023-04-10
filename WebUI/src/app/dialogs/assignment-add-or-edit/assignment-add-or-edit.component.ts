import {Component, inject, Inject, OnInit} from '@angular/core';
import {Assignment} from "../../models/assignment.model";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../services/user.service";
import {User} from "../../models/user.model";
import {AssignmentService} from "../../services/assignment.service";
import {Observable} from "rxjs";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-assignment-add-or-edit',
  templateUrl: './assignment-add-or-edit.component.html',
  styleUrls: ['./assignment-add-or-edit.component.css']
})
export class AssignmentAddOrEditComponent implements OnInit{

  public assignment: Assignment;
  public assignmentForm!: FormGroup;
  public users: User[] = [];

  private readonly _dialogRef = inject(MatDialogRef<AssignmentAddOrEditComponent>);
  private readonly _userService = inject(UserService);
  private readonly _assignmentService = inject(AssignmentService);
  private readonly _toastr = inject(ToastrService);

  constructor(@Inject(MAT_DIALOG_DATA) public data: Assignment) {
    this.assignment = data;
  }

  get name() {
    return this.assignmentForm.get('name')!;
  }

  get userControl() {
    return this.assignmentForm.get('userId')!;
  }

  ngOnInit() {
    this.getUsers();
  }

  createAssignmentForm() {
    this.assignmentForm = new FormGroup({
      id: new FormControl<string | null>(this.assignment.id ?? null),
      userId: new FormControl<string>(this.assignment.userId ?? '', [Validators.required]),
      name: new FormControl<string>(this.assignment.name ?? '', [Validators.required, Validators.maxLength(200)]),
      description: new FormControl<string>(this.assignment.description ?? ''),
      isCompleted: new FormControl<boolean>(this.assignment.isCompleted ?? false),
      createDate: new FormControl(this.assignment.createDate ?? new Date())
    });
  }

  getUsers() {
    this._userService.getUsers().subscribe({
      next: ((response) => {
        if (response) {
          this.users = response;
          this.createAssignmentForm();
        }
      }),
      error: error => {
        console.log(error);
      }
    });
  }

  onClose(reload: boolean) {
    this._dialogRef.close(reload)
  }

  onSubmit() {
    if (this.assignmentForm.invalid) {
      return;
    }
    const assignment = this.assignmentForm.value as Assignment;
    let command: Observable<Assignment>;
    let successMessage = '';
    if (assignment.id) {
      command = this._assignmentService.updateAssignment(assignment.id!, assignment);
      successMessage = `Aufgabe ${assignment.name} erfolgreich bearbeitet.`;
    } else {
      command = this._assignmentService.createAssignment(assignment);
      successMessage = `Aufgabe ${assignment.name} erstellt.`
    }
    command.subscribe({
      next: ((response) => {
        if (response) {
          this._toastr.success(successMessage, 'Aufgaben');
          this.onClose(true);
        }
      })
    });
  }

}
