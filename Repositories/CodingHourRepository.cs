using System.Globalization;
using CodingTrackerWeb.Data;
using CodingTrackerWeb.Models;

namespace CodingTrackerWeb.Repositories;

public class CodingHourRepository : Repository<CodingHour>, ICodingHourRepository
{
    public CodingHourRepository(CodingTrackerWebContext dbContext) : base(dbContext)
    {
    }

    public override void InsertRecord(CodingHour codingHour)
    {
        codingHour.Duration = GetDuration(codingHour.StartTime, codingHour.EndTime);
        base.InsertRecord(codingHour);
        DbContext.SaveChanges();
    }

    public override void UpdateRecord(int id, CodingHour codingHour)
    {
        codingHour.StartTime = codingHour.StartTime;
        codingHour.EndTime = codingHour.EndTime;
        codingHour.Duration = GetDuration(codingHour.StartTime, codingHour.EndTime);
        base.UpdateRecord(id, codingHour);
        DbContext.SaveChanges();
    }

    public override void DeleteRecord(int id)
    {
        base.DeleteRecord(id);
        DbContext.SaveChanges();
    }

    private string GetDuration(string startTime, string endTime)
    {
        var parsedStartTime = DateTime.ParseExact(startTime, "HH:mm", null, DateTimeStyles.None);
        var parsedEndTime = DateTime.ParseExact(endTime, "HH:mm", null, DateTimeStyles.None);

        var duration = parsedEndTime.Subtract(parsedStartTime);

        if (duration < TimeSpan.Zero)
        {
            duration += TimeSpan.FromDays(1);
        }

        return duration.ToString();
    }
}