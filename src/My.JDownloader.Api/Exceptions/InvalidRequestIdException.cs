using System;

namespace My.JDownloader.Api.Exceptions
{
    public class InvalidRequestIdException: Exception
    {
        public InvalidRequestIdException(string msg) : base(msg)
        { 
        }
    }
}
