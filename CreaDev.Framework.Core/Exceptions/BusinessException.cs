using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public List<string> Errors { get; set; }

        public int Code { get; protected set; }


        public BusinessException()
        {
            this.Errors = new List<string>();
        }

        public BusinessException(string message) : base(message)
        {
            this.Errors = new List<string>();
            this.Errors.Add(message);
        }

        public BusinessException(string message, Exception inner) : base(message, inner) { }

        public BusinessException(List<string> errors) : base()
        {
            this.Errors = errors;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
