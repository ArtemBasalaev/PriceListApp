namespace PriceList.Contracts;

public class ProductPriceListData
{
    public ProductDto Product { get; set; }

    public List<ProductColumnData> ColumnsData { get; set; }
}