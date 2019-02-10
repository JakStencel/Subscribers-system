import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';
import { Invoice } from '../Models/invoice';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(private http: HttpClient) { }

  generateInvoice(subscriberId: number): Observable<Invoice>{
    return this.http.get<Invoice>(environment.subscribersSystemApi + `invoices/generate/${subscriberId}`);
  }
}
