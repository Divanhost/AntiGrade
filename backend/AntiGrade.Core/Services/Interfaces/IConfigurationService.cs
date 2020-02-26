using System.Collections.Generic;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        string Email
        {
            get;
        }
        string Password
        {
            get;
        }
        string SenderName
        {
            get;
        }
        List<string> ToEmailAddress
        {
            get;
        }
        string SentryUrl
        {
            get;
        }
        string HashSecret
        {
            get;
        }
        string Key
        {
            get;
        }
        string JwtIssuer
        {
            get;
        }
        string JwtAudience
        {
            get;
        }
        string JwtKey
        {
            get;
        }
        double JwtExpireSeconds
        {
            get;
        }
        string BaseUrl
        {
            get;
        }
        double DefaultUrlPriority
        {
            get;
        }
        string UploadsPath
        {
            get;
        }

        string AvatarPath
        {
            get;
        }

        string ScreenShotsFolder
        {
            get;
        }
    }
}