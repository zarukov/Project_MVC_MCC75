﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_MVC_MCC75.Models;

[Table("tb_tr_profilings")]
public class Profiling
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Required, Column("employee_nik", TypeName="nchar(5)")]
    public string EmployeeNIK { get; set; }

    [Required, Column("education_id")]
    public int EducationId { get; set; }

    //relasi & kardinalitas
    [ForeignKey(nameof(EducationId))]
    public Education? Education { get; set; }

    [ForeignKey(nameof(EmployeeNIK))]
    public Employee? Employee { get; set; }
}
