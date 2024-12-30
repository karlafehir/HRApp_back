using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HRApp_back.Models.Models;

public class Candidate
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? ResumeUrl { get; set; }
    public string? GithubUrl { get; set; }
    [ForeignKey("Job")]
    public int JobId { get; set; }
    [JsonIgnore]
    public Job Job { get; set; }
    public CandidateStatus Status { get; set; } = CandidateStatus.NewApplied;
}

public enum CandidateStatus
{
    NewApplied,
    Shortlisted,
    Interview,
    Test,
    Hired
}
