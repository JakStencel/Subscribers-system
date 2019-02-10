import {Offer} from "./offer";
import { Connection } from './Connection';
import { Sms } from './Sms';

export class Phone{
    Id: number;
    PhoneNumber: number;
    SecondsLeftInBundle: number;
    TextMessagesLeftInBundle: number;
    CostOfConnectionsOutsideBundle: number;
    CostOfMessagesOutsideBundle: number;
    Offer: Offer;
    Connections: Connection[];
    ShortTextMessages: Sms[];
}