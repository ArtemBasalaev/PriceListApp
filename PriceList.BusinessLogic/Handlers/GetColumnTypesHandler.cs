using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;

namespace PriceList.BusinessLogic.Handlers;

public class GetColumnTypesHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public GetColumnTypesHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public Task<List<ColumnTypeDto>> HandleAsync()
    {
        return _priceListDbContext.DataTypes
            .Select(c => new ColumnTypeDto
            {
                Id = c.Id,
                Name = c.TypeName
            })
            .ToListAsync();
    }
}