using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data;

namespace Medusa.BLL 
{
    public class ProjetoBLL : AbstractCrudWithLog<Projeto>
    {

        public List<ArquivoAnexoProjeto> oldAnexos { get; set; }

        public override bool SaveChanges()
        {
            bool rt = base.SaveChanges();

            var ds = ObjEF.ArquivoAnexoProjetos.ToList();
            foreach (var item in ds)
            {
                _dbContext.Entry(item).State = EntityState.Detached;
            }

            return rt;
        }

        public ProjetoBLL()
            : base()
        {
        }

        public ProjetoBLL(Contexto ctx)
            : base(ctx)
        {
        }

        public decimal SaldoProjeto(DateTime data)
        {
            return 0;
        }

        public bool EhSubProjeto()
        {
            return !String.IsNullOrEmpty(ObjEF.sub_projeto.Trim());
        }

        public Projeto GetProjetoPai()
        {
            if (EhSubProjeto())
                return _dbSet.Where(it => it.codigo == ObjEF.cod_def_projeto).FirstOrDefault();
            else
                return null;
        }

        public bool TemContaEspecifica()
        {
            return ObjEF.Contas.Where(it => it.ContaTipo.conta_especifico).Count() > 0;
        }

        public bool DataIsValid(DateTime data,ref string strMsg)
        {
        
            bool rt = false;
    
            if (EstaBloqueado())
            {
                strMsg = String.Format("Projeto {0} bloqueado!", ObjEF.codigo);
                return rt;
            }
            else if (EstaInativo())
            {
                strMsg = String.Format("Projeto {0} inativo!", ObjEF.codigo);
                return rt;
            }

            else if (EstaEncerrado(data))
            {
                strMsg = String.Format("Projeto {0} encerrado!", ObjEF.codigo); 
                return rt;
            }
            rt = true;
            
            return rt;
        }

        public bool EstaInativo()
        {
            return ObjEF.id_ultimo_status == StatusProjetoBLL.Inativo;
        }

        public bool EstaBloqueado()
        {
            return ObjEF.id_ultimo_status == StatusProjetoBLL.Bloqueado;
        }

        public bool EstaEncerrado(DateTime data)
        {
            if (ObjEF.data_termino.HasValue)
                return ObjEF.data_termino < data;
            else
                return false;
        }

        public Int32 UltimoCodigoA()
        {
            return _dbSet.Max(c => (int?)c.cod_a_projeto).GetValueOrDefault();
        }

        public bool ExisteProjetoA(int cod_a, string sub_a)
        {
            return _dbSet.Where(it => it.cod_a_projeto == cod_a & it.sub_projeto_a == sub_a).Count() > 0;
        }

        public bool ValidarCadastro(out string msg)
        {
            var rt = true;
            var txtMsg = new StringBuilder();

            if (ObjEF.Coordenadores.Where(it => it.tipo == TipoCoordenador.coordenador).Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("* cadastre ao menos um coordenador!<br/>");
            }

            if (ObjEF.Financiadores.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("* cadastre ao menos um financiador!<br/>");
            }

            if (ObjEF.Enderecos.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("* cadastre ao menos um endereço!<br/>");
            }

            //if (ObjEF.Contatos.Count() == 0)
            //{
            //    rt = false;
            //    txtMsg.AppendLine("* cadastre ao menos um contato!<br/>");
            //}

            if (ObjEF.Documentos.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("* cadastre ao menos um documento!<br/>");
            }

            //if (ObjEF.Taxas.Count() == 0)
            //{
            //    rt = false;
            //    txtMsg.AppendLine("* cadastre ao menos uma taxa!<br/>");
            //}

            if (ObjEF.SetorResponsavel.Count() == 0)
            {
                rt = false;
                txtMsg.AppendLine("* cadastre ao menos uma setor responsável!<br/>");
            }

            if (rt)
                msg = String.Empty;
            else
                msg = txtMsg.ToString();

            return rt;
        }

