using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;

namespace PriceList.BusinessLogic.Handlers;

public class GetPriceListsHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public GetPriceListsHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public Task<List<PriceListDto>> HandleAsync()
    {
        return _priceListDbContext.PriceLists
            .Select(c => new PriceListDto
            {
                Id = c.Id,
                Name = c.Name,
                CreationDate = c.CreationDate
            })
            .OrderByDescending(p => p.CreationDate)
            .ToListAsync();
    }
}