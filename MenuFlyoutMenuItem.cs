using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controle_financeiro
{
    public class MenuFlyoutMenuItem
    {
        public MenuFlyoutMenuItem()
        {
            TargetType = typeof(MenuFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        public Type TargetType { get; set; }
        public string ImageSource { get; set; }
    }
}