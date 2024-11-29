using ClimaTempo.Models;
using ClimaTempo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClimaTempo.ViewModels
{
    public partial class PrevisaoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cidade;
        
        [ObservableProperty]
        private string estado;

        [ObservableProperty]
        private string condicao;

        [ObservableProperty]
        private string condicao_desc;

        [ObservableProperty]
        private DateTime data;
        
        [ObservableProperty]
        private double min;

        [ObservableProperty]
        private double max;

        [ObservableProperty]
        private double indice_uv;

        [ObservableProperty]
        private List<Clima> proximosDias;

        private Previsao previsao;
        private Previsao proxPrevisao;


        // Dados da Pesquisa
        [ObservableProperty]
        private string cidade_pesquisada;


        // Dados da lista de Cidade
        [ObservableProperty]
        private List<Cidade> cidade_list;

        public ICommand BuscarPrevisaoCommand { get; }

        public ICommand BuscarCidadesCommand { get; }


        public PrevisaoViewModel()
        {
            BuscarPrevisaoCommand = new Command(BuscarPrevisao);
            BuscarCidadesCommand = new Command(BuscarCidade);
        }

        public async  void BuscarPrevisao()
        {
            //Busca os dados da previsão para uma cidade especificada:
            previsao = await new PrevisaoServices().GetPrevisaoById(244);
            
            Cidade = previsao.Cidade;
            Estado = previsao.Estado;
            Condicao = previsao.Clima[0].Condicao;
            Condicao_desc = previsao.Clima[0].Condicao_desc;

            Min = previsao.Clima[0].Min;
            Max = previsao.Clima[0].Max;
            Indice_uv = previsao.Clima[0].Indice_uv;
            Data = previsao.Clima[0].Data;


            proxPrevisao = await new PrevisaoServices().GetPrevisaoForXDaysById(244, 3);
            ProximosDias = proxPrevisao.Clima;
           
            

            //Busca os dados da previsão para os próximos dias:
            proxPrevisao = await new PrevisaoServices().GetPrevisaoForXDaysById(244, 3);
            ProximosDias = proxPrevisao.Clima;
        }
    
        public async void BuscarCidade()
        {
            Cidade_list = new List<Cidade>();
            Cidade_list = await new CidadeService().GetCidadesByName(Cidade_pesquisada);
        }
    
    
    }
}
