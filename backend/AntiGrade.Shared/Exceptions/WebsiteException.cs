using System.Runtime.Serialization;
using AntiGrade.Shared.Enums;
namespace AntiGrade.Shared.Exceptions
{
    public class WebsiteException : ApiException
    {
        public WebsiteException(string message) : base(ResponseCode.UnexpectedError, message)
        { }

        public WebsiteException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
