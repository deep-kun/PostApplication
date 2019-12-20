export class SendedMessage {
    constructor(
        public subject: string,
        public body: string,
        public receiver: string,
    ) { }
}
