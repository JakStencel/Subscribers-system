import { Component, OnInit } from '@angular/core';
import { Offer } from '../../Models/offer';
import { Router } from '../../../../node_modules/@angular/router';
import { OfferService } from '../../services/offer.service';

@Component({
  selector: 'app-add-offer',
  templateUrl: './add-offer.component.html',
  styleUrls: ['./add-offer.component.css']
})
export class AddOfferComponent implements OnInit {

  offer: Offer;

  constructor(private offerService: OfferService,
              private router: Router) { 
                this.offer = new Offer();
              }

  ngOnInit() {
  }

  addNewOffer(){
    this.offerService.add(this.offer).subscribe(
            data => alert('Offer with id: ' + data + '  has been added'));
            this.router.onSameUrlNavigation = "reload";
            this.router.navigateByUrl('offers');
  }
}
