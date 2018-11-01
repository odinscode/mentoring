using System;

namespace StringConverter.Models
{
    public class StringConverterException : Exception
    {
        public StringConverterException(string message) : base(message) { }
    }
}
