import { PageParams } from './PageParams';

export interface MatchParams extends PageParams
{
    tournamentId: number;
    typeId: number;
    modeId: number;
    seasonNumberId: number;
}
