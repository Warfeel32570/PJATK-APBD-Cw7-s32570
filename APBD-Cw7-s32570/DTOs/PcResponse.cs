namespace APBD_Cw7_s32570.DTOs;

public record PcResponse(
    int Id,
    string Name,
    double Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock
);