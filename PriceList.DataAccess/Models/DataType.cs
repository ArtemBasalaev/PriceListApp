namespace PriceList.DataAccess.Models;

public class DataType
{
    public int Id { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<PriceListColumn> PriceListColumns { get; set; } = new List<PriceListColumn>();
}