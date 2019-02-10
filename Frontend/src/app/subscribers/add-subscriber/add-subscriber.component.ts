import { Component, OnInit } from '@angular/core';
import { SubscriberService } from '../../services/subscriber.service';
import { Router } from '@angular/router';
import { Subscriber } from '../../Models/subscriber';

@Component({
  selector: 'app-add-subscriber',
  templateUrl: './add-subscriber.component.html',
  styleUrls: ['./add-subscriber.component.css']
})
export class AddSubscriberComponent implements OnInit {

  subscriber: Subscriber;

  constructor(private subscriberService: SubscriberService,
              private router: Router) {
                    this.subscriber = new Subscriber();
               }

  ngOnInit() {
  }

  addNewSubscriber(){
    this.subscriberService.add(this.subscriber).subscribe(
            data => alert('Subscriber with id: ' + data + ' has been added'));
            this.router.onSameUrlNavigation = "reload";
            this.router.navigateByUrl('subscribers');
  }
}
