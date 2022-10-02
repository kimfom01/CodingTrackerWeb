using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CodingTrackerWeb.Models
{
    public class CodingHoursModel
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

        public string GetDuration()
        {
            DateTime parsedStartTime = DateTime.ParseExact(this.StartTime, "HH:mm", null, DateTimeStyles.None);
            DateTime parsedEndTime = DateTime.ParseExact(this.EndTime, "HH:mm", null, DateTimeStyles.None);

            return parsedEndTime.Subtract(parsedStartTime).ToString();
        }
    }
}
