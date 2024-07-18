using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;
using PriceList.DataAccess.Models;
using DataTypeEnum = PriceList.Contracts.DataTypeEnum;

namespace PriceList.BusinessLogic.Handlers;

public class AddDataToPriceListHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    private readonly Dictionary<DataTypeEnum, JsonValueKind> _dataTypes = new()
    {
        [DataTypeEnum.Text] = JsonValueKind.String,
        [DataTypeEnum.MultiLineText] = JsonValueKind.String,
        [DataTypeEnum.Integer] = JsonValueKind.Number,
        [DataTypeEnum.Decimal] = JsonValueKind.Number
    };

    public AddDataToPriceListHandler(PriceListDbContext priceListDbContext)
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

        await using var transaction = await _priceListDbContext.Database.BeginTransactionAsync();

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

            if (!columnTypes.ContainsKey(priceListColumnId) && productData[i].Value is not JsonElement)
            {
                throw new Exception("Ошибка при вставке данных");
            }

            var value = (JsonElement)productData[i].Value;
            var dataType = (DataTypeEnum)columnTypes[priceListColumnId];
            var dataRecordId = newRecords[i].Id;

            if (_dataTypes[dataType] == JsonValueKind.String)
            {
                textValues.Add(new TextColumnData
                {
                    Id = dataRecordId,
                    Value = value.GetString()
                });
            }
            else if (_dataTypes[dataType] == JsonValueKind.Number)
            {
                if (dataType == DataTypeEnum.Integer)
                {
                    integerValues.Add(new IntegerColumnData
                    {
                        Id = dataRecordId,
                        Value = value.GetInt32()
                    });
                }
                else if (dataType == DataTypeEnum.Decimal)
                {
                    decimalValues.Add(new DecimalColumnData
                    {
                        Id = dataRecordId,
                        Value = value.GetDecimal()
                    });
                }
            }
            else
            {
                throw new Exception("Неизвестный тип данных");
            }
        }

        _priceListDbContext.TextColumnsData.AddRange(textValues);
        _priceListDbContext.IntegerColumnData.AddRange(integerValues);
        _priceListDbContext.DecimalColumnData.AddRange(decimalValues);

        await _priceListDbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return BaseResponse.GetSuccessResponse("Данные успешно сохранены");
    }
}