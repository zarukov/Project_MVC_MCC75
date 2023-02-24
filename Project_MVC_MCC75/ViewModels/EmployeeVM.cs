/*using Microsoft.Build.Framework;
using System.ComponentModel;*/
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC_MCC75.ViewModels;
public class EmployeeVM
{
    [Required(ErrorMessage = "NIK Must Be Filled")]
    public string NIK { get; set; }
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First Name Must Be Filled")]
    public string FirstName { get; set; }
    [Display(Name ="Last Name")]
    public string? LastName { get; set; }
    [Display(Name = "Birth Date")]
    [Required(ErrorMessage = "Birth Date Must Be Filled")]
    public DateTime BirthDate { get; set; }
    [Required(ErrorMessage = "Gender Must Be Filled")]
    public GenderEnum Gender { get; set; }
    [Display(Name = "Hiring Date")]
    [Required(ErrorMessage = "Hiring Date Must Be Filled")]
    public DateTime HiringDate { get; set; }
    [Required(ErrorMessage = "Email Must Be Filled")]
    public string Email { get; set; }
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
}
public enum GenderEnum
{
    Male,
    Female
}