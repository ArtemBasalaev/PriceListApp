﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public Task<BaseResponse> SavePriceList([FromServices] SavePriceListHandler savePriceListHandler, CreatePriceListRequest request)
    {
        return savePriceListHandler.HandleAsync(request);
    }
}