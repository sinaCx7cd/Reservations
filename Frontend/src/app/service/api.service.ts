import { formatDate } from '@angular/common';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddReservationModel } from '../models/AddReservationModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private headers: HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
  constructor(private http: HttpClient) {

  }

  getReservations(date: string) : Observable<HttpResponse<Array<number>>> {
    return this.http.get<Array<number>>('/api/reservation/get?date=' + date, { observe: "response" });
  }

  addReservation(addReservationModel: AddReservationModel) {
    return this.http
      .post('/api/reservation/add', addReservationModel, { observe: "response", responseType: "json" });
  }
}
