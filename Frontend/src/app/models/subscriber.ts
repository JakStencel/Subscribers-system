import {Phone} from "./phone";

export class Subscriber{
    Id: number;
    Name: string;
    Surname: string;
    DateOfBirth: Date;
    Email: string;
    BillingCycle: number;
    Phones: Phone[];
}