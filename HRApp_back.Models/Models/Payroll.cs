using System;
using System.ComponentModel.DataAnnotations;

namespace HRApp_back.Models.Models;

public class Payroll
{
    [Key]
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public DateTime Date { get; set; }
    public decimal Salary { get; set; }
    public decimal? Bonus { get; set; }
    public bool IsComplete { get; set; }
}