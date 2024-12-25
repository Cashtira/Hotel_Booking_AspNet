namespace _4._Presentation.Controllers;

using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class InvoiceController(IInvoiceService invoiceService) : ControllerBase
{
    private readonly IInvoiceService invoiceService = invoiceService;

    [HttpGet]
    public async Task<IActionResult> GetAllInvoicesAsync()
    {
        var genres = await this.invoiceService.GetAllInvoicesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceByIdAsync(int id)
    {
        var genre = await this.invoiceService.GetInvoiceByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return HandleResult(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddInvoiceAsync([FromBody] InvoiceDTO invoiceDto)
    {
        if ((!ModelState.IsValid) || (invoiceDto == null))
        {
            return BadRequest(ModelState);
        }

        await this.invoiceService.AddInvoiceAsync(invoiceDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return CreatedAtAction(nameof(GetInvoiceByIdAsync), new { id = invoiceDto.InvoiceId }, invoiceDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoiceAsync(int id, [FromBody] InvoiceDTO invoiceDto)
    {
        if (invoiceDto == null)
        {
            return BadRequest();
        }

        if (id != invoiceDto.InvoiceId)
        {
            return BadRequest("Invalid ID.");
        }

        await this.invoiceService.UpdateInvoiceAsync(invoiceDto).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoiceByIdAsync(int id)
    {
        await this.invoiceService.DeleteInvoiceByIdAsync(id).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return NoContent();
    }
}
