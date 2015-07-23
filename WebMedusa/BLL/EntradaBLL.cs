using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;
using System.Web;
using System.Collections;

namespace Medusa.BLL
{
    public class EntradaBLL : AbstractCrudWithLog<Entrada>
    {
        public EntradaBLL() { }

        public EntradaBLL(Contexto ctx)
            :base(ctx)
        {

        }

        public override void Add()
        {
            ObjEF.dataent = DateTime.Now;
            ObjEF.ano = ObjEF.dataprot.Year;
            ObjEF.id_usu_de = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.id_usu_entrada = SecurityBLL.GetCurrentUsuario().id_usuario;
            SalvarPrimeiroStatus(SecurityBLL.GetCurrentSetor(ObjEF.id_usu_para).id_setor);
            base.Add();
        }

        private void SalvarPrimeiroStatus(int Id_setor)
        {
            var histEntradaBLL = new HistoricoEntradaBLL(_dbContext);
            histEntradaBLL.ObjEF.data = DateTime.Now;
            histEntradaBLL.ObjEF.id_status_entrada = StatusEntradaBLL.Encaminhado;
            histEntradaBLL.ObjEF.id_usuario_de = SecurityBLL.GetCurrentUsuario().id_usuario;
            //histEntradaBLL.ObjEF.id_usuario_para = ObjEF.id_ultimo_para = ObjEF.id_usu_para;
            histEntradaBLL.ObjEF.obs = ObjEF.obsent;
            histEntradaBLL.ObjEF.Entrada = ObjEF;
            //ObjEF.id_ultimo_status = histEntradaBLL.ObjEF.id_status_entrada;
            var ufBLL = new UsuarioFuspBLL();
            foreach (var item in ufBLL.Find(it=>it.id_setor == Id_setor).ToList())
            {
                var destinatario = new DestinatarioEntradaBLL();
                destinatario.ObjEF.id_historico_entrada = histEntradaBLL.ObjEF.id_historico_entrada;
                destinatario.ObjEF.id_usuario = item.id_usuario;
                destinatario.Add();
                histEntradaBLL.ObjEF.DestinatariosEntrada.Add(destinatario.ObjEF);
            }
            histEntradaBLL.Add();
        }

        public override void Update()
        {
            ObjEF.ano = ObjEF.dataprot.Year;
            base.Update();
        }

        public void GetPorProtocolo(int ano,int numprot)
        {
            ObjEF = _dbSet.FirstOrDefault(it => it.nprotent == numprot & it.dataent.Year == ano);
        }

        public int GetNextProtocolo()
        {
            var ano = DateTime.Now.Year;
            var id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            
            int next = _dbSet.Where(it => it.ano == ano & it.id_usu_entrada == id_usuario).Select(it => (int?)it.nprotent).Max().GetValueOrDefault() + 1;
            while (true)
	        {                
                if (_dbSet.Where(it => it.ano == ano & it.nprotent == next).Count() == 0)
                    break;
                next += 1;
	        }
            
            return next;
        }


        public IEnumerable<int> GetAnos()
        {

           var anos = (from e in _dbSet
             select e.ano).Distinct().ToList();

            anos.Sort(delegate(int x1, int x2)
            {
                return x2.CompareTo(x1);
            });

            return anos;
        }

        public bool Exists()
        {
            return ObjEF.id_entrada != 0;
        }

        public bool ExistsSaida()
        {
            return ObjEF.saida != null;
        }

        public void EfetuarSaida()
        {
            StatusEntradaBLL seBLL = new StatusEntradaBLL();
            seBLL.Get(StatusEntradaBLL.Saida);
            var histEntradaBLL = new HistoricoEntradaBLL();
            histEntradaBLL.ObjEF = new HistoricoEntrada()
            {
                StatusEntrada = seBLL.ObjEF,
                id_status_entrada =seBLL.ObjEF.id_status_entrada,
                id_usuario_de = SecurityBLL.GetCurrentUsuario().id_usuario
            };

            AtualizarEstado(new List<Entrada>() { ObjEF }, histEntradaBLL.ObjEF, new List<UsuarioFusp>());
        }

        public void CancelarSaida()
        {
            StatusEntradaBLL seBLL = new StatusEntradaBLL();
            seBLL.Get(StatusEntradaBLL.SaidaCancelada);
            var histEntradaBLL = new HistoricoEntradaBLL();
            histEntradaBLL.ObjEF = new HistoricoEntrada()
            {
                StatusEntrada = seBLL.ObjEF,
                id_status_entrada = seBLL.ObjEF.id_status_entrada,
                id_usuario_de = SecurityBLL.GetCurrentUsuario().id_usuario
            };

            AtualizarEstado(new List<Entrada>() { ObjEF }, histEntradaBLL.ObjEF, new List<UsuarioFusp>());
        }

        public override bool DataIsValid()
        {
            return !ExistsSaida();
        }

