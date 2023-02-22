using System.ComponentModel.DataAnnotations;

namespace Project_MVC_MCC75.ViewModels;

public class LoginVM
{
    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "The {0} Must Be Filled Between 6 and 12 Characters.")]
    public string Password { get; set; }
}
