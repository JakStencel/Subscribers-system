import { Component, OnInit } from '@angular/core';
import { Offer } from '../../Models/offer';
import { OfferService } from '../../services/offer.service';

@Component({
  selector: 'app-offers-list',
  templateUrl: './offers-list.component.html',
  styleUrls: ['./offers-list.component.css']
})
export class OffersListComponent implements OnInit {

  offers: Offer[] = undefined;

  constructor(private offerService: OfferService) { }

  ngOnInit() {
    this.getOffers();
  }

  getOffers(){
    this.offerService.getOffers().subscribe(offerFromService => this.offers = offerFromService);
  }

}
