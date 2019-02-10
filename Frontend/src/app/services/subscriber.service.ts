import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subscriber } from '../Models/Subscriber';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SubscriberService {

  constructor(private http: HttpClient) { }

  getSubscribers(): Observable<Subscriber[]>{
    return this.http.get<Subscriber[]>((environment.subscribersSystemApi + 'subscribers/getAll/'));
  }

  add(subscriber: Subscriber): Observable<Subscriber>{
    return this.http.post<Subscriber>(environment.subscribersSystemApi + 'subscribers/add/', subscriber);                            
  }
}
