using Microsoft.AspNetCore.Mvc;
using PriceList.BusinessLogic.Handlers;
using PriceList.Contracts;

namespace PriceList.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class PriceListController : ControllerBase
{
    [HttpGet]
    public Task<List<PriceListDto>> GetPriceLists([FromServices] GetPriceListsHandler getPriceListsHandler)
    {
        return getPriceListsHandler.HandleAsync();
    }

    [HttpGet("{priceListId}")]
    public Task<PriceListDataDto> GetPriceList([FromServices] GetPriceListDataHandler getPriceListDataHandler, int priceListId)
    {
        return getPriceListDataHandler.HandleAsync(priceListId);
    }

    [HttpPost]
    public Task<BaseResponse> AddNewPriceList([FromServices] AddNewPriceListHandler addNewPriceListHandler, AddNewPriceListRequest request)
    {
        return addNewPriceListHandler.HandleAsync(request);
    }
}