        //public List<Entrada> Find(List<Filter> lstFilters, string sortExpression, string sortDirection, int top)
        //{
        //    if (top == 0)
        //        return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList();
        //    else
        //        return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        //}

        public bool AtualizarEstado(List<Entrada> SelectedEntradas, HistoricoEntrada newEstado, List<UsuarioFusp> usuariosDestinatarios)
        {
            int countError = 0;
            if(newEstado.StatusEntrada.escolhe_destinatarios)
                foreach (var ent in SelectedEntradas.Where(it=> it.EstadoAtual.id_status_entrada != newEstado.id_status_entrada))
                {
                    var histBLL = new HistoricoEntradaBLL(_dbContext);
                    histBLL.ObjEF.obs = newEstado.obs;
                    histBLL.ObjEF.id_usuario_de = newEstado.id_usuario_de;
                    histBLL.ObjEF.id_status_entrada = newEstado.id_status_entrada;   
                    foreach (var item in usuariosDestinatarios)
                    {
                        var dstBLL = new DestinatarioEntradaBLL();
                        dstBLL.ObjEF.id_historico_entrada = histBLL.ObjEF.id_historico_entrada;
                        dstBLL.ObjEF.id_usuario = item.id_usuario;
                        histBLL.ObjEF.id_entrada = ent.id_entrada;
                        dstBLL.Add();
                        histBLL.ObjEF.DestinatariosEntrada.Add(dstBLL.ObjEF);
                    }
                    histBLL.Add();
                    if (!histBLL.SaveChanges())
                        countError++;
                }
            else
                foreach (var ent in SelectedEntradas.Where(it => it.EstadoAtual.id_status_entrada != newEstado.id_status_entrada))
                {
                    var histBLL = new HistoricoEntradaBLL(_dbContext);
                    histBLL.ObjEF.obs = newEstado.obs;
                    histBLL.ObjEF.id_usuario_de = newEstado.id_usuario_de;
                    histBLL.ObjEF.id_status_entrada = newEstado.id_status_entrada;
                    histBLL.ObjEF.id_entrada = ent.id_entrada;

                    foreach (var item in ent.EstadoAtual.DestinatariosEntrada)
                    {
                        var dstBLL = new DestinatarioEntradaBLL();
                        dstBLL.ObjEF.id_historico_entrada = histBLL.ObjEF.id_historico_entrada;
                        dstBLL.ObjEF.id_usuario = item.id_usuario;
                        dstBLL.Add();
                        histBLL.ObjEF.DestinatariosEntrada.Add(dstBLL.ObjEF);
                    }
                    histBLL.Add();
                    if (!histBLL.SaveChanges())
                        countError++;
                }

            return countError == 0;
        }

        public bool DataIsValid(ref string strMsg, List<int> lstSelected)
        {
            bool rt = true;
            if (lstSelected.Count == 0)
            {
                rt = false;
                strMsg = "selecione ao menos um documento";
            }

            return rt;
        }

        public bool DataIsValid(ref string strMsg, int id_posivel_providencia, List<int> lstSelected)
        {
            var pp = new PossivelProvidenciaBLL();
            pp.Get(id_posivel_providencia);

            if (pp.ObjEF.Providencia.StatusFinal.exige_protocolo)
            {
                if (lstSelected.Count() > 1)
                {
                    Util.ShowMessage("somente um documento é permitido para esta providência");
                    return false;
                }

                foreach (var item in lstSelected)
                {
                    HttpContext.Current.Response.Redirect(String.Format(@"../../Sistemas/SREC/Entradas.aspx?pk={0}", Util.InteiroToString(item)));
                }
                return false;
            }

            if (pp.ObjEF.Providencia.SetoresCompetentes.Count() > 0 &&
                !pp.ObjEF.Providencia.SetoresCompetentes.Select(it => it.id_setor).Contains(SecurityBLL.GetCurrentSetor(SecurityBLL.GetCurrentUsuario().id_usuario).id_setor))
            {
                strMsg = "você não tem permissão para efetuar esta ação ";
                return false;
            }
            else
                return true;
        }

        public IEnumerable FindCaixaEntrada(List<Filter> lstFilters, string sortExpression, string sortDirection, int top, int id_usuario, int id_status_doc, int ano)
        {
            if (top == 0)
                return _dbContext.CarregarCaixaEntrada(id_usuario,id_status_doc,ano).Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbContext.CarregarCaixaEntrada(id_usuario, id_status_doc, ano).Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }

        public IEnumerable FindCaixaSaida(List<Filter> lstFilters, string sortExpression, string sortDirection, int top, int id_usuario, int id_status_doc, int ano)
        {
            if (top == 0)
                return _dbContext.CarregarCaixaSaida(id_usuario, id_status_doc, ano).Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbContext.CarregarCaixaSaida(id_usuario, id_status_doc, ano).Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }
    }
}
