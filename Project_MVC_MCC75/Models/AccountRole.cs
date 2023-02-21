using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_MCC75.Models;

[Table("tb_tr_account_roles")]
public class AccountRole
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Required, Column("account_nik", TypeName ="nchar(5)")]
    public string AccountNIK { get; set; }

    [Required, Column("role_id")]
    public int RoleId { get; set; }

    //relasi & kardinalitas
    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }

    [ForeignKey(nameof(AccountNIK))]
    public Account? Account { get; set; }
}
