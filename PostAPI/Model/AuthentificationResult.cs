using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAPI.Model
{
    public class AuthentificationResult
    {
        public bool IsSuccess { get => string.IsNullOrEmpty(this.ErrorMessage) && !string.IsNullOrEmpty(this.Token); }
        public string Token { get; set; }
        public User User { get; set; }
        public string ErrorMessage { get; set; }
    }
}
