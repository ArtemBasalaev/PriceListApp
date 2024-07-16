namespace PriceList.DataAccess.Models;

public class PriceList
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<PriceListColumn> PriceListColumns { get; set; } = new List<PriceListColumn>();
}