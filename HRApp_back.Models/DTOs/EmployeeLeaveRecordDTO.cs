namespace HRApp_back.Models.DTOs;

public class EmployeeLeaveRecordDTO
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string Email { get; set; }
    public int? AnnualLeaveDays { get; set; }
    public int? SickLeaveDays { get; set; }
    public int? RemainingAnnualLeave { get; set; }
    public int? RemainingSickLeave { get; set; }
}
