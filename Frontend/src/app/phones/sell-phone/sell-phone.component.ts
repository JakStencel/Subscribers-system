import { Component, OnInit } from '@angular/core';
import { Subscriber } from '../../Models/subscriber';
import { Offer } from '../../Models/offer';
import { SubscriberService } from '../../services/subscriber.service';
import { OfferService } from '../../services/offer.service';
import { PhoneService } from '../../services/phone.service';
import { Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-sell-phone',
  templateUrl: './sell-phone.component.html',
  styleUrls: ['./sell-phone.component.css']
})
export class SellPhoneComponent implements OnInit {

  subscribers: Subscriber[];
  offers: Offer[];
  selectedSubscriber: Subscriber;
  selectedOffer: Offer;

  constructor(private subscriberService: SubscriberService,
              private offersService: OfferService,
              private phoneService: PhoneService,
              private router: Router) { }

  ngOnInit() {
    this.subscriberService.getSubscribers().subscribe(subscribersFromService => this.subscribers = subscribersFromService);
    this.offersService.getOffers().subscribe(offerFromService => this.offers = offerFromService);
  }

  sellPhone(subscriberId: number, offerId: number){
    this.phoneService.sellPhone(subscriberId, offerId)
      .subscribe(phoneId => alert(`Phone with id: ${phoneId} has been assigned`));
      this.router.navigateByUrl('');
  }
}
