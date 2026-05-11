namespace APBD_Cw7_s32570.Services;

using Microsoft.EntityFrameworkCore;
using APBD_Cw7_s32570.DTOs;
using APBD_Cw7_s32570.Exceptions;
using APBD_Cw7_s32570.Infrastructure;
using APBD_Cw7_s32570.Models;

public class PcService(AppDbContext context) : IPcService
{
    public async Task<IEnumerable<PcResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.PCs
            .Select(pc => new PcResponse(
                pc.Id,
                pc.Name,
                pc.Weight,
                pc.Warranty,
                pc.CreatedAt,
                pc.Stock
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<PcDetailsResponse> GetByIdWithComponentsAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.PCs
            .Where(pc => pc.Id == id)
            .Select(pc => new PcDetailsResponse(
                pc.Id,
                pc.Name,
                pc.Weight,
                pc.Warranty,
                pc.CreatedAt,
                pc.Stock,
                pc.PCComponents.Select(pcComponent => new PcComponentResponse(
                    pcComponent.Amount,
                    new ComponentResponse(
                        pcComponent.Component.Code,
                        pcComponent.Component.Name,
                        pcComponent.Component.Description,
                        new ComponentManufacturerResponse(
                            pcComponent.Component.Manufacturer.Id,
                            pcComponent.Component.Manufacturer.Abbreviation,
                            pcComponent.Component.Manufacturer.FullName,
                            pcComponent.Component.Manufacturer.FoundationDate
                        ),
                        new ComponentTypeResponse(
                            pcComponent.Component.Type.Id,
                            pcComponent.Component.Type.Abbreviation,
                            pcComponent.Component.Type.Name
                        )
                    )
                ))
            ))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"PC with id {id} not found");
    }

    public async Task<PcResponse> AddAsync(
        CreatePcRequest request,
        CancellationToken cancellationToken)
    {
        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        context.PCs.Add(pc);
        await context.SaveChangesAsync(cancellationToken);

        return new PcResponse(
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        );
    }

    public async Task UpdateAsync(
        int id,
        UpdatePcRequest request,
        CancellationToken cancellationToken)
    {
        var affectedRows = await context.PCs
            .Where(pc => pc.Id == id)
            .ExecuteUpdateAsync(setters => setters
                    .SetProperty(pc => pc.Name, request.Name)
                    .SetProperty(pc => pc.Weight, request.Weight)
                    .SetProperty(pc => pc.Warranty, request.Warranty)
                    .SetProperty(pc => pc.CreatedAt, request.CreatedAt)
                    .SetProperty(pc => pc.Stock, request.Stock),
                cancellationToken
            );

        if (affectedRows == 0)
        {
            throw new NotFoundException($"PC with id {id} not found");
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var affectedRows = await context.PCs
            .Where(pc => pc.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            throw new NotFoundException($"PC with id {id} not found");
        }
    }
}