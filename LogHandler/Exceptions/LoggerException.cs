using System;

namespace LoggerHandler.Exceptions
{
    public class LoggerException : Exception
    {
        public LoggerException() : base("An Error Occured while logging Message.")
        {

        }
    }
}
