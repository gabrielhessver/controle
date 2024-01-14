using System;
using System.IO;
using controle_financeiro.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace controle_financeiro
{
    public partial class App : Application
    {
        static SQLiteCadastro dbControle;
        private static SQLiteUsuario dbUsuario;
        public App()
        {
            InitializeComponent();

            App.Current.MainPage = new NavigationPage(new Login());
        }
        public static SQLiteCadastro SQLiteDbControle
        {
            get
            {
                if (dbControle == null)
                {
                    dbControle = new SQLiteCadastro(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "controle_financeiro.db3"));
                }
                return dbControle;
            }
        }

        public static SQLiteUsuario SQLiteDbUsuario
        {
            get
            {
                if (dbUsuario == null)
                {
                    dbUsuario = new SQLiteUsuario(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "controle_financeiro_usuario.db3"));
                }
                return dbUsuario;
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
