using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Web;
using Medusa.LIB;


namespace Medusa.BLL
{
    public class TarefaBLL : AbstractCrudWithLog<Tarefa>
    {
        public TarefaBLL()
        {
            
        }        

        public IEnumerable<Tarefa> MinhasTarefas(Int32 id_usuario)
        {
            return (from t in _dbSet
                    from d in t.Destinatarios
                    where t.status == Constantes.TAREFA_PENDENTE & (t.id_usuario_de == id_usuario || d.id_usuario == id_usuario)
                    select t).Distinct().ToList();
            //return (from t in _dbContext.TarefaDestinatarios
            //        where (t.id_usuario == id_usuario || t.Tarefa.id_usuario_de == id_usuario) & t.Tarefa.status == Constantes.TAREFA_PENDENTE
            //          orderby t.Tarefa.data descending
            //          select t.Tarefa).Distinct().ToList<Tarefa>();
        }

        public IEnumerable<Tarefa> MinhasTarefasEncerradas(Int32 id_usuario)
        {
            return (from t in _dbSet
                    from d in t.Destinatarios
                    where t.status == Constantes.TAREFA_ENCERRADA & (t.id_usuario_de == id_usuario || d.id_usuario == id_usuario)
                    select t).Distinct().ToList();
            //return (from t in _dbContext.TarefaDestinatarios
            //        where (t.id_usuario == id_usuario || t.Tarefa.id_usuario_de == id_usuario) & t.Tarefa.status == Constantes.TAREFA_ENCERRADA 
            //        orderby t.Tarefa.data descending
            //        select t.Tarefa).Distinct().ToList<Tarefa>();
        }


        public string TornarPendente(int id_tarefa, int intId_usuario)
        {
            Get(id_tarefa);
            // somente quem criou a tarefa pode torná-la pendente novamente
            if (ObjEF.id_usuario_de == intId_usuario)
            {
                ObjEF.status = Constantes.TAREFA_PENDENTE;
                Update();
                if (SaveChanges())
                    return "Tarefa retornou ao status pendente!";
                else
                    return "ERRO!";

                    
            }
                else return "somente quem criou pode tornar a tarefa pendente";
        }


        public string EncerrarTarefa(int id_tarefa,int intId_usuario)
        {
            Get(id_tarefa);
            // Somente quem criou a tarefa pode encerrá-la
            if (ObjEF.id_usuario_de == intId_usuario)
            {  
                ObjEF.status = Constantes.TAREFA_ENCERRADA;
                Update();
                if (SaveChanges())
                    return "tarefa encerrado com sucesso!";
                else return "ERRO!";
            }
            else return "somente quem criou a tarefa pode encerrá-la";
        }

        public string ExcluirTarefa(int id_tarefa, int intId_usuario)
        {
            Get(id_tarefa);
            // Somente quem criou a tarefa pode encerrá-la
            if (ObjEF.id_usuario_de == intId_usuario)
            {
                Delete();
                if (SaveChanges())
                    return "tarefa excluida com sucesso!";
                else return "ERRO!";
            }
            else return "somente quem criou a tarefa pode exclui-la";
        }


        public string CriarTarefa(string txtTarefa,List<ListaUsuario> listaDestinatarios, Int32 intId_usuario)
        {
            ObjEF.data = DateTime.Now;
            ObjEF.id_usuario_de = intId_usuario;
            ObjEF.Destinatarios = new List<TarefaDestinatario>();            
            listaDestinatarios.ForEach(it => ObjEF.Destinatarios.Add(new TarefaDestinatario() { id_usuario = it.id_pessoa }));             
            ObjEF.tarefa = txtTarefa;
            ObjEF.status = Constantes.TAREFA_PENDENTE;
            Add();
            if (SaveChanges())
                return "tarefa criada com sucesso!";
            else return "ERRO!";
        }
    }
}
