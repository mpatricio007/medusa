using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.BLL;
using Medusa.LIB;
using Medusa.DAL;

namespace Medusa.Relatorios.Almoxarifado
{
    public class RelatorioMaterial
    {
        public string tipoMedida { get; set; }

        public string nomeMaterial { get; set; }

        public int qtde_total { get; set; }

        public int qtde_minima { get; set; }

        public decimal valor { get; set; }

        public string tipoMaterial { get; set; }

        MaterialConsumoBLL matBLL = new MaterialConsumoBLL();

        public RelatorioMaterial()
        {
        }

        public List<RelatorioMaterial> GerarRelatorio(IEnumerable<MaterialConsumo> materiais)
        {
            return (from item in materiais
                    orderby item.descricao
                    select new RelatorioMaterial()
                    {
                        tipoMedida = item.UnidadeMedida.descricao,
                        nomeMaterial = item.descricao,
                        qtde_minima = item.qtde_minima,
                        qtde_total = item.Total,
                        valor = item.valor,
                        tipoMaterial = item.TipoMaterial.nome,
                    }).ToList();
        }
    }
}