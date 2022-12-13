using System.ComponentModel.DataAnnotations;

namespace CodingTrackerWeb.Models
{
    public class CodingHours
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string StartTime { get; set; } = String.Empty;

        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string EndTime { get; set; } = String.Empty;

        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string Duration { get; internal set; } = String.Empty;
    }
}
