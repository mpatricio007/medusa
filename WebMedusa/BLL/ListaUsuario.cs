using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL
{
    [Serializable]
    public class ListaUsuario
    {
        public int id_pessoa { get; set; }
        public string nome { get; set; }

        public ListaUsuario(DAL.Usuario usu)
        {
            id_pessoa = usu.id_usuario;
            nome = usu.PessoaFisica.nome;
        }
    }
}