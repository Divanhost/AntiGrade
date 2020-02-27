export class TokenCouple {
    jwt: string;
    refresh: string;
    id: number;
    constructor(jwt: string, refresh: string) {
        this.jwt = jwt;
        this.id = 0;
        this.refresh = refresh;
    }
}
