import { Component, OnInit } from '@angular/core';
import { Subscriber } from '../../Models/subscriber';
import { SubscriberService } from '../../services/subscriber.service';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-subscribers-list',
  templateUrl: './subscribers-list.component.html',
  styleUrls: ['./subscribers-list.component.css']
})

export class SubscribersListComponent implements OnInit {

  subscribers: Subscriber[] = undefined;

  constructor(private subscriberService: SubscriberService,
              private invoiceService: InvoiceService) { }

  ngOnInit() {
    this.getSubscribers();
  }

  getSubscribers(){
    this.subscriberService.getSubscribers()
                  .subscribe(dataFromService => this.subscribers = dataFromService);
  }

  generateInvoice(subscriberId: number) {
    this.invoiceService.generateInvoice(subscriberId)
                  .subscribe(invoice => alert(`New invoice with number: ${invoice.Number} created!`));
  }
}
