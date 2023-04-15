import {inject, Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Maintenance} from "../models/maintenance.model";

@Injectable({
  providedIn: 'root'
})
export class MaintenanceService {

  private readonly _serviceUrl = environment.apiBaseUrl + '/Maintenances/';
  private readonly _httpClient = inject(HttpClient);

  getMaintenanceById(maintenanceId: string): Observable<Maintenance> {
    return this._httpClient.get<Maintenance>(this._serviceUrl + maintenanceId);
  }

  getMaintenances(): Observable<Maintenance[]> {
    return this._httpClient.get<Maintenance[]>(this._serviceUrl);
  }

  getFilteredMaintenancesByMonth(filterMonth: number):Observable<Maintenance[]> {
    const param = new HttpParams().set('month', filterMonth);
    return this._httpClient.get<Maintenance[]>(this._serviceUrl + 'GetMaintenancesByMonth', {params: param});
  }

  closeMaintenance(maintenance: Maintenance): Observable<boolean> {
    return this._httpClient.put<boolean>(this._serviceUrl + 'CloseMaintenance/' + maintenance.id, maintenance);
  }

  createMaintenance(name: string, interval: number): Observable<Maintenance> {
    return this._httpClient.post<Maintenance>(this._serviceUrl, {name, interval});
  }

  deleteMaintenance(maintenanceId: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(this._serviceUrl + maintenanceId);
  }
}
