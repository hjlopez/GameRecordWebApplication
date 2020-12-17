export interface User {
    id: number;
    username: string;
    token: string;
    photoUrl: string;
    gamerTag: string;
    playMH: boolean;
    playDota: boolean;
    joinBilliards: boolean;
    roles: string[];
    email: string;
}
