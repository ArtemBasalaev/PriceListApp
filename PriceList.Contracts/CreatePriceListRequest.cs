namespace PriceList.Contracts;

public class CreatePriceListRequest
{
    public string Name { get; set; }

    public List<ColumnDescriptionDto> Columns { get; set; }
}