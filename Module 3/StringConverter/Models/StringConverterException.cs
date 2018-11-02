using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace StringConverter.Models
{
    [Serializable]
    public class StringConverterException : Exception
    {
        public char NonNumericChar { get; private set; }

        public int CharPosition { get; private set; }

        public StringConverterException() { }

        public StringConverterException(string message) : base(message) { }

        public StringConverterException(string message, Exception innerException) : base(message, innerException) { }

        public StringConverterException(char nonNumericChar, int charPosition)
            : base($"Input string contains at least one non-numeric symbol: {nonNumericChar} at position {charPosition}")
        {
            NonNumericChar = nonNumericChar;
            CharPosition = charPosition;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected StringConverterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            NonNumericChar = info.GetChar("NonNumericChar");
            CharPosition = info.GetInt32("CharPosition");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("NonNumericChar", NonNumericChar);
            info.AddValue("CharPosition", CharPosition);
            base.GetObjectData(info, context);
        }
    }
}
