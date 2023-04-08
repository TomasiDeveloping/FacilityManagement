import {inject, Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Assignment} from "../models/assignment.model";

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {

private readonly _serviceUrl = environment.apiBaseUrl + '/Assignments/';
private readonly _httpClient = inject(HttpClient);

getUserAssignments(userId: string): Observable<Assignment[]> {
  return this._httpClient.get<Assignment[]>(this._serviceUrl + 'Users/' + userId);
}

updateAssignment(assignmentId: string, assignment: Assignment): Observable<Assignment> {
  return this._httpClient.put<Assignment>(this._serviceUrl + assignmentId, assignment);
}

}
