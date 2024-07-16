namespace PriceList.Contracts;

public class PriceListColumnDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreationDate { get; set; }

    public List<ColumnDescriptionDto> Columns { get; set; }
}