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

    public void InsertRecord(CodingHours codingHours)
    {
        _db.Add(codingHours);
        _db.SaveChanges();
    }

    public void DeleteRecord(int id)
    {
        var record = GetById(id);
        _db.Remove(record);
        _db.SaveChanges();
    }

    public List<CodingHours> GetAllRecords()
    {
        return _db.CodingHours.ToList();
    }

    public void UpdateRecord(int id, CodingHours codingHours)
    {
        var record = GetById(id);
        record.StartTime = codingHours.StartTime;
        record.EndTime = codingHours.EndTime;
        record.Duration = GetDuration(codingHours.StartTime, codingHours.EndTime);
    }

    public CodingHours GetById(int id)
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