namespace APBD_Cw7_s32570.Controllers;

using Microsoft.AspNetCore.Mvc;
using APBD_Cw7_s32570.DTOs;
using APBD_Cw7_s32570.Exceptions;
using APBD_Cw7_s32570.Services;

[ApiController]
[Route("api/pcs")]
public class PcsController(IPcService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var pcs = await service.GetAllAsync(cancellationToken);
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetByIdWithComponents(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var pc = await service.GetByIdWithComponentsAsync(id, cancellationToken);
            return Ok(pc);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] CreatePcRequest request,
        CancellationToken cancellationToken)
    {
        var pc = await service.AddAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetByIdWithComponents),
            new { id = pc.Id },
            pc
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdatePcRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await service.UpdateAsync(id, request, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            await service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}