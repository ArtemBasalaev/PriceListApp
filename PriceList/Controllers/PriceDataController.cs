using Microsoft.AspNetCore.Mvc;
using PriceList.BusinessLogic.Handlers;
using PriceList.Contracts;

namespace PriceList.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class PriceDataController
{
    [HttpPost]
    public Task<BaseResponse> AddDataInPriceList([FromServices] AddDataInPriceList addDataInPriceList, AddNewDataToPriceListRequest request)
    {
        return addDataInPriceList.HandleAsync(request);
    }
}