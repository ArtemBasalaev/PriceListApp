using Microsoft.AspNetCore.Mvc;
using PriceList.BusinessLogic.Handlers;
using PriceList.Contracts;

namespace PriceList.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class ColumnController : ControllerBase
{
    [HttpGet]
    public Task<List<ColumnNameDto>> GetExistingColumns([FromServices] GetColumnNameHandler getColumnNameHandler)
    {
        return getColumnNameHandler.HandleAsync();
    }
}