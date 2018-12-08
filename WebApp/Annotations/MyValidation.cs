using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAccesLayer;
using WebApp.Models;

namespace WebApp.Annotations
{
    public class MyValidation : ValidationAttribute
    {
        UserRepositiry rep = new UserRepositiry();
        public override bool IsValid(object value)
        {
            if (value!=null)
            {
                return rep.CheckUser((string)value);
            }
            return false;
        }
    }

    public class CheckForExist : ValidationAttribute
    {
        UserRepositiry rep = new UserRepositiry();
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                return !rep.CheckUser((string)value);
            }
            return false;
        }
    }
}