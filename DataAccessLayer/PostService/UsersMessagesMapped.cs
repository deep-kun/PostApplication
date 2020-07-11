using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.PostService
{
    [Table("UsersMessagesMapped")]
    public partial class UsersMessagesMapped
    {
        [Key]
        public int UsersMessagesMappedId { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int PlaceHolderId { get; set; }
        public bool IsRead { get; set; }
        public bool IsStarred { get; set; }

        [ForeignKey(nameof(MessageId))]
        [InverseProperty("UsersMessagesMappeds")]
        public virtual Message Message { get; set; }
        [ForeignKey(nameof(PlaceHolderId))]
        [InverseProperty(nameof(MessagePlaceHolder.UsersMessagesMappeds))]
        public virtual MessagePlaceHolder PlaceHolder { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UsersMessagesMappeds")]
        public virtual User User { get; set; }
    }
}
