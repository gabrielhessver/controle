using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace controle_financeiro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupFotoView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupFotoView()
        {
            InitializeComponent();
        }
        public Image PopImage
        {
            get { return popImage; }
        }
    }
}