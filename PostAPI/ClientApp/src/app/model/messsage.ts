export interface Message {
    MessageId: number;
    date: Date;
    subject: string;
    isRead: boolean;
    IsStared: boolean;
    author: string;
}
