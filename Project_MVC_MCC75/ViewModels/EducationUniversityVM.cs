using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC_MCC75.ViewModels;

public class EducationUniversityVM
{
    public int Id { get; set; }
    public string Major { get; set; }
    [MaxLength(2), MinLength(2, ErrorMessage = "Example of Degree Input: S1/D3")]
    [Required(ErrorMessage = "Example of Degree Input: S1/D3")]
    public string Degree { get; set; }
    [Range(0, 4, ErrorMessage ="Value That You Added is Out of Range (Should Be Between {1} Until {2}).")]
    public float GPA { get; set; }
    [Display(Name ="University Name")]
    public string UniversityName { get; set; }
}
