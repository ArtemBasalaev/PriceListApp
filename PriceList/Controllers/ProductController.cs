using Microsoft.AspNetCore.Mvc;
using PriceList.BusinessLogic.Handlers;
using PriceList.Contracts;

namespace PriceList.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    [HttpPost]
    public Task<ProductDto> AddNewProduct([FromServices] AddNewProductHandler addNewProductHandler, AddNewProductRequest request)
    {
        return addNewProductHandler.HandleAsync(request);
    }
}