using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;

namespace PriceList.BusinessLogic.Handlers;

public class GetColumnNameHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public GetColumnNameHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public Task<List<ColumnNameDto>> HandleAsync()
    {
        return _priceListDbContext.Columns
            .Where(c => c.Id != 1 && c.Id != 2)
            .Select(c => new ColumnNameDto
            {
                Id = c.Id,
                Name = c.Name
            })

            .ToListAsync();
    }
}