import { Injectable } from '@angular/core';
import { Observable } from '../../../node_modules/rxjs';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Offer } from '../Models/offer';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private http: HttpClient) { }

  getOffers(): Observable<Offer[]>{
    return this.http.get<Offer[]>((environment.subscribersSystemApi + 'offers/getAll/'));
  }

  add(offer: Offer): Observable<Offer>{
    return this.http.post<Offer>(environment.subscribersSystemApi + 'offers/add/', offer);                            
  }
}
