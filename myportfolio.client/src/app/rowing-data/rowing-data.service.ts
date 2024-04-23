import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class RowingEventService {

  constructor(private http: HttpClient) { }

  getRowingEvents(skip: number, take: number, sortColumn: string, sortAscending: boolean) {
    return this.http.get(`${environment.apiUrl}/RowingEvent?skip=${skip}&take=${take}&sortColumn=${sortColumn}&sortAscending=${sortAscending}`);
  }

  addRowingEvent(rowingEvent: any) {
    return this.http.post(`${environment.apiUrl}/RowingEvent`, rowingEvent, { observe: 'response' });
  }

  getChartData() {
    return this.http.get(`${environment.apiUrl}/RowingEvent/summary`);
  }
}
