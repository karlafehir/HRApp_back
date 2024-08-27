using System.ComponentModel.DataAnnotations;

namespace HRApp_back.Models.Models;

public class Candidate
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ResumeUrl { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
}