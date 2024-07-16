using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;
using PriceListData = PriceList.Contracts.PriceListData;

namespace PriceList.BusinessLogic.Handlers;

public class GetPriceListDataHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public GetPriceListDataHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<PriceListData> HandleAsync(int priceListId)
    {
        var priceList = await _priceListDbContext.PriceLists
            .FirstOrDefaultAsync(pl => pl.Id == priceListId);

        if (priceList == null)
        {
            throw new Exception("Прайс-лист не найден");
        }
        var priceListColumns = await _priceListDbContext.PriceListColumns
            .Where(c => c.PriceListId == priceListId)
            .Select(c => new ColumnDescriptionDto
            {
                ColumnName = new ColumnNameDto
                {
                    Id = c.Column.Id,
                    Name = c.Column.Name
                },
                ColumnType = new ColumnTypeDto
                {
                    Id = c.DataType.Id,
                    Name = c.DataType.TypeName
                }
            })
            .ToListAsync();

        var priceListData = await _priceListDbContext.PriceListData
            .Where(c => c.PriceListColumn.PriceListId == priceListId)
            .ToListAsync();

        var priceListProductsIds = priceListData
            .Select(c => c.ProductId)
            .Distinct()
            .ToList();

        var priceListProducts = await _priceListDbContext.Products
            .Where(c => priceListProductsIds.Contains(c.Id))
            .Select(c => new ProductDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code
            })
            .ToListAsync();

        var productsPriceListData = new List<ProductPriceListData>();

        foreach (var product in priceListProducts)
        {
            var productPriceListData = new ProductPriceListData
            {
                Product = product
            };

            var productColumnsData = priceListData
                .Where(p => p.ProductId == product.Id)
                .ToList();

            var columnsData = new List<ProductColumnData>();

            foreach (var columnData in productColumnsData)
            {
                var columnTypeId = columnData.PriceListColumn.DataTypeId;

                var productColumnData = new ProductColumnData
                {
                    Id = columnData.Id,
                    ColumnTypeId = columnTypeId,
                    ColumnNameId = columnData.PriceListColumn.ColumnId,
                    Value = GetValueAsync(columnTypeId, columnData.Id)
                };

                columnsData.Add(productColumnData);
            }

            productPriceListData.ColumnsData = columnsData;
            productsPriceListData.Add(productPriceListData);
        }

        return new PriceListData
        {
            PriceList = new PriceListDto
            {
                Id = priceList.Id,
                Name = priceList.Name,
                CreationDate = priceList.CreationDate
            },
            Columns = priceListColumns,
            ProductsPriceListData = productsPriceListData
        };
    }

    private async Task<object?> GetValueAsync(int typeId, int columnDataId)
    {
        var type = (DataType)typeId;

        if (type == DataType.Text || type == DataType.MultiLineText)
        {
            var data = await _priceListDbContext.TextColumnsData
                .FirstOrDefaultAsync(d => d.Id == columnDataId);

            if (data != null)
            {
                return data.Value;
            }
        }

        if (type == DataType.Integer)
        {
            var data = await _priceListDbContext.IntegerColumnData
                .FirstOrDefaultAsync(d => d.Id == columnDataId);

            if (data != null)
            {
                return data.Value;
            }
        }

        if (type == DataType.Decimal)
        {
            var data = await _priceListDbContext.DecimalColumnData
                .FirstOrDefaultAsync(d => d.Id == columnDataId);

            if (data != null)
            {
                return data.Value;
            }
        }

        return default;
    }
}