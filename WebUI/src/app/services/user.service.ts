import {inject, Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {map, Observable, Subject} from "rxjs";
import {User} from "../models/user.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private _currentUserSubject = new Subject<User | null>();
  private readonly _serviceUrl = environment.apiBaseUrl + '/users/';
  private readonly _httpClient = inject(HttpClient);


  getUsers(): Observable<User[]> {
    return this._httpClient.get<User[]>(this._serviceUrl);
  }

  getUserById(userId: string): Observable<User> {
    return this._httpClient.get<User>(this._serviceUrl + userId).pipe(map(res => {
      this._currentUserSubject.next(res);
      return res
    }));
  }

  getCurrentUser() {
    return this._currentUserSubject.asObservable();
  }
}
