using System.ComponentModel.DataAnnotations;

namespace API.Models.Domains
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? Img { get; set; }
        public string? ImgTitle { get; set; }
        public string? Trailer { get; set; }
        public string? Video { get; set; }
        public string? Year { get; set; }
        public int Limit { get; set; }
        public string? Genre { get; set; }
        public bool IsSeries { get; set; }
        public DateTimeOffset DateCreated { get; set; } = GetCurrentTimeInDesiredTimeZone();

        private static DateTime GetCurrentTimeInDesiredTimeZone()
        {
            TimeZoneInfo desiredTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // ((GMT+07:00) Bangkok, Hanoi, Jakarta)

            return TimeZoneInfo.ConvertTime(DateTime.Now, desiredTimeZone);
        }

    }
}
