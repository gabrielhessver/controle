using controle_financeiro.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace controle_financeiro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuFlyout : ContentPage
    {
        public ListView ListView;

        public MenuFlyout()
        {
            InitializeComponent();  

            BindingContext = new MenuFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class MenuFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuFlyoutMenuItem> MenuItems { get; set; }

            public MenuFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MenuFlyoutMenuItem>(new[]
                {
                    new MenuFlyoutMenuItem { Id = 0, Title = "Cadastrar despesa" , PageName = "Cadastrar despesa", TargetType = typeof(Cadastrar), ImageSource = "register.png", },
                    new MenuFlyoutMenuItem { Id = 1, Title = "Listar despesas" , PageName = "Listar despesas", TargetType = typeof(Gestao), ImageSource = "list.png" },
                    new MenuFlyoutMenuItem { Id = 2, Title = "Logout", PageName = "Logout", TargetType = typeof(Login), ImageSource = "logout.png" },                    
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}