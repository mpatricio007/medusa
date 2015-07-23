using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.BLL;
using Medusa.DAL;
using System.Text;

namespace Medusa.Relatorios.Projeto
{
    public class EtiquetaCoordenador
    {
        public string saudacoes { get; set; }

        public string nome { get; set; }

        public string unidadeCoord { get; set; }

        
        public EtiquetaCoordenador()
        { 
        }

        public EtiquetaCoordenador(Medusa.DAL.Coordenador c)
        {
            saudacoes = "Ilmo(a). Sr(a). Prof(a). Dr(a).";
            nome = c.PessoaFisica.nome;
            var txtUni = new StringBuilder();
            txtUni.AppendFormat("{0} - {1} - {2}",c.Unidade.sigla,c.id_departamento.HasValue ? c.Departamento.sigla : String.Empty,c.id_laboratorio.HasValue ? c.Laboratorio.sigla : String.Empty);
            unidadeCoord = txtUni.ToString();
       
        }

        public IEnumerable<EtiquetaCoordenador> GetEtiquetas(string status, string definitivos, TipoCoordenador tipo)
        {
            var lst = new List<EtiquetaCoordenador>();
            int id_status = 0;
            bool eh_defitivo = false;

            var ctx = new Contexto();
            var ds = ctx.ProjetoCoordenadores.ToList();

            if (int.TryParse(status, out id_status))
                ds = ds.Where(it => it.Projeto.id_ultimo_status == id_status).ToList();

            if (bool.TryParse(definitivos, out eh_defitivo))
                ds = ds.Where(it => it.Projeto.codigo.HasValue == eh_defitivo).ToList();
            
            foreach (var c in ds.Where(it => it.tipo == tipo).Select(it => it.Coordenador).Distinct())            
                lst.Add(new EtiquetaCoordenador(c));
            
            return lst;
        }
    }
}