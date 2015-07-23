using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.Relatorios.Recepcao
{
    public class RelatorioPesquisaEntrada
    {
        public int id_entrada { get; set; }
        public int? projeto { get; set; }
        public int? projetoa { get; set; }
        public string enviado_por { get; set; }
        public string documento { get; set; }
        public decimal? valor { get; set; }
        public DateTime dtEntrada { get; set; }
        public DateTime dtProtocolo { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public int protocolo { get; set; }
        public string encaminhado_para { get; set; }
        public string numDoc { get; set; }
        public string encaminhado_de { get; set; }

        public RelatorioPesquisaEntrada()
        { 
        }

        public List<RelatorioPesquisaEntrada> GerarRelatorio(List<Entrada> list)
        {
            return (from item in list
                    select new RelatorioPesquisaEntrada()
                    {
                        id_entrada = item.id_entrada,
                        projeto = item.codproj,
                        projetoa = item.codproja,
                        enviado_por = item.enviadoent,
                        documento = item.tipodocent,
                        dtEntrada = item.dataent,
                        dtProtocolo = item.dataprot,
                        descricao = item.descrent,
                        observacao = item.obsent,
                        protocolo = item.nprotent,
                        encaminhado_para = item.UsuarioPara.login,
                        valor = item.valorent,
                        numDoc = item.numdocent,
                        encaminhado_de = item.UsuarioDe.login
                    }).ToList();

        }
    }
}