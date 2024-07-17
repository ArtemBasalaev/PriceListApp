namespace PriceList.Contracts;

public class PriceListDataDto
{
    public PriceListDto PriceList { get; set; }
    
    public List<ColumnDescriptionDto> Columns { get; set; }

    public List<ProductPriceListData> ProductsPriceListData { get; set; }
}