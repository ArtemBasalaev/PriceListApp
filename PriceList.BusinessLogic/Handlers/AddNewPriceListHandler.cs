using PriceList.Contracts;
using PriceList.DataAccess;
using PriceList.DataAccess.Models;

namespace PriceList.BusinessLogic.Handlers;

public class AddNewPriceListHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public AddNewPriceListHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<BaseResponse> HandleAsync(AddNewPriceListRequest request)
    {
        var requestedColumns = new List<ColumnDescriptionDto>
        {
            new()
            {
                ColumnName = new() { Id = 1 },
                ColumnType = new() { Id = 1 }
            },
            new()
            {
                ColumnName = new() { Id = 2 },
                ColumnType = new() { Id = 1 }
            }
        };

        requestedColumns.AddRange(request.Columns);

        var newRequestedColumns = requestedColumns
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

        await using var transaction = await _priceListDbContext.Database.BeginTransactionAsync();
        await _priceListDbContext.SaveChangesAsync();

        var priceListColumns = requestedColumns
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
        await transaction.CommitAsync();

        return BaseResponse.GetSuccessResponse("Прайс лист успешно сохранен");
    }
}