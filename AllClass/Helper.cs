using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Trade.AllPages;

namespace Trade.AllClass
{
    public class Helper
    {
        public static Frame MainFrame= new Frame();
        public static TextBlock TbHeader = new TextBlock();
        public static String Role = "Гость";
        private static TradeEntities TradeEntities = new TradeEntities();
        public static TradeEntities GetData()
        {
            if (TradeEntities == null)
            {
                TradeEntities = new TradeEntities();
            }
            return TradeEntities;
        }
    }
}
