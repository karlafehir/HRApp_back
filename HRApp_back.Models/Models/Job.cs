using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp_back.Models.Models;

public class Job
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
        
    // Foreign key to Department
    public int? DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
        
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    
    public JobStatus Status { get; set; } // Enum type
    
    public string Location { get; set; }
    public string Priority { get; set; }
    public ICollection<Candidate>? Candidates { get; set; } = new List<Candidate>();
}

