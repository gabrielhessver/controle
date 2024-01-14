using controle_financeiro.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.IO;

namespace controle_financeiro
{
    public partial class Gestao : ContentPage
    {
        public ObservableCollection<Cadastro> Aux { get; set; }
        public Cadastro SelectedItem { get; set; }
        private Cadastro itemEditar { get; set; }
        public ICommand OnItemSwipedCommand { get; set; }

        public Gestao()
        {
            InitializeComponent();

            OnItemSwipedCommand = new Command(OnItemSwiped);

            // Chame o método de leitura de elementos de forma assíncrona
            LerElementosAsync();

            var titleLabel = new Label
            {
                Text = "Lançamentos",
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.White
            };

            var titleView = new ContentView
            {
                Content = titleLabel,
                Padding = new Thickness(10, 0)
            };

            NavigationPage.SetTitleView(this, titleView);
        }

        private async void LerElementosAsync()
        {
            // Leia os elementos do banco de dados de forma assíncrona
            Aux = new ObservableCollection<Cadastro>(await App.SQLiteDbControle.ReadElementos());

            // Defina o contexto de dados da página para si mesma
            BindingContext = this;

            // Exibir os itens do Picker
            ExibirItensPicker();
        }

        private void ExibirItensPicker()
        {
            foreach (var item in Aux)
            {
                var swipeView = new SwipeView();
                var swipeContent = new Grid();

                var swipeGestureRecognizer = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
                swipeGestureRecognizer.Command = OnItemSwipedCommand;
                swipeGestureRecognizer.CommandParameter = item;
                swipeContent.GestureRecognizers.Add(swipeGestureRecognizer);

                var swipeItems = new SwipeItems
        {
                    new SwipeItem { Text = "Excluir", BackgroundColor = Color.FromHex("#7CCD7C"), Command = new Command(() => ExcluirItem(item)) },
                    new SwipeItem { Text = "Editar", BackgroundColor = Color.FromHex("#CEE8C3"), Command = new Command(() => EditarItem(item)) },
                    new SwipeItem { Text = "Anexo", BackgroundColor = Color.FromHex("#7CCD7C"), Command = new Command(() => AnexarItem(item)) }
            
        };

                swipeView.Content = swipeContent;
                swipeView.RightItems = swipeItems;

                var label = new Label
                {
                    Text = $" ID: {item.ID}\n Tipo de Despesa: {item.TipoDespesa}\n Data de Despesa: {item.DataDespesa}\n Valor: {item.Valor}",
                    FontSize = 20,
                    Margin = new Thickness(1, 10),
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("#4FC143"),                  
                };

                swipeContent.Children.Add(label);
                lancamentosLayout.Children.Add(swipeView);
            }
        }

        private async void ExibirDetalhes(Cadastro item)
        {
            int ID = item.ID;
            string tipoDespesa = item.TipoDespesa;
            string dataDespesa = item.DataDespesa;
            string valor = item.Valor;
            string descricao = item.Descricao;

            // Exibir os dados obtidos do item selecionado
            await DisplayAlert("Detalhes do Lançamento", $"ID: {ID}\nTipo de Despesa: {tipoDespesa}\nData: {dataDespesa}\nValor: {valor}\nDescrição: {descricao}", "OK");
        }

        private async void ExcluirItem(Cadastro item)
        {
            bool confirmarExclusao = await DisplayAlert("Confirmação", "Deseja excluir este item?", "Sim", "Não");
            if (confirmarExclusao)
            {
                Aux.Remove(item);
                await App.SQLiteDbControle.DeleteElementoAsync(item);

                foreach (var child in lancamentosLayout.Children)
                {
                    if (child is SwipeView swipeView && swipeView.Content is Grid swipeContent && swipeContent.GestureRecognizers.FirstOrDefault() is SwipeGestureRecognizer swipeGestureRecognizer && swipeGestureRecognizer.CommandParameter == item)
                    {
                        lancamentosLayout.Children.Remove(swipeView);
                        break;
                    }
                }
            }
        }

        private void EditarItem(Cadastro item)
        {
           var cadastrarPage = new Cadastrar(item);
           Navigation.PushAsync(cadastrarPage);
        }
        private void OnItemSwiped(object parameter)
        {
            var item = parameter as Cadastro;
            if (item != null)
            {
                // Executar ação desejada para o deslize para a direita
                ExibirDetalhes(item);
            }
        }

        private async void AnexarItem(Cadastro item)
        {
            var pagina = new PopupFotoView();

            var popupFotoView = new PopupFotoView();
            popupFotoView.PopImage.Source = ImageSource.FromFile(item.Anexo); // Substitua "fotoPath" pela variável que contém o caminho da foto

            await PopupNavigation.Instance.PushAsync(popupFotoView);
        }

        private void Editar_Clicked(object sender, EventArgs e)
        {

        }

        private async void Remover_Clicked(object sender, EventArgs e)
        {
            bool confirmarExclusao = await DisplayAlert("Confirmação", "Deseja excluir este item?", "Sim", "Não");
            if (confirmarExclusao)
            {
                if (SelectedItem != null)
                {
                    Aux.Remove(SelectedItem);
                    await App.SQLiteDbControle.DeleteElementoAsync(SelectedItem);

                    var swipeViewToRemove = lancamentosLayout.Children.FirstOrDefault(child => child is SwipeView);
                    if (swipeViewToRemove != null)
                    {
                        lancamentosLayout.Children.Remove(swipeViewToRemove);
                    }
                }
                else
                {
                    await DisplayAlert("Erro", "Nenhum item selecionado", "OK");
                }
            }
        }
        private void OnLancamentosTabSelected(object sender, EventArgs e)
        {
            // Lógica do manipulador de eventos OnLancamentosTabSelected
        }

        private void OnLancamentosTabUnselected(object sender, EventArgs e)
        {
            // Lógica do manipulador de eventos OnLancamentosTabUnselected
        }

        private async void Aguarde()
        {
            await Task.Delay(1000);
        }

        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            btnEnviar.IsEnabled = false;
            DAL OperacaoBancoDados = new DAL();
            await Task.Run(Aguarde);

            /*
             * enviando dados ao banco de dados remoto
             */
            var elementos = await App.SQLiteDbControle.ReadElementos();
            foreach (var elemento in elementos)
            {
                await Task.Run(Aguarde);
                OperacaoBancoDados.InserirNovoRegistro(
                elemento.ID,
                elemento.TipoDespesa,
                elemento.DataDespesa,
                elemento.Valor,
                elemento.Descricao,
                elemento.Anexo,
                IdUsr.ValorIdUsr
                ) ;
                await Task.Run(Aguarde);

                var swipeViewToRemove = lancamentosLayout.Children.FirstOrDefault(child =>
                {
                    if (child is SwipeView swipeView)
                    {
                        var swipeContent = swipeView.Content as Grid;
                        var label = swipeContent.Children.OfType<Label>().FirstOrDefault();
                        return label != null && label.Text.Contains(elemento.ID.ToString());
                    }
                    return false;
                });

                if (swipeViewToRemove != null)
                {
                    lancamentosLayout.Children.Remove(swipeViewToRemove);
                }


                await App.SQLiteDbControle.ExcluirElemento(elemento);

            }


            await DisplayAlert("Aviso", "Feito! Banco de dados atualizado!", "OK");
        }
    }
}