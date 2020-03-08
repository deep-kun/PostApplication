using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    class OperationResult<T>
    {
        public OperationResult(T result)
        {
            this.Result = result;
        }
        public T Result { get;}
    }

    enum ErrorStatus
    {
        Failure
    }
}
