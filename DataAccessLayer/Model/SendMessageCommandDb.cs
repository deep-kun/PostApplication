namespace DataAccessLayer.Model
{
    public class SendMessageCommandDb
    {
        public string Subject { get; set; }

        public string Body { get; set; }
        
        public int ReceiverId { get; set; }
        
        public int AuthorId { get; set; }
    }
}
