namespace APBD_Cw7_s32570.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("PCComponents")]
[PrimaryKey(nameof(PCId), nameof(ComponentCode))]
public class PCComponent
{
    public int PCId { get; set; }

    [Column(TypeName = "char(10)")]
    public string ComponentCode { get; set; } = string.Empty;

    public int Amount { get; set; }

    [ForeignKey(nameof(PCId))]
    public PC PC { get; set; } = null!;

    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null!;
}