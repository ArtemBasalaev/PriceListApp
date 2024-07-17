using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;
using PriceList.DataAccess.Models;
using DataType = PriceList.Contracts.DataType;

namespace PriceList.BusinessLogic.Handlers;

public class AddDataInPriceList : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public AddDataInPriceList(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<BaseResponse> HandleAsync(AddNewDataToPriceListRequest request)
    {
        var productId = request.ProductId;
        var productData = request.columnsData;

        var newRecords = productData
            .Select(columnData => new PriceListData
            {
                PriceListColumnId = columnData.PriceListColumnId,
                ProductId = productId,
                RecordDate = DateTime.Now
            })
            .ToList();

        _priceListDbContext.PriceListData.AddRange(newRecords);
        await _priceListDbContext.SaveChangesAsync();

        var columnsIds = productData
            .Select(c => c.PriceListColumnId)
            .ToList();

        var columnTypes = await _priceListDbContext.PriceListColumns
            .Where(c => columnsIds.Contains(c.Id))
            .Select(c => new
            {
                PriceListColumnId = c.Id,
                DataTypeId = c.DataTypeId
            })
            .ToDictionaryAsync(k => k.PriceListColumnId, v => v.DataTypeId);

        var textValues = new List<TextColumnData>();
        var integerValues = new List<IntegerColumnData>();
        var decimalValues = new List<DecimalColumnData>();

        for (var i = 0; i < newRecords.Count; i++)
        {
            var priceListColumnId = newRecords[i].PriceListColumnId;

            if (!columnTypes.ContainsKey(priceListColumnId))
            {
                throw new Exception("Ошибка при вставке данных");
            }

            var dataType = (DataType)columnTypes[priceListColumnId];

            var dataRecordId = newRecords[i].Id;
            
            if (dataType is DataType.Text or DataType.MultiLineText && productData[i].Value is string textValue)
            {
                textValues.Add(new TextColumnData
                {
                    Id = dataRecordId,
                    Value = textValue
                });
            }
            else if (dataType == DataType.Integer && productData[i].Value is int integerValue)
            {
                integerValues.Add(new IntegerColumnData
                {
                    Id = dataRecordId,
                    Value = integerValue
                });
            }
            else if (dataType == DataType.Decimal && productData[i].Value is decimal decimalValue)
            {
                decimalValues.Add(new DecimalColumnData
                {
                    Id = dataRecordId,
                    Value = decimalValue
                });
            }
            else
            {
                throw new Exception("Неизвестный тип данных");
            }
        }

        _priceListDbContext.TextColumnsData.AddRange(textValues);
        _priceListDbContext.IntegerColumnData.AddRange(integerValues);
        _priceListDbContext.DecimalColumnData.AddRange(decimalValues);

        return BaseResponse.GetSuccessResponse("Данные успешно сохранены");
    }
}