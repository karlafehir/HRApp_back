using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp_back.Models.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfHire { get; set; }

    // Job
    [ForeignKey("Job")]
    public int? JobId { get; set; }
    public Job Job { get; set; }

    // Department
    public int? DepartmentId { get; set; } 
    
    // Project
    [ForeignKey("Project")]
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    // Employment Details
    public string JobTitle { get; set; }
    public decimal Salary { get; set; }
    public string EmploymentStatus { get; set; }

    // Leave Details
    public EmployeeLeaveRecord EmployeeLeaveRecord { get; set; }
}