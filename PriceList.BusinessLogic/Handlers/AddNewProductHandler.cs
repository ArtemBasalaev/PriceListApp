using PriceList.Contracts;
using PriceList.DataAccess.Models;
using PriceList.DataAccess;

namespace PriceList.BusinessLogic.Handlers;

public class AddNewProductHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public AddNewProductHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<ProductDto> HandleAsync(AddNewProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Code))
        {
            throw new Exception("Название и код должны быть заполнены");
        }

        var newProduct = new Product
        {
            Name = request.Name,
            Code = request.Code
        };

        _priceListDbContext.Product.Add(newProduct);

        await _priceListDbContext.SaveChangesAsync();

        return new ProductDto
        {
            Id = newProduct.Id,
            Name = newProduct.Name,
            Code = newProduct.Code
        };
    }
}