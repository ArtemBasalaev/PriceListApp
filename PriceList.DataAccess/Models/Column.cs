namespace PriceList.DataAccess.Models;

public class Column
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PriceListColumn> PriceListColumns { get; set; } = new List<PriceListColumn>();
}