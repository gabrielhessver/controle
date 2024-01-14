using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Plugin.Media;
using Exception = System.Exception;
using controle_financeiro.Models;
using System.Globalization;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;

namespace controle_financeiro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastrar : ContentPage
    {
        private Cadastro itemEditar;
        string caminhoSalvarFotos = string.Empty;
        string dataHorario = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string fotoPath = string.Empty;
        string FotoSelecionada;
        public Cadastrar()
        {
            InitializeComponent();

            if (!Directory.Exists("/storage/emulated/0/Android/data/com.ver.controle_financeiro/files/Pictures"))
            {
                Directory.CreateDirectory("/storage/emulated/0/Android/data/com.ver.controle_financeiro/files/" + "Pictures" + "/controle_financeiro");
            }
            caminhoSalvarFotos = "/storage/emulated/0/Android/data/com.ver.controle_financeiro/files/" + "Pictures" + "/controle_financeiro";

        }

        public Cadastrar(Cadastro cadastro) : this()
        {
            itemEditar = cadastro;
            pckTipo.SelectedItem = cadastro.TipoDespesa;
            btnData.Date = DateTime.ParseExact(cadastro.DataDespesa, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            btnValor.Text = cadastro.Valor;
            btnDescricao.Text = cadastro.Descricao;
            FotoSelecionada = cadastro.Anexo;
            FotoGaleria.Source = ImageSource.FromFile(FotoSelecionada);

        }

        string f_Anexo = string.Empty;
        private async void btnAnexo_Clicked(object sender, EventArgs e)
        {
            var pagina = new PopUpAnexoControle();

            await PopupNavigation.PushAsync(pagina, true);

            var result = await pagina.Show(); //wait till user taps/selects option 

            string NomeAnexo = dataHorario + "NOTA_FISCAL_1" + "_01";

            //Tira FOTO e Salva
            if (result && pagina.opcao == false)
            {
                await CapturarFoto(NomeAnexo.ToUpper());
            }
            else
            {
                await PegarGaleria(NomeAnexo.ToUpper());
            }

        }

        bool temFoto = false;
        async Task CapturarFoto(string nomeElemento)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                string _CaminhoComCodigoElemento = caminhoSalvarFotos + "/" + nomeElemento; // caminho config no diretorio raiz
                string _extensaoArquivoBruto = System.IO.Path.GetExtension(photo.FullPath); // extensao do arquivo da funcao q chama foto

                string _novoArquivoComExtensao = _CaminhoComCodigoElemento + _extensaoArquivoBruto;
                File.Copy(photo.FullPath, _novoArquivoComExtensao, true);

                await CarregarFotoAsync(photo);
                Console.WriteLine($"Capturado completo {caminhoSalvarFotos}");
                temFoto = true;

                FotoSelecionada = _novoArquivoComExtensao;
                FotoGaleria.Source = ImageSource.FromFile(FotoSelecionada);

                if (FotoSelecionada != null)
                {
                    _ = DisplayAlert("Notal Fiscal", "FOTO NOTA FISCAL OK", "OK");
                    btnAnexo.Text = "FOTO NOTA FISCAL - OK";
                    btnAnexo.BackgroundColor = Color.Green;
                }
                
            }
            catch (FeatureNotSupportedException fnsEx)
            {

                _ = DisplayAlert("Permissão", "Ação não suportada para o usuário" + fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                _ = DisplayAlert("Permissão", "Ação não permitada pelo usuário" + pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Erro", $"Capture PhotoAsync: {ex.Message}", "OK");
            }            
        }

        async Task PegarGaleria(string nomeElemento)
        {
            try
            {
                var file = await CrossFilePicker.Current.PickFile();

                if (file != null)
                {
                    string _fileName = file.FilePath;
                    FotoSelecionada = _fileName;
                    FotoGaleria.Source = ImageSource.FromFile(FotoSelecionada);
                    if (FotoSelecionada != null)
                    {
                        _ = DisplayAlert("Notal Fiscal", "FOTO NOTA FISCAL OK", "OK");
                        btnAnexo.Text = "FOTO NOTA FISCAL - OK";
                        btnAnexo.BackgroundColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"PegarGaleria: {ex.Message}", "OK");
            }            
        }        


        async Task CarregarFotoAsync(FileResult photo)
        {
            // se cancelado
            if (photo == null)
            {
                caminhoSalvarFotos = null;
                return;
            }

        }


        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            if (pckTipo.SelectedItem == null)
            {
                await DisplayAlert("Erro", "Informe o tipo de despesa", "OK");
                return;
            }

            if (string.IsNullOrEmpty(btnValor.Text))
            {
                await DisplayAlert("Erro", "O campo valor não foi preenchido", "OK");
                return;
            }

            if (string.IsNullOrEmpty(btnDescricao.Text))
            {
                bool continuarSemPreencherDescricao = await DisplayAlert("Atenção", "Campo descrição sem preenchimento ou preenchimento incompleto. Deseja continuar?", "SIM", "NÃO");
                if (!continuarSemPreencherDescricao)
                {
                    return;
                }
            }

            if (FotoSelecionada == null)
            {
                await DisplayAlert("Erro", "A nota fiscal não foi anexada", "OK");
                return;
            }

            bool confirmarGravar = await DisplayAlert("Atenção", "Confirmar salvar os dados?", "SIM", "NÃO");
            if (!confirmarGravar)
            {
                return;
            }

            string tipo_despesa = ((string)pckTipo.SelectedItem) ?? "0";
            string data_despesa = btnData.Date.ToString("dd-MM-yyyy");
            string valor = btnValor.Text;
            string descricao = btnDescricao.Text;

            if (itemEditar == null)
            {
                // Crie um novo objeto Cadastro apenas se não houver itemEditar
                await App.SQLiteDbControle.CreateElemento(new Models.Cadastro
                {
                    TipoDespesa = tipo_despesa,
                    DataDespesa = data_despesa,
                    Valor = valor,
                    Descricao = descricao,
                    Anexo = FotoSelecionada
                });
            }
            else
            {
                // Atualize os dados do objeto itemEditar
                itemEditar.TipoDespesa = tipo_despesa;
                itemEditar.DataDespesa = data_despesa;
                itemEditar.Valor = valor;
                itemEditar.Descricao = descricao;
                itemEditar.Anexo = FotoSelecionada;

                // Atualize o item existente no banco de dados
                await App.SQLiteDbControle.UpdateElementoAsync(itemEditar);
            }

            await Navigation.PopAsync();
        }



        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            var pagina = new PopupFotoView();

            var popupFotoView = new PopupFotoView();
            popupFotoView.PopImage.Source = ImageSource.FromFile(FotoSelecionada); // Substitua "fotoPath" pela variável que contém o caminho da foto

            await PopupNavigation.Instance.PushAsync(popupFotoView);
        }
    }
}