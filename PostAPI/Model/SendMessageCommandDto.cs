namespace PostAPI.Model
{
    public class SendMessageCommandDto
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public int AuthorId { get; set; }
        
        public string Receiver { get; set; }
    }
}