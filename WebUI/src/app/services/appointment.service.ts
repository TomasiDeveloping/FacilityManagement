import {inject, Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Appointment} from "../models/appointment.model";

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  private readonly _serviceUrl = environment.apiBaseUrl + '/Appointments/';
  private readonly _httpClient = inject(HttpClient);

  getAppointments(): Observable<Appointment[]> {
    return this._httpClient.get<Appointment[]>(this._serviceUrl);
  }

  getUserAppointments(userId: string): Observable<Appointment[]> {
    return this._httpClient.get<Appointment[]>(this._serviceUrl + 'Users/' + userId);
  }

  getAppointmentById(appointmentId: string): Observable<Appointment> {
    return this._httpClient.get<Appointment>(this._serviceUrl + appointmentId);
  }

  insertAppointment(appointment: Appointment): Observable<Appointment> {
    return this._httpClient.post<Appointment>(this._serviceUrl, appointment);
  }

  updateAppointment(appointmentId: string, appointment: Appointment): Observable<Appointment> {
    return this._httpClient.put<Appointment>(this._serviceUrl + appointmentId, appointment);
  }

  deleteAppointment(appointmentId: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(this._serviceUrl + appointmentId);
  }
}
