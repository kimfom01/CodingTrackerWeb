using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace CodingTrackerWeb.Models;

public class CodingHour
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
    public string Date { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    [Display(Name = "Start Time")]
    public string StartTime { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    [Display(Name ="End Time")]
    public string EndTime { get; set; } = string.Empty;

    [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
    public string Duration { get; internal set; } = string.Empty;

    public ApplicationUser? ApplicationUser { get; set; }
    public required string ApplicationUserId { get; set; }
}
