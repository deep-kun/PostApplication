using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.PostService
{
    public partial class MessagePlaceHolder
    {
        public MessagePlaceHolder()
        {
            UsersMessagesMappeds = new HashSet<UsersMessagesMapped>();
        }

        [Key]
        public int PlaceHolderId { get; set; }
        [Required]
        [StringLength(25)]
        public string PlaceHolder { get; set; }

        [InverseProperty(nameof(UsersMessagesMapped.PlaceHolder))]
        public virtual ICollection<UsersMessagesMapped> UsersMessagesMappeds { get; set; }
    }
}
