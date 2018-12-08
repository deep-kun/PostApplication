using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebApp.Annotations;

namespace WebApp.Models
{
    public class SendedMessageView
    {
        [MyValidation]
        public string Receiver { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}