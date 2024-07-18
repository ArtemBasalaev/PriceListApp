using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;

namespace PriceList.BusinessLogic.Handlers;

public class GetPriceListDataHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public GetPriceListDataHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<PriceListDataDto> HandleAsync(int priceListId)
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
                PriceListColumnId = c.Id,
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

        if (priceListData.Count == 0)
        {
            return new PriceListDataDto
            {
                PriceList = new PriceListDto
                {
                    Id = priceList.Id,
                    Name = priceList.Name,
                    CreationDate = priceList.CreationDate
                },
                Columns = priceListColumns,
                ProductsPriceListData = new List<ProductPriceListData>()
            };
        }

        var productsIds = priceListData
            .Select(c => c.ProductId)
            .Distinct()
            .ToList();

        var products = await _priceListDbContext.Product
            .Where(c => productsIds.Contains(c.Id))
            .Select(c => new ProductDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code
            })
            .ToListAsync();

        var recordsIds = priceListData
            .Select(c => c.Id)
            .ToList();

        var textValues = await _priceListDbContext.TextColumnsData
            .Where(v => recordsIds.Contains(v.Id))
            .ToListAsync();

        var integerValues = await _priceListDbContext.IntegerColumnData
            .Where(v => recordsIds.Contains(v.Id))
            .ToListAsync();

        var decimalValues = await _priceListDbContext.DecimalColumnData
            .Where(v => recordsIds.Contains(v.Id))
            .ToListAsync();

        var productsPriceListData = new List<ProductPriceListData>();

        foreach (var product in products)
        {
            var productPriceListData = new ProductPriceListData
            {
                Product = product
            };

            var productData = priceListData
                .Where(p => p.ProductId == product.Id)
                .ToList();

            var columnsData = new List<ProductColumnData>();

            foreach (var data in productData)
            {
                var productColumnData = new ProductColumnData
                {
                    Id = data.Id,
                    PriceListColumnId = data.PriceListColumnId,
                };

                if (data.PriceListColumn.ColumnId == 1)
                {
                    productColumnData.Value = data.Product.Name;
                }
                else if (data.PriceListColumn.ColumnId == 2)
                {
                    productColumnData.Value = data.Product.Code;
                }
                else
                {
                    var columnTypeId = data.PriceListColumn.DataTypeId;
                    var type = (DataTypeEnum)columnTypeId;

                    switch (type)
                    {
                        case DataTypeEnum.Text:
                        case DataTypeEnum.MultiLineText:
                            productColumnData.Value = textValues.FirstOrDefault(d => d.Id == data.Id)?.Value;
                            break;
                        case DataTypeEnum.Integer:
                            productColumnData.Value = integerValues.FirstOrDefault(d => d.Id == data.Id)?.Value;
                            break;
                        case DataTypeEnum.Decimal:
                            productColumnData.Value = decimalValues.FirstOrDefault(d => d.Id == data.Id)?.Value;
                            break;
                    }
                }

                columnsData.Add(productColumnData);
            }

            productPriceListData.ColumnsData = columnsData;
            productsPriceListData.Add(productPriceListData);
        }

        return new PriceListDataDto
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
}