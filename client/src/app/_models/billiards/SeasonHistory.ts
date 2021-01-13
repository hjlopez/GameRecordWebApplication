export interface SeasonHistory
{
    id: number;
    seasonNumberId: number;
    seasonNumber: number;
    tournamentId: number;
    tournamentName: string;
    typeId: number;
    typeName: string;
    userId: number;
    username: string;
    rank: number;
    matchId: number;
    modeId: number;
    isDone: boolean;
    url: string;
    wins: number;
}
