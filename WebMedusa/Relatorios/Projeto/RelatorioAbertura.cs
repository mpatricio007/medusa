using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL;
using Medusa.LIB;
using System.Text;

namespace Medusa.Relatorios.Projeto
{ 
    public class RelatorioAbertura
    {
        public int px { get; set; }

        public string solicitante { get; set; }

        public string titulo { get; set; }

        public string descricao { get; set; } 

        public string coordenador { get; set; }

        public string financiador { get; set; }

        public string data_solicitacao { get; set; }

        public string tipo_solicitacao { get; set; }

        public string observacao { get; set; }

        public string strSubCoordenador { get; set; }
        
        public string unidade { get; set; }

        public string departamento { get; set; }

        public string laboratorio { get; set; }

        public string moeda { get; set; }

        public  string valor_global { get; set; }

        public string inicio { get; set; }

        public string termino { get; set; }

        public string instrContr { get; set; }

        public string contrato_patrocinio { get; set; }

        public string setor { get; set; }

        public RelatorioAbertura()
        {

        }

        public RelatorioAbertura(ProjetoSolicitacao p)
        {
            px = p.codigo;
            solicitante = p.Usuario.PessoaFisica.nome;
            titulo = p.titulo;
            descricao = p.descricao;
            coordenador = p.strCoordenador;
            financiador = p.strFinanciador;
            data_solicitacao = Util.DateToString(p.data_solicitacao);
            tipo_solicitacao = p.TipoSolicitacao.nome;
            observacao = p.observacao;
            strSubCoordenador = p.strSubCoordenador;
            unidade = p.id_unidade.HasValue ? p.Unidade.nome : String.Empty; 
            departamento = p.id_departamento.HasValue ? p.Departamento.nome : String.Empty;
            laboratorio = p.id_laboratorio.HasValue ? p.Laboratorio.nome : String.Empty;
            valor_global = p.valor_global.HasValue ? String.Format("{0:N2}", p.valor_global) : String.Empty;
            inicio = Util.DateToString(p.inicio);
            termino = Util.DateToString(p.termino);
            instrContr = p.id_instrumento_contratual.HasValue ? String.Format("{0} nº: {1}", p.InstrumentoContratual.nome, p.contrato_patrocinio) : String.Empty;
            contrato_patrocinio = p.contrato_patrocinio;
            moeda = p.id_moeda.HasValue ? p.Moeda.sigla : String.Empty;

            var u = (UsuarioFusp)p.Usuario;
            setor = u.Setor.nome;                
      }

        public IEnumerable<RelatorioAbertura> GerarRelatorio(int id_solicitacao)
        {
            var psBLL = new ProjetoSolicitacaoBLL();
            return (from p in psBLL.Find(it => it.id_sol_proj == id_solicitacao).OfType<ProjetoSolicitacao>()
                    select new RelatorioAbertura(p)
                    ).ToList();
        }
    }
}