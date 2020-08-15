using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
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

    public class NotEnoughRightsException : PostException
    {
        public NotEnoughRightsException(string actionName)
            : base($"Not enoght right to do {actionName}.")
        {
        }
    }
}
