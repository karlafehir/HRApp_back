using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApp_back.Models.Models;

public class Job
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime? ClosingDate { get; set; } 
    public string Status { get; set; }
    public string Location { get; set; }
    public string Priority { get; set; }
    public ICollection<Candidate>? Candidates { get; set; } = new List<Candidate>();
}

