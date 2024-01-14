using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using controle_financeiro.View;

namespace controle_financeiro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : FlyoutPage
    {
        public Menu()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Detail = new NavigationPage(new MainPage());
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as MenuFlyoutMenuItem;
                if (item == null)
                    return;

                IsPresented = false;
                FlyoutPage.ListView.SelectedItem = null;

                Page page = null;

                switch (item.PageName)
                {
                    case "Cadastrar despesa":
                        page = new Cadastrar();
                        break;

                    case "Listar despesas":
                        page = new Gestao();
                        break;

                    case "Logout":
                        page = new Login();
                        NavigationPage.SetHasNavigationBar(page, false); 
                        NavigationPage.SetHasBackButton(page, false); 
                        break;

                }

                if (page != null)
                {
                    if (Detail is NavigationPage navigationPage)
                    {

                        navigationPage.Navigation.InsertPageBefore(page, navigationPage.Navigation.NavigationStack[0]);
                        await navigationPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        Detail = new NavigationPage(page);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}