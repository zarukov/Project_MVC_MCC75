using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Project_MVC_MCC75.Models;

[Table("tb_m_employees")]
public class Employee
{
    [Key, Column("nik", TypeName ="nchar(5)")]
    public string NIK { get; set; }

    [Required, Column("first_name"), MaxLength(50)]
    public string FirstName { get; set; }

    [Column("last_name"), MaxLength(50)]
    public string? LastName { get; set; }

    [Required, Column("birthdate")]
    public DateTime BirthDate { get; set; }

    [Required, Column("gender")]
    public GenderEnum Gender { get; set; }

    [Required, Column("hiring_date")]
    public DateTime HiringDate { get; set; } = DateTime.Now;//(data akan ke input sesuai dgn waktu saat ini

    [Required, Column("email"), MaxLength(50)]
    public string Email { get; set; }

    [Column("phone_number"), MaxLength(20)]
    public string? PhoneNumber { get; set; }

    //kardinalitas
    public ICollection<Profiling>? Profilings { get; set; }
    public Account? Account { get; set; }
}
public enum GenderEnum
{
    Male,
    Female
}
