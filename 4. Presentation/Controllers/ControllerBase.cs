namespace _4._Presentation.Controllers;

using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;
using ApiControllerAttribute = Microsoft.AspNetCore.Mvc.ApiControllerAttribute;

[ApiController]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected IActionResult HandleResult<T>(T? result) where T : class
    {
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
