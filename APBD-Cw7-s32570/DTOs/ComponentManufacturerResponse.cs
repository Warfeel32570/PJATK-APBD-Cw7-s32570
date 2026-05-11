namespace APBD_Cw7_s32570.DTOs;

public record ComponentManufacturerResponse(
    int Id,
    string Abbreviation,
    string FullName,
    DateOnly FoundationDate
);