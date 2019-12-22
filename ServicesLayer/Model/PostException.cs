using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesLayer.Model
{
    public class PostException : Exception
    {
        public PostException(string message)
            :base(message)
        {
        }
    }

    public class NotFoundExeption : PostException
    {
        public NotFoundExeption(string entityValue)
            :base($"Entity '{entityValue}' not found.")
        {
        }
    }

    public class AlredyExistsException : PostException
    {
        public AlredyExistsException(string entityValue)
            : base($"Entity '{entityValue}' already exists.")
        {
        }
    }
}
