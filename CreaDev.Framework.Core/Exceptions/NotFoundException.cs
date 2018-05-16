using System;

namespace CreaDev.Framework.Core.Exceptions
{
    [Serializable]
    public class NotFoundException : BusinessException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class ItemAlreadyExistException : BusinessException
    {
        public ItemAlreadyExistException() { }
        public ItemAlreadyExistException(string message) : base(message) { }
        public ItemAlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected ItemAlreadyExistException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}