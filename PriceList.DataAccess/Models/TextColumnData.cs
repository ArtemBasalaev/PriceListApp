namespace PriceList.DataAccess.Models;

public class TextColumnData
{
    public int Id { get; set; }

    public string Value { get; set; }

    public virtual PriceListData PriceListData { get; set; }
}