namespace PriceList.DataAccess.Models;

public class DecimalColumnData
{
    public int Id { get; set; }

    public decimal Value { get; set; }

    public virtual PriceListData PriceListData { get; set; }
}