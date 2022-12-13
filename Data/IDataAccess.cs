using CodingTrackerWeb.Models;

namespace CodingTrackerWeb.Data;

public interface IDataAccess
{
    public void InsertRecord(CodingHours codingHours);
    public void DeleteRecord(int id);
    public List<CodingHours> GetAllRecords();
    public void UpdateRecord(int id, CodingHours codingHours);
    public CodingHours GetById(int id);
    
}