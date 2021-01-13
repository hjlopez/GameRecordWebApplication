import { SeasonHistory } from './SeasonHistory';

export class SeasonHistoryArray
{
    constructor(
        public rank: number,
        public member: string,
        public typeId: number,
        public url: string,
        public wins: number
        ) {}
    // constructor(public history: SeasonHistory[]) {}
}
