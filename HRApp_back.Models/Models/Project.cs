using System;
using System.ComponentModel.DataAnnotations;

namespace HRApp_back.Models.Models;

public class Project
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? managerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}