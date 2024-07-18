namespace PriceList.Contracts;

public class ProductColumnData
{
    public int Id { get; set; }

    public int PriceListColumnId { get; set; }

    public object? Value { get; set; }
}