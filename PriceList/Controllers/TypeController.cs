using Microsoft.AspNetCore.Mvc;
using PriceList.BusinessLogic.Handlers;
using PriceList.Contracts;

namespace PriceList.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class TypeController : ControllerBase
{
    [HttpGet]
    public Task<List<ColumnTypeDto>> GetExistingColumns([FromServices] GetColumnTypesHandler getColumnTypesHandler)
    {
        return getColumnTypesHandler.HandleAsync();
    }
}