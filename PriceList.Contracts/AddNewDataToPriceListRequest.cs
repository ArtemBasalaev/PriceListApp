namespace PriceList.Contracts;

public class AddNewDataToPriceListRequest
{
    public int ProductId { get; set; }

    public List<PriceListColumnData> columnsData { get; set; }

}