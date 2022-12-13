using System.Globalization;
using CodingTrackerWeb.Models;
using Microsoft.Data.Sqlite;

namespace CodingTrackerWeb.Data;

public class DataAccess : IDataAccess
{
    private readonly IConfiguration _configuration;

    public DataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void InsertRecord(CodingHours codingHours)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @$"INSERT INTO coding_hours(Date, StartTime, EndTime, Duration)
                                            VALUES('{codingHours.Date}', '{codingHours.StartTime}', '{codingHours.EndTime}', '{GetDuration(codingHours.StartTime, codingHours.EndTime)}')";

                command.ExecuteNonQuery();
            }
        }
    }

    public List<CodingHours> GetAllRecords()
    {
        var codingHoursModels = new List<CodingHours>();

        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "SELECT * FROM coding_hours";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var tempModel = new CodingHours
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        StartTime = (string)reader["StartTime"],
                        EndTime = (string)reader["EndTime"],
                        Duration = (string)reader["Duration"]
                    };

                    codingHoursModels.Add(tempModel);
                }
            }
        }

        return codingHoursModels;
    }

    public void UpdateRecord(int id, CodingHours codingHours)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @$"UPDATE coding_hours
                                            SET Date = '{codingHours.Date}', StartTime = '{codingHours.StartTime}', EndTime = '{codingHours.EndTime}', Duration = '{GetDuration(codingHours.StartTime, codingHours.EndTime)}'
                                            WHERE Id = {id}";

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteRecord(int id)
    {
        using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @$"DELETE FROM coding_hours
                                            WHERE Id = {id}";

                command.ExecuteNonQuery();
            }
        }
    }

    public CodingHours GetById(int id)
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString"));
        {
            using (var command = connection.CreateCommand())
            {
                var codingHours = new CodingHours();

                connection.Open();

                command.CommandText = @$"SELECT * FROM coding_hours
                                            WHERE Id = {id}";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    codingHours.Id = reader.GetInt32(0);
                    codingHours.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    codingHours.StartTime = (string)reader["StartTime"];
                    codingHours.EndTime = (string)reader["EndTime"];
                    codingHours.Duration = (string)reader["Duration"];
                }

                return codingHours;
            }
        }
    }

    private string GetDuration(string startTime, string endTime)
    {
        DateTime parsedStartTime = DateTime.ParseExact(startTime, "HH:mm", null, DateTimeStyles.None);
        DateTime parsedEndTime = DateTime.ParseExact(endTime, "HH:mm", null, DateTimeStyles.None);

        return parsedEndTime.Subtract(parsedStartTime).ToString();
    }
}