namespace PriceList.Contracts;

public class ColumnDescriptionDto
{
    public int PriceListColumnId { get; set; }

    public ColumnNameDto ColumnName { get; set; }

    public ColumnTypeDto ColumnType { get; set; }
}