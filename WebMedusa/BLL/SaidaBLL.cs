using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class SaidaBLL : AbstractCrudWithLog<Saida>
    {
        public override void Add()
        {
            ObjEF.ano = ObjEF.datasai.Year;
            ObjEF.id_usu_saida = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.datasai = DateTime.Now;

            ObjEF.Entrada = _dbContext.Entradas.Find(ObjEF.id_entrada);
            base.Add();
        }

        public override void Update()
        {
            ObjEF.ano = ObjEF.datasai.Year;
            base.Update();
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

        public void GetPorEntrada(int id_entrada)
        {
            ObjEF = _dbSet.SingleOrDefault(it => it.id_entrada == id_entrada);
        }

        public override bool DataIsValid()
        {

            return !Exists();
        }

        public bool Exists()
        {
            return ObjEF.id_saida != 0;
        }

        public override bool SaveChanges()
        {
            //EntradaBLL ent = new EntradaBLL(_dbContext);
            //ent.Get(ObjEF.Entrada.id_entrada);
            //ent.EfetuarSaida();

            return base.SaveChanges();
        }

        public override void Delete()
        {
            EntradaBLL ent = new EntradaBLL(_dbContext);
            ent.Get(ObjEF.Entrada.id_entrada);
            //ent.CancelarSaida();
            base.Delete();
        }

        public void SalvarStausSaida()
        {
            EntradaBLL ent = new EntradaBLL(_dbContext);
            ent.Get(ObjEF.id_entrada);
            ent.EfetuarSaida();
        }

        public override bool DataIsValid(ref string strMsg)
        {
            if (ObjEF.Entrada.EstadoAtual.DestinatariosEntrada.Select(it => it.id_usuario).Contains(SecurityBLL.GetCurrentUsuario().id_usuario)
                && base.DataIsValid())
                return true;
            else 
            {
                strMsg = "operação não permitida no momento";
                return false;
            }
        }
    }
}
