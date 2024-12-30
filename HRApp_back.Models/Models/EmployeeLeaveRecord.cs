using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp_back.Models.Models;

public class EmployeeLeaveRecord
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    // public Employee Employee { get; set; }

    // Leave Details
    public int? AnnualLeaveDays { get; set; } = 20;
    public int? SickLeaveDays { get; set; } = 10;
    public int? RemainingAnnualLeave { get; set; }
    public int? RemainingSickLeave { get; set; } 
}
