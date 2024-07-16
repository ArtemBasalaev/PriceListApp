using Microsoft.EntityFrameworkCore;
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
                TypeName = "Однострочный текст"
            },
            new()
            {
                TypeName = "Многострочный текст"
            },
            new()
            {
                TypeName = "Целое число"
            },
            new()
            {
                TypeName = "Десятичное число"
            }
        };

        _dbContext.DataTypes.AddRange(dataTypes);
        await _dbContext.SaveChangesAsync();
    }
}