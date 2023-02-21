using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_MCC75.Models;

[Table("tb_m_roles")]
public class Role
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Required, Column("name"), MaxLength(50)]
    public string Name { get; set; }

    //relasi
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
