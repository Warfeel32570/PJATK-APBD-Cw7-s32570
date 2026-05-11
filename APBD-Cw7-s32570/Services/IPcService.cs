namespace APBD_Cw7_s32570.Services;

using APBD_Cw7_s32570.DTOs;

public interface IPcService
{
    Task<IEnumerable<PcResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<PcDetailsResponse> GetByIdWithComponentsAsync(int id, CancellationToken cancellationToken);
    Task<PcResponse> AddAsync(CreatePcRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(int id, UpdatePcRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}