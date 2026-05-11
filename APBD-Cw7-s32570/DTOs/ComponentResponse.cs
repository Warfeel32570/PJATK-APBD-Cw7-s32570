namespace APBD_Cw7_s32570.DTOs;

public record ComponentResponse(
    string Code,
    string Name,
    string Description,
    ComponentManufacturerResponse Manufacturer,
    ComponentTypeResponse Type
);