using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    ///  Define o tipo de cada Arquivo
    ///  01 = Cobrança                                 -- Titulos                 
    ///  22 = Pagamento de Contas, Tributos e Impostos -- Contas de Consumo
    ///  98 = Pagamentos Diversos                      -- Pagamentos 
    /// </summary>
    public sealed class TipoServico
    {
        public readonly string name;        
        public readonly string codigo;


        public static readonly TipoServico Titulos = new TipoServico("01", "Cobrança");
        public static readonly TipoServico ContasConsumo = new TipoServico("98", "Pagamento de Contas, Tributos e Impostos");
        public static readonly TipoServico Pagamentos = new TipoServico("20", "Pagamentos Diversos");

        //public static Dictionary<string, TipoServico> GetTipo = new Dictionary<string, TipoServico>()
        //{
        //    //{ "00", null },
        //    //{ "01", TipoServico.Titulos },
        //    //{ "98", TipoServico.ContasConsumo },
        //    { "98", TipoServico.Pagamentos },
        //};

        //public static Dictionary<int, TipoServico> GetTipoPorLote = new Dictionary<int, TipoServico>()
        //{
        //    { 2, TipoServico.Titulos },
        //    { 1 , TipoServico.Pagamentos },
        //    { 3 , TipoServico.ContasConsumo },
        //};

        public static Dictionary<Type, TipoServico> GetTipoPorLote = new Dictionary<Type, TipoServico>()
        {
            { typeof(LoteBoleto), TipoServico.Titulos },
            { typeof(LotePagBB) , TipoServico.Pagamentos },
            { typeof(LoteCons) , TipoServico.ContasConsumo },
        };

        private TipoServico(string codigo,string name)
        {
            this.name = name;
            this.codigo = codigo;
        }

        public override String ToString()
        {
            return name;
        }

        public static TipoServico GetTipoArquivo(string strLinha)
        {
            string key = strLinha.Substring(228, 2);
            return TipoServico.GetTipo[key];
        }

    }
}