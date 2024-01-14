using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace controle_financeiro
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var animation = new Animation();

            animation.Add(0, 1, new Animation(callback: d => foto.Scale = d, start: 1, end: 2));
            animation.Add(0, 1, new Animation(callback: d => foto.Opacity = d, start: 1, end: 0.5));

            // Defina a duração da animação (em milissegundos)
            animation.Commit(foto, "ImageAnimation", length: 2000, easing: Easing.Linear);

            // Aguarde a conclusão da animação
            await Task.Delay(2000);

            // Reverta a animação
            await foto.ScaleTo(1, 500, Easing.Linear);
            await foto.FadeTo(1, 500, Easing.Linear);
        }
    }
}
