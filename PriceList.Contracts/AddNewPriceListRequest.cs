namespace PriceList.Contracts;

public class AddNewPriceListRequest
{
    public string Name { get; set; }

    public List<ColumnDescriptionDto> Columns { get; set; }
}