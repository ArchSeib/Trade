//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trade
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Order = new HashSet<Order>();
        }
    
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPhoto { get; set; }
        public BitmapImage PathPhoto
        {
            set
            {
                if (ProductPhoto == null)
                {
                    this.PathPhoto = new BitmapImage(new Uri("/Resources/picture.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    this.PathPhoto = new BitmapImage(new Uri(ProductPhoto, UriKind.Relative));
                }
            }
            get { return this.PathPhoto; }
        }
        public string ProductManufacturer { get; set; }
        public decimal ProductCost { get; set; }
        public Nullable<byte> ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
        public string Unit { get; set; }
        public byte MaxDiscountAmount { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> MinCount { get; set; }
        public Nullable<int> CountPack { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
