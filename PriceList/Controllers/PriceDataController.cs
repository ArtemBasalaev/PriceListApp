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
    public Task<BaseResponse> AddDataToPriceList([FromServices] AddDataToPriceListHandler addDataToPriceListHandler, AddNewDataToPriceListRequest request)
    {
        return addDataToPriceListHandler.HandleAsync(request);
    }

    [HttpDelete]
    public Task<BaseResponse> DeleteDataFromPriceList([FromServices] DeleteDataFromPriceListHandler deleteDataFromPriceListHandler, int[] ids)
    {
        return deleteDataFromPriceListHandler.HandleAsync(ids);
    }
}