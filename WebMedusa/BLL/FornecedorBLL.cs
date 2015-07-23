using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;
using System.Collections;

namespace Medusa.BLL
{
    public class FornecedorBLL : AbstractCrudWithLog<Fornecedor>
    {
        public override void Add()
        {
            var status = _dbContext.StatusFornecedores.SingleOrDefault(it => it.ordem == 0);
            ObjEF.StatusFornecedor = status;
            ObjEF.Historicos = new List<HistoricoFornecedor>();
            var primeiroStatus = new HistoricoFornecedor();
            primeiroStatus.StatusFornecedor = status;
            primeiroStatus.data = DateTime.Now;
            ObjEF.Historicos.Add(primeiroStatus);
            base.Add();
        }

        public bool EnviarParaAnalise()
        {
            var status = _dbContext.StatusFornecedores.Where(it => it.ordem > 0).OrderBy(it => it.ordem).First();
            ObjEF.StatusFornecedor = status;            
            var analiseStatus = new HistoricoFornecedor();
            analiseStatus.StatusFornecedor = status;
            analiseStatus.data = DateTime.Now;
            ObjEF.Historicos.Add(analiseStatus);
            var rt =  SaveChanges();
            //if (rt)
            //{
                
            //    //Util.ShowMessage("Cadastro enviado para análise com sucesso!");
            //}
            return rt;
        }

        public void Get(CNPJ cnpj)
        {
            ObjEF = _dbSet.SingleOrDefault(it => it.cnpj.Value == cnpj.Value);
        }

        public bool Exists()
        {
            return ObjEF.id_fornecedor != 0;
        }

        public bool ValidarCadastro(out string msg)
        {
            var txtMsg = new StringBuilder();
            var rt = true;

            if (ObjEF.Socios.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("Cadastre ao menos um socio!");
            }
            if (ObjEF.Diretores.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("Cadastre ao menos um diretor!");
            }
            if (ObjEF.RepresentantesLegais.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("Cadastre ao menos um representante legal!");
            }

            if (ObjEF.ReferenciasBancarias.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("Cadastre ao menos uma referencia bancaria!");
            }
            if (rt == true)
                msg = String.Empty;
            else
                msg = txtMsg.ToString();
            return rt;

            //msg = rt == true ? String.Empty : txtMsg.ToString();
            
        }

        public bool IsValid()
        {
            return ObjEF.validade >= DateTime.Now;
        }

        public bool EmitirCRC()
        {
            return IsValid() & (ObjEF.id_categoria.HasValue ? ObjEF.Categoria.emitir_crc.GetValueOrDefault() : false) & (ObjEF.id_ultimo_status == 3 || ObjEF.id_ultimo_status == 6);
        }

        public override IEnumerable Find(List<Filter> lstFilters, string sortExpression, string sortDirection, int top)
        {            
            if (top == 0)
                return _dbSet.Where( lstFilters).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }
    }
}
