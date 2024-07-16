using PriceList.Contracts;
using PriceList.DataAccess;
using PriceList.DataAccess.Models;

namespace PriceList.BusinessLogic.Handlers;

public class SavePriceListHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public SavePriceListHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<BaseResponse> HandleAsync(CreatePriceListRequest request)
    {
        var newRequestedColumns = request.Columns
            .Where(c => c.ColumnName.Id == 0)
            .ToList();

        var newColumns = new Queue<Column>(newRequestedColumns
            .Select(c => new Column
            {
                Name = c.ColumnName.Name
            }));

        if (newColumns.Count != 0)
        {
            _priceListDbContext.Columns.AddRange(newColumns);
        }

        var newPriceList = new DataAccess.Models.PriceList
        {
            Name = request.Name,
            CreationDate = DateTime.Now
        };

        _priceListDbContext.PriceLists.Add(newPriceList);
        await _priceListDbContext.SaveChangesAsync();

        var priceListColumns = request.Columns
            .Select(column => new PriceListColumn
            {
                PriceListId = newPriceList.Id,
                DataTypeId = column.ColumnType.Id,
                ColumnId = column.ColumnName.Id != 0
                    ? column.ColumnName.Id
                    : newColumns.Dequeue().Id
            })
            .ToList();

        _priceListDbContext.PriceListColumns.AddRange(priceListColumns);
        await _priceListDbContext.SaveChangesAsync();

        return BaseResponse.GetSuccessResponse("Прайс лист успешно сохранен");
    }
}