export class User {
    constructor(
        public login: string,
        public name: string,
        public password: string,
        public token?: string
    ) { }

}
