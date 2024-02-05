using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Domains
{
    public class User : IdentityUser
    {
        public string? ProfilePic { get; set; }
        public DateTimeOffset DateCreated { get; set; } = GetCurrentTimeInDesiredTimeZone();

        private static DateTime GetCurrentTimeInDesiredTimeZone()
        {
            TimeZoneInfo desiredTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // ((GMT+07:00) Bangkok, Hanoi, Jakarta)

            return TimeZoneInfo.ConvertTime(DateTime.Now, desiredTimeZone);
        }

    }
}
