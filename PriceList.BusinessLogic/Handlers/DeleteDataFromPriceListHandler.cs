using PriceList.Contracts;
using PriceList.DataAccess;
using PriceList.DataAccess.Models;

namespace PriceList.BusinessLogic.Handlers;

public class DeleteDataFromPriceListHandler : IHandler
{
    private readonly PriceListDbContext _priceListDbContext;

    public DeleteDataFromPriceListHandler(PriceListDbContext priceListDbContext)
    {
        _priceListDbContext = priceListDbContext ?? throw new ArgumentNullException(nameof(priceListDbContext));
    }

    public async Task<BaseResponse> HandleAsync(int[] ids)
    {
        var dataToDelete = ids
            .Select(id => new PriceListData
            {
                Id = id
            });

        _priceListDbContext.PriceListData.RemoveRange(dataToDelete);
        await _priceListDbContext.SaveChangesAsync();

        return BaseResponse.GetSuccessResponse("Данные успешно удалены");
    }
}