namespace PriceList.DataAccess.Models;

public class PriceListData
{
    public int Id { get; set; }

    public int PriceListColumnId { get; set; }

    public int ProductId { get; set; }

    public DateTime RecordDate { get; set; }

    public virtual PriceListColumn PriceListColumn { get; set; }

    public virtual Product Product { get; set; }

    public virtual DecimalColumnData DecimalColumnData { get; set; }

    public virtual TextColumnData TexColumnData { get; set; }

    public virtual IntegerColumnData IntegerColumnData { get; set; }

}