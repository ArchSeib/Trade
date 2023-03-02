using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Trade.AllClass
{
    public partial class Product
    {
        public string ColorCodeDiscount { get; set; }
        public Decimal ProductRealCost { get; set; }
        public BitmapImage PathPhoto { get; set; }
        public Int32 CountInOrder { get; set; }
    }
}
