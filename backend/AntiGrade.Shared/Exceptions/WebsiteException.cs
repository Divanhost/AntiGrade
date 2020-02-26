using System.Runtime.Serialization;
using AntiGrade.Shared.Enums;
using DigitalSkynet.DotnetCore.Api.Exceptions;

namespace BusinessIntelligence.BusinessObjects.Exceptions
{
    public class WebsiteException : ApiException
    {
        public WebsiteException(string message) : base(ResponseCode.UnexpectedError, message)
        { }

        public WebsiteException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
