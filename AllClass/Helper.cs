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
        public static TextBlock TbFIO = new TextBlock();
        public static Button BtnBack = new Button();
        public static Button BtnExitAccaunt = new Button();
        public static Int32 levelPageActive = 0;
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
