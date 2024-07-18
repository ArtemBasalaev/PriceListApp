using Microsoft.EntityFrameworkCore;
using PriceList.Contracts;
using PriceList.DataAccess;
using PriceList.DataAccess.Models;

namespace PriceList;

public class DbInitializer : IDbInitializer
{
    private readonly PriceListDbContext _dbContext;

    public DbInitializer(PriceListDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.MigrateAsync();

        if (_dbContext.DataTypes.Any())
        {
            return;
        }

        var dataTypes = new List<DataType>
        {
            new()
            {
                Id = (int)DataTypeEnum.Text,
                TypeName = "Однострочный текст"
            },
            new()
            {
                Id = (int)DataTypeEnum.MultiLineText,
                TypeName = "Многострочный текст"
            },
            new()
            {
                Id = (int)DataTypeEnum.Integer,
                TypeName = "Целое число"
            },
            new()
            {
                Id = (int)DataTypeEnum.Decimal,
                TypeName = "Десятичное число"
            }
        };

        _dbContext.DataTypes.AddRange(dataTypes);
        await _dbContext.SaveChangesAsync();
    }
}