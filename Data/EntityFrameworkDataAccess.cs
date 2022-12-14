using System.Globalization;
using CodingTrackerWeb.Context;
using CodingTrackerWeb.Models;

namespace CodingTrackerWeb.Data;

public class EntityFrameworkDataAccess : IDataAccess
{
    private readonly CodingHoursContext _db;

    public EntityFrameworkDataAccess(CodingHoursContext db)
    {
        _db = db;
    }

    public void InsertRecord(CodingHour codingHour)
    {
        codingHour.Duration = GetDuration(codingHour.StartTime, codingHour.EndTime);
        _db.Add(codingHour);
        _db.SaveChanges();
    }

    public void DeleteRecord(int id)
    {
        var record = GetById(id);
        _db.Remove(record);
        _db.SaveChanges();
    }

    public List<CodingHour> GetAllRecords()
    {
        return _db.CodingHours.ToList();
    }

    public void UpdateRecord(int id, CodingHour codingHour)
    {
        var record = GetById(id);
        record.StartTime = codingHour.StartTime;
        record.EndTime = codingHour.EndTime;
        record.Duration = GetDuration(codingHour.StartTime, codingHour.EndTime);
    }

    public CodingHour GetById(int id)
    {
        return _db.CodingHours.First(x => x.Id == id);
    }
    
    private string GetDuration(string startTime, string endTime)
    {
        DateTime parsedStartTime = DateTime.ParseExact(startTime, "HH:mm", null, DateTimeStyles.None);
        DateTime parsedEndTime = DateTime.ParseExact(endTime, "HH:mm", null, DateTimeStyles.None);

        return parsedEndTime.Subtract(parsedStartTime).ToString();
    }
}