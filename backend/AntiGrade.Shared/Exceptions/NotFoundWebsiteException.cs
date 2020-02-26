using AntiGrade.Shared.Enums;
using DigitalSkynet.DotnetCore.Api.Exceptions;

namespace AntiGrade.Shared.Exceptions
{
    public class NotFoundWebsiteException : ApiException
    {
        public NotFoundWebsiteException(string message) : base(ResponseCode.NotFound, message)
        { }
    }
}
