namespace PriceList.DataAccess.Models;

public class IntegerColumnData
{
    public int Id { get; set; }
    
    public int Value { get; set; }

    public virtual PriceListData PriceListData { get; set; }
}