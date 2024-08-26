using System.ComponentModel.DataAnnotations;

namespace HRApp_back.Models.Models;

public class Job
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

