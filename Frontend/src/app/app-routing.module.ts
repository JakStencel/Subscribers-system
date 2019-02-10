import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SubscribersListComponent } from './Subscribers/subscribers-list/subscribers-list.component';
import { NavComponent } from './nav/nav/nav.component';
import { AddSubscriberComponent } from './subscribers/add-subscriber/add-subscriber.component';
import { OffersListComponent } from './offers/offers-list/offers-list.component';
import { AddOfferComponent } from './offers/add-offer/add-offer.component';
import { SellPhoneComponent } from './phones/sell-phone/sell-phone.component';

const routes: Routes = [
  {path: '', component: NavComponent},
  {path: 'subscribers', component: SubscribersListComponent},
  {path: 'addSubscriber', component: AddSubscriberComponent},
  {path: 'offers', component: OffersListComponent},
  {path: 'addOffer', component: AddOfferComponent},
  {path: 'sellOffer', component: SellPhoneComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
