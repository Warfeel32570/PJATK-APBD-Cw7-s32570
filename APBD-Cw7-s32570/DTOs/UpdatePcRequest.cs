namespace APBD_Cw7_s32570.DTOs;

using System.ComponentModel.DataAnnotations;

public class UpdatePcRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public double Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }
}