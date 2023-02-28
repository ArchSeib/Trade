using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Trade.AllClass
{
    public class ProductItem
    {
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductManufacturer { get; set; }
        public decimal ProductCost { get; set; }
        public decimal CostNow { get; set; }
        public Int32 ColorCodeDiscount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
        public string Unit { get; set; }
        public byte MaxDiscountAmount { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> MinCount { get; set; }
        public Nullable<int> CountPack { get; set; }
        public string ProductPhoto { get; set; }
        public BitmapImage PathPhoto => new BitmapImage(new Uri(ProductPhoto, UriKind.Relative));
    }
}
