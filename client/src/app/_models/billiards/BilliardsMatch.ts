export interface BilliardsMatch
{
    id: number;
    winUserId: number;
    loseUserId: number;
    winPhotoUrl: string;
    losePhotoUrl: string;
    typeId: number;
    modeId: number;
    seasonNumberId: number;
    tournamentId: number;
    winnerWins: number;
    loserWins: number;
    totalGamesPlayed: number;
    datePlayed: Date;
}
