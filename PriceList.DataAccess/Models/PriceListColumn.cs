namespace PriceList.DataAccess.Models;

public class PriceListColumn
{
    public int Id { get; set; }

    public int PriceListId { get; set; }

    public int ColumnId { get; set; }

    public int DataTypeId { get; set; }

    public virtual Column Column { get; set; }

    public virtual PriceList PriceList { get; set; }

    public virtual DataType DataType { get; set; }

    public virtual ICollection<PriceListData> PriceListData { get; set; } = new List<PriceListData>();
}