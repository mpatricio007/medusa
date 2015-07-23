using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;
using System.Data;
using System.Data.SqlClient;

namespace Medusa.BLL
{
    public class TaxaBLL : AbstractCrudWithLog<Taxa>
    {
        public decimal? valorBase { get; set; }
        public decimal? valorAdeduzir { get; set; }
        public decimal? valorImposto { get; set; }
        public decimal? inss_outr_empresa { get; set; }
        public DateTime data_pgto { get; set; }
        
        public TabelaTaxas GetTabela()
        {
            
                var tt = ObjEF.Tabelas.Where(it => it.data_ini <= data_pgto & it.data_fim >= data_pgto).FirstOrDefault();
                if (tt == null)
                    throw new System.ArgumentException(String.Format("Tabela de {0} não cadastrada para a data de pagamento {1:d}", ObjEF.nome, data_pgto));
                return tt;
            
            
        }

        public FaixaTaxas GetFaixa()
        {
            var fx = GetTabela().faixas.Where(k => k.faixa_de <= valorBase & k.faixa_ate >= valorBase).FirstOrDefault();
            if (fx == null)
                throw new System.ArgumentException(String.Format("Faixa de {0} não cadastrada para o valor {1:n2}", ObjEF.nome, valorBase));
            
            return fx;
        }

        public List<string> GetDedutiveis()
        {   
            return GetTabela().dedutiveis.Select(it => it.taxa.PlanoConta.codigo).ToList();
        }
         
        public decimal calcular()
        {

            var faixa = GetFaixa();             

            decimal? valor = (faixa.aliquota / 100) * (valorBase) - faixa.deducao - valorAdeduzir;

            if (faixa.valor_max.HasValue)
                    valor = valor + valorAdeduzir >= faixa.valor_max.GetValueOrDefault() ? faixa.valor_max.GetValueOrDefault() - valorAdeduzir
                        : valor;
            if (valor <= faixa.vlr_minimo) valor = 0;

            if (inss_outr_empresa.GetValueOrDefault(0) != 0)
            {
                if (inss_outr_empresa + valor > faixa.valor_max.GetValueOrDefault())
                {
                    if (faixa.valor_max.GetValueOrDefault(0) - inss_outr_empresa < valor)
                        valor = faixa.valor_max.GetValueOrDefault(0) - inss_outr_empresa;
                    else
                        valor = 0;
                } 
            }
            valor = valor.GetValueOrDefault(0).Truncar();
            valorImposto = valor;

            return valor.GetValueOrDefault(0);
        }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();
            if (rt)
                PlanoContaTaxa_Configuration();
            return rt;
        }

        public bool PlanoContaTaxa_Configuration()
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager
                          .ConnectionStrings["Contexto"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Taxa SET id_plano_conta = @Id_plano_conta WHERE id_taxa = @Id_taxa";
            command.Parameters.AddWithValue("@Id_plano_conta",ObjEF.id_plano_conta);
            command.Parameters.AddWithValue("@Id_taxa",ObjEF.id_taxa);
            connection.Open();
            int rt = command.ExecuteNonQuery();
            connection.Close();
            return rt > 0;
        }

        public TaxaBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
        }
        public TaxaBLL()
        {

        }

        public bool Exists()
        {
            return ObjEF.id_taxa != 0;
        }
    }
}
