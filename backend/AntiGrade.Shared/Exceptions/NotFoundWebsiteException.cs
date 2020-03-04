using AntiGrade.Shared.Enums;
namespace AntiGrade.Shared.Exceptions
{
    public class NotFoundWebsiteException : ApiException
    {
        public NotFoundWebsiteException(string message) : base(ResponseCode.NotFound, message)
        { }
    }
}
