namespace PriceList.DataAccess.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public virtual ICollection<PriceListData> PriceListData { get; set; } = new List<PriceListData>();
}