import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

@Component({
  selector: 'app-maintenance-description',
  templateUrl: './maintenance-description.component.html',
  styleUrls: ['./maintenance-description.component.css']
})
export class MaintenanceDescriptionComponent {
  maintenanceDescription: string;

  constructor(@Inject(MAT_DIALOG_DATA) public description: string) {
    this.maintenanceDescription = description;
  }
}
