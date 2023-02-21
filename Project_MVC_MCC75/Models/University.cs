using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_MCC75.Models;

[Table("tb_m_universities")]
public class University
{
    [Required, Column("id")]
    public int Id { get; set; }

    [Required, Column("name"), MaxLength(100)]
    public string Name { get; set; }

    //Relasi & kardinalitas
    public ICollection<Education>? Educations { get; set; }//ICollection = many
}
