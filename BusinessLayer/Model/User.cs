﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}