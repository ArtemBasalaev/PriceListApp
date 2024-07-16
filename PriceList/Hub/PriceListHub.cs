using Microsoft.AspNetCore.SignalR;

namespace PriceList.Hub;

public class PriceListHub : Hub<IPriceListHubClient>
{
    public Task NotifyPriceListCreated(string priceListName, string userName)
    {
        return Clients.All.PriceListCreated(priceListName, userName);
    }
}