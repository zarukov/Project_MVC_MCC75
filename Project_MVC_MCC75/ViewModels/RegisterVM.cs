using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_MVC_MCC75.Models;

namespace Project_MVC_MCC75.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "NIK Must Be Filled")]
    public string NIK { get; set; }
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First Name Must Be Filled")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }
    [Display(Name = "Birth Date")]
    [Required(ErrorMessage = "Birth Date Must Be Filled")]
    public DateTime BirthDate { get; set; }
    public Gender_Enum Gender { get; set; }
    [Display(Name = "Hiring Date")]
    [Required(ErrorMessage = "Hiring Date Must Be Filled")]
    public DateTime HiringDate { get; set; }
    [EmailAddress]
    [Required(ErrorMessage = "Email Must Be Filled")]
    public string Email { get; set; }
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
    [Required(ErrorMessage = "Major Must Be Filled")]
    public string Major { get; set; }
    [MaxLength(2), MinLength(2, ErrorMessage = "ex: Wrong Input of {0}. (Ex: D3/S1)")]
    [Required(ErrorMessage = "{0} Should Be Filled. (Ex: D3/S1)")]
    public string Degree { get; set; }
    [Range(0, 4, ErrorMessage = "GPA Must Be Filled Between {1} to {2}")]
    [Required(ErrorMessage = "GPA Must Be Filled.")]
    public float GPA { get; set; }
    [Display(Name = "University Name")]
    [Required(ErrorMessage = "University Name Must Be Filled")]
    public string UniversityName { get; set; }
    [DataType(DataType.Password)]
    [StringLength(12, ErrorMessage = "The {0} Must Be Filled Between {2} and {1} Characters.", MinimumLength = 6)]
    public string Password { get; set; }
    [Display(Name = "Password Confirmation")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The Password Confirmation Did Not Match With the Password.")]
    public string PasswordConfirm { get; set; }
}
public enum Gender_Enum
{
    Male,
    Female
}