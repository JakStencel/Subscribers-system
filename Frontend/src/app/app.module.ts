import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SubscribersListComponent } from './Subscribers/subscribers-list/subscribers-list.component';
import { HttpClientModule } from '@angular/common/http';
import { SubscriberService } from './services/subscriber.service';
import { NavComponent } from './nav/nav/nav.component';
import { AddSubscriberComponent } from './subscribers/add-subscriber/add-subscriber.component';
import { OffersListComponent } from './offers/offers-list/offers-list.component';
import { AddOfferComponent } from './offers/add-offer/add-offer.component';
import { SellPhoneComponent } from './phones/sell-phone/sell-phone.component';

@NgModule({
  declarations: [
    AppComponent,
    SubscribersListComponent,
    NavComponent,
    AddSubscriberComponent,
    OffersListComponent,
    AddOfferComponent,
    SellPhoneComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    SubscriberService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
