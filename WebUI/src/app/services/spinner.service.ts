import {inject, Injectable} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  private busyRequestCounter = 0;
  private readonly _spinnerService = inject(NgxSpinnerService);

  busy(): void {
    this.busyRequestCounter++;
    this._spinnerService.show(undefined, {
      type: 'ball-spin-clockwise',
      bdColor: 'rgba(0, 0, 0, 0.8)',
      color: '#593196'
    }).then();
  }

  idle(): void {
    this.busyRequestCounter--;
    if (this.busyRequestCounter <= 0) {
      this.busyRequestCounter = 0;
      this._spinnerService.hide().then();
    }
  }
}
