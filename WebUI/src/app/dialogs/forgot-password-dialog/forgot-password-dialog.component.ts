import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-forgot-password-dialog',
  templateUrl: './forgot-password-dialog.component.html',
  styleUrls: ['./forgot-password-dialog.component.css']
})
export class ForgotPasswordDialogComponent {

  private readonly _dialogRef = inject(MatDialogRef<ForgotPasswordDialogComponent>);

  public emailForm: FormGroup = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email])
  });

  get email(){
    return this.emailForm.get('email')!;
  }
  onClose(data: string | null):void {
    this._dialogRef.close(data);
  }

  onSubmit() {
    this._dialogRef.close(this.email.value);
  }
}
