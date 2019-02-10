import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from "../../environments/environment";
import { Observable } from '../../../node_modules/rxjs';
import { Phone } from '../Models/phone';

@Injectable({
  providedIn: 'root'
})
export class PhoneService {

  constructor(private http: HttpClient) { }

  sellPhone(subscriberId: number, offerId: number): Observable<Phone>{
      return this.http.get<Phone>(environment.subscribersSystemApi + `phones/assignOfferToSubscriber/${subscriberId}/${offerId}`);
  }
}
