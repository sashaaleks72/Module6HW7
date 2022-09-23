using System;

namespace Module6HW7.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
