using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.PostService
{
    public partial class Message
    {
        public Message()
        {
            UsersMessagesMappeds = new HashSet<UsersMessagesMapped>();
        }

        [Key]
        public int MessageId { get; set; }
        [Required]
        [StringLength(25)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [InverseProperty(nameof(User.Messages))]
        public virtual User Author { get; set; }
        [InverseProperty(nameof(UsersMessagesMapped.Message))]
        public virtual ICollection<UsersMessagesMapped> UsersMessagesMappeds { get; set; }
    }
}
