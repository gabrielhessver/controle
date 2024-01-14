using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace controle_financeiro
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpAnexoControle : Rg.Plugins.Popup.Pages.PopupPage
    {
        public bool opcao;
        TaskCompletionSource<bool> _tcs = null;
        public bool Opcao
        {
            get { return opcao; }   // get method
            set { opcao = value; }  // set method
        }
        public PopUpAnexoControle ()
		{
			InitializeComponent ();
		}

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {
            opcao = false;
            _tcs?.SetResult(true);

            await PopupNavigation.PopAllAsync();
        }

        private async void BtnGaleria_Clicked(object sender, EventArgs e)
        {
            opcao = true;
            _tcs?.SetResult(true);

            await PopupNavigation.PopAllAsync();
        }
        public async Task<bool> Show()
        {
            _tcs = new TaskCompletionSource<bool>();

            return await _tcs.Task;
        }
    }
}