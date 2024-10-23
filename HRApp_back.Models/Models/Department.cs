using System.ComponentModel.DataAnnotations;

namespace HRApp_back.Models.Models;

public class Department
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}