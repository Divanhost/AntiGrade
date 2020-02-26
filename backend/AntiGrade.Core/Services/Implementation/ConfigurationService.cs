using AntiGrade.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BusinessIntelligence.Core.Services.Implementation
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Email => _configuration.GetSection("EmailNotifications")["FromEmailAddress:Email"];
        public string Password => _configuration.GetSection("EmailNotifications")["FromEmailAddress:Password"];
        public string SenderName => _configuration.GetSection("EmailNotifications")["SenderName"];
        public List<string> ToEmailAddress => _configuration.GetSection("EmailNotifications:ToEmailAddress").Get<List<string>>();
        public string SentryUrl => _configuration.GetSection("Sentry")["Url"];
        public string HashSecret => _configuration.GetSection("Hash")["Secret"];
        public string Key => _configuration.GetSection("Hash")["Key"];
        public string JwtIssuer => _configuration.GetSection("JWT")["Issuer"];
        public string JwtKey => _configuration.GetSection("JWT")["Secret"];
        public double JwtExpireSeconds
        {
            get
            {
                string expireString = _configuration.GetSection("JWT")["ExpireSeconds"];
                double secondsExpire;
                double.TryParse(expireString, out secondsExpire);
                return secondsExpire;
            }
        }

        public string JwtAudience => _configuration.GetSection("JWT")["Audience"];
        public string BaseUrl => _configuration.GetSection("SiteMap")["BaseUrl"];
        public double DefaultUrlPriority => 1.0d;
        public string UploadsPath => _configuration.GetSection("Uploads")["UploadsPath"];
        public string AvatarPath => _configuration.GetSection("Uploads")["AvatarPath"];
        public string ScreenShotsFolder => _configuration.GetSection("ActivityScreensFolder")["Path"];
    }
}