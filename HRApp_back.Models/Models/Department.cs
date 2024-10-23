using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRApp_back.Models.Models;

public class Department
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Manager
    [ForeignKey("Manager")]
    public int? ManagerId { get; set; }
    public Employee Manager { get; set; }

    // Employees
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}