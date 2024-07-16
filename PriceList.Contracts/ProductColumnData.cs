namespace PriceList.Contracts;

public class ProductColumnData
{
    public int Id { get; set; }

    public int ColumnTypeId { get; set; }

    public int ColumnNameId { get; set; }

    public object Value { get; set; }
}