export interface BilliardsTournamentMembers
{
    id: number;
    userId: number;
    username: string;
    gamerTag: string;
    tournamentId: number;
    tournamentName: string;
    photoUrl: string;
    wins: number;
    seasonWins: number;
    typeWins: number;
    nonPlayoffWins: number;
    playoffWins: number;
    seasonPlayed: number;
    typePlayed: number;
    nonPlayoffPlayed: number;
    playoffPlayed: number;
    totalGamesPlayed: number;
    opponentUserId: number;
    typeId: number;
}
