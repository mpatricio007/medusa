using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class TarefaProvidenciaBLL : AbstractCrudWithLog<TarefaProvidencia>
    {
        public string ExcluirProvidencia(int id_providencia,int id_usuario)
        {
            Get(id_providencia);
            if (ObjEF.id_usuario == id_usuario)
            {
                Delete();
                if (SaveChanges())
                    return "excluido com sucesso!";
                else
                    return "ERRO!";
            }
            else
                return "somente quem postou pode excluir a providencia!";
        }

        public string PostarProvidencia(int id_tarefa, string strProvidencia, int id_usuario)
        {
            ObjEF = new TarefaProvidencia();
            ObjEF.data = DateTime.Now;
            ObjEF.providencia = strProvidencia;
            ObjEF.id_tarefa = id_tarefa;
            ObjEF.id_usuario = id_usuario;
            Add();
            if (SaveChanges())
                return "postado com sucesso!";
            else
                return "ERRO!";
        }
    }
}