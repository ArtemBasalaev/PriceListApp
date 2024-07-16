namespace PriceList.Hub;

public interface IPriceListHubClient
{
    Task PriceListCreated(string priceListName, string userName);
}