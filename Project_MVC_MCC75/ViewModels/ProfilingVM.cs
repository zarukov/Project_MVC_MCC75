using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_MCC75.ViewModels;

public class ProfilingVM
{
    public int Id { get; set; }
    [Display(Name = "Employee's NIK")]
    [Required(ErrorMessage ="Employee's NIK Must Be Filled.")]
    public string EmployeeNIK { get; set; }
    [Display(Name = "Education ID")]
    [Required(ErrorMessage = "Education ID Must Be Filled.")]
    public int EducationId { get; set; }
}
