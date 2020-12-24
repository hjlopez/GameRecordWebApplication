export interface TournamentModes
{
    id: number;
    tournamentId: number;
    order: number;
    isLast: boolean;
    isConsolation: boolean;
    highestRank: number;
    isPlayoff: boolean;
    modeId: number;
}
