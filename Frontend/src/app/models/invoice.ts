import { Subscriber } from './subscriber';
import { PhoneReport } from './phoneReport';

export class Invoice{
    Id: number;
    Number: string;
    BeginningDate: Date;
    GenerationDate: Date;
    Subscriber: Subscriber;
    TotalCostOfConnections: number;
    TotalCostOfTextMessages: number;
    TotalOffersCost: number;
    TotalCostToBePaid: number;

    PhoneReports: PhoneReport[];
}