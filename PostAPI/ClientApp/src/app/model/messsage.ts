export interface Message {
    messageId: number;
    date: Date;
    subject: string;
    isRead: boolean;
    IsStared: boolean;
    author: string;
}
