using System;
using System.Runtime.Serialization;

namespace eStore_Admin.Application.Exceptions
{
    [Serializable]
    public class ShoppingCartAlreadyAddedException : ApplicationException
    {
        public ShoppingCartAlreadyAddedException() : base()
        {
        }

        public ShoppingCartAlreadyAddedException(string message) : base(message)
        {
        }

        public ShoppingCartAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ShoppingCartAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}