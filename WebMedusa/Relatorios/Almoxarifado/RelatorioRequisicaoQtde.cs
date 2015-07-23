using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel;
using Medusa.DAL;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Medusa.BLL;

namespace Medusa.Relatorios.Almoxarifado
{
    public class RelatorioRequisicaoQtde
    {        

        public string material { get; set; }
        public int quantidade { get; set; }
        public string setor { get; set; }

        public RelatorioRequisicaoQtde()
        {
        }

        public RelatorioRequisicaoQtde(RequisicaoMaterial r)
        {
            // TODO: Complete member initialization
            material = r.MaterialConsumo.descricao;
            quantidade = r.quantidade;
            setor = r.Requisicao.Usuario.Setor.nome;
        }

        public IEnumerable<RelatorioRequisicaoQtde> GerarRelatorio(DateTime de, DateTime ate)
        {
            var ctx = new Contexto();

            var ds = (from r in ctx.RequisicaoMateriais
                      where  r.Requisicao.data >= de & r.Requisicao.data <= ate
                      orderby r.Requisicao.data
                      select new RelatorioRequisicaoQtde()
                      {
                          material = r.MaterialConsumo.descricao,
                          quantidade = r.quantidade,
                          setor = r.Requisicao.Usuario.Setor.nome
                      }).ToList();
            return (from r in ctx.RequisicaoMateriais
                    where r.Historicos.OrderByDescending(it => it.id_historico_requisicao).FirstOrDefault().id_status_requisicao_material == StatusRequisicaoMaterialBLL.AguardandoAtendimento  & r.Requisicao.data >= de & r.Requisicao.data <= ate
                    orderby r.Requisicao.data
                    select new RelatorioRequisicaoQtde()
                    {
                        material = r.MaterialConsumo.descricao,
                        quantidade = r.quantidade,
                        setor = r.Requisicao.Usuario.Setor.nome
                    }).ToList();
                    //select r).Where(it => StatusRequisicaoMaterialBLL.lstEmAberto.Contains(it.StatusRequisicaoMaterial.id_status_requisicao_material)).
                    //Select(k => new RelatorioRequisicaoQtde(k)).ToList();
        }

        public IEnumerable<RelatorioRequisicaoQtde> GerarRelatorio(IEnumerable<RequisicaoMaterial> requisicoes)
        {

            return (from r in requisicoes
                    select new RelatorioRequisicaoQtde()
                    {
                        material = r.MaterialConsumo.descricao,
                        quantidade = r.quantidade,
                        setor = r.Requisicao.Usuario.Setor.nome,
                    }).ToList();
        }
    }
}