using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models.Domains
{
    public class List
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Genre { get; set; }
        [JsonIgnore]
        public string? Content { get; set; }

        public DateTimeOffset DateCreated { get; set; } = GetCurrentTimeInDesiredTimeZone();

        private static DateTime GetCurrentTimeInDesiredTimeZone()
        {
            TimeZoneInfo desiredTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // ((GMT+07:00) Bangkok, Hanoi, Jakarta)

            return TimeZoneInfo.ConvertTime(DateTime.Now, desiredTimeZone);
        }

        [NotMapped]
        
        public string[] ArrayContent
        {
            get
            {
                if (this.Content != null)
                {
                    string[] tab = this.Content.Split(';');
                    return tab;
                }
                return new string[0];
            }
            set
            {
                this.Content = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }

    }
}
