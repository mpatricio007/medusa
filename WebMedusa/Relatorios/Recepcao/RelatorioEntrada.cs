using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.Relatorios.Recepcao
{
    public class RelatorioEntrada
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
        public string encaminhado_de { get; set; }
        public string moeda { get; set; }
        public string numDoc { get; set; }

        public int? protocolosaida { get; set; }
        public string destinatario { get; set; }
        public DateTime? dtSaida { get; set; }
        public string obsSaida { get; set; }
        public string responsavel_saida { get; set; }
        

        public RelatorioEntrada()
        {

        }
       
        public IEnumerable<RelatorioEntrada> GerarRelatorio(int de, int ate, int ano)
        {
            var ctx = new Contexto();
            return (from r in ctx.Entradas
                    join s in ctx.Saidas on r.id_entrada equals s.Entrada.id_entrada into rs
                    from s in rs.DefaultIfEmpty()
                    where r.ano == ano & r.nprotent >= de & r.nprotent <= ate
                    orderby r.nprotent
                    select new RelatorioEntrada()
                    {
                        id_entrada = r.id_entrada,
                        projeto = r.codproj,
                        projetoa = r.codproja,
                        enviado_por = r.enviadoent,
                        documento = r.tipodocent,
                        dtEntrada = r.dataent,
                        dtProtocolo = r.dataprot,
                        descricao = r.descrent,
                        observacao = r.obsent,
                        protocolo = r.nprotent,
                        encaminhado_para = r.UsuarioPara.PessoaFisica.nome,
                        moeda = r.ValorMoeda.sigla,
                        valor = r.valorent,
                        encaminhado_de = r.UsuarioDe.PessoaFisica.nome,
                        numDoc = r.numdocent,
                        protocolosaida = s.nprotsai ,
                        destinatario = s.destinatario,
                        dtSaida = s.datasai,
                        obsSaida = s.obssaida,
                        responsavel_saida = s.UsuarioResp.PessoaFisica.nome,
                    }).ToList();
        }        

        public IEnumerable<RelatorioEntrada> GerarRelatorio(IEnumerable<Entrada> entradas)
        {
            
            return (from r in entradas                                
                    select new RelatorioEntrada()
                    {
                        id_entrada = r.id_entrada,
                        projeto = r.codproj,
                        projetoa = r.codproja,
                        enviado_por = r.enviadoent,
                        documento = r.tipodocent,
                        dtEntrada = r.dataent,
                        dtProtocolo = r.dataprot,
                        descricao = r.descrent,
                        observacao = r.obsent,
                        protocolo = r.nprotent,
                        encaminhado_para = r.UsuarioPara.PessoaFisica.nome,
                        moeda = r.ValorMoeda.sigla,
                        valor = r.valorent,
                        encaminhado_de = r.UsuarioDe.PessoaFisica.nome    ,
                        numDoc = r.numdocent,
                    }).ToList();
        }
    }
}
        
 