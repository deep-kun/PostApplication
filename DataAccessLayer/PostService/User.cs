using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.PostService
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            UsersMessagesMappeds = new HashSet<UsersMessagesMapped>();
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string UserLogin { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(Message.Author))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(UsersMessagesMapped.User))]
        public virtual ICollection<UsersMessagesMapped> UsersMessagesMappeds { get; set; }
    }
}