        public bool GerarProjetoDefinitivo(int codDef, string subProj)
        {
            bool valida = _dbSet.Where(it => it.cod_def_projeto == codDef & it.sub_projeto == subProj).Count() == 0;
            ObjEF.sub_projeto = subProj;

            var rt = false;            
            if (valida)
            {                
                ObjEF.id_ultimo_status = StatusProjetoBLL.Preliminar;
                var hp = new HistoricoProjeto();
                hp.id_status_projeto = ObjEF.id_ultimo_status.GetValueOrDefault();
                hp.data = DateTime.Now;
                hp.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
                ObjEF.HistoricoProjetos.Add(hp);
                ObjEF.data_proj_def = DateTime.Now;

                Update();
                rt = SaveChanges();
                if (rt)
                    Util.ShowMessage("projeto gerado com sucesso!");
                else
                    Util.ShowMessage("ERRO ao tentar gerar projeto!");
            }
            else
                Util.ShowMessage("nº de projeto já cadastrado!");
            return rt;
        }

        public IEnumerable<Projeto> GetAllDefinitivos()
        {
            return _dbSet.Where(it => it.cod_def_projeto.HasValue).OrderBy(it => it. codigo).ToList();
        }

        public bool EhDefinitivo()
        {
            return ObjEF.cod_def_projeto.HasValue;
        }

        public IEnumerable<Projeto> Find(List<Filter> lstFilters, string sortExpression, string sortDirection, int top, int id_coordenador, int id_financiador, int id_ultimo_status, int id_nat_projeto)
        {
            var customFilters = new List<Filter>();
            if (id_coordenador != 0)            
            {
                var projetosCoordenadores = _dbContext.ProjetoCoordenadores.Where(it => it.id_coordenador == id_coordenador);

                if (projetosCoordenadores.Count() == 0)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_projeto",
                        property_name = "id_projeto", 
                        value = "0"
                    });
                }
                foreach (var pc in projetosCoordenadores)
                {
                    
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_projeto",// ehDefinitivo ? "cod_def_projeto" : "cod_a_projeto",

                        property_name = "ID", //ehDefinitivo ? "código definitivo" : "projeto A",
                        value = pc.id_projeto.ToString(),                        
                    });
                }
            }

            if (id_financiador != 0)
            {
                var projetosFinanciadores = _dbContext.ProjetoFinanciadores.Where(it => it.id_financiador == id_financiador);

                if (projetosFinanciadores.Count() == 0)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_projeto",
                        property_name = "id_projeto",
                        value = "0"
                    });
                }

                foreach (var pf in projetosFinanciadores)
                {
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_projeto",
                        property_name = "id_projeto",
                        value = pf.id_projeto.ToString()
                    });
                }
            }


            if (id_ultimo_status != 0)
            {
                var status = _dbContext.StatusProjeto.Find(id_ultimo_status);
                   
                    customFilters.Add(new Filter()
                    {
                        mode = "==",
                        property = "id_ultimo_status",
                        property_name = "status",
                        value = Util.InteiroToString(status.id_status_projeto),
                        displayValue = status.nome
                    });
                
            }

            if (id_nat_projeto != 0)
            {
                var natProj = _dbContext.NaturezaProjetos.Find(id_nat_projeto);

                customFilters.Add(new Filter()
                {
                    mode = "==",
                    property = "id_nat_projeto",
                    property_name = "natureza",
                    value = Util.InteiroToString(natProj.id_nat_projeto),
                    displayValue = natProj.nome
                });

            }

            foreach (var item in customFilters)
            {
                if(!lstFilters.Contains(item))
                    lstFilters.Add(item);
            }


            var ds = top == 0 ? _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList() : 
                _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();          
            return ds;
        }


        public bool Exists()
        {
            return ObjEF.id_projeto != 0;    
        }

       public void GetProjeto(int codigo)
       {
           ObjEF = _dbSet.SingleOrDefault(it => it.codigo == codigo);
       }
    }
}      
