using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Collections;

namespace Medusa.BLL
{
    public class RamaisDoUsuario
    {
        public string nome { get; set; }
        public string ramal { get; set; }
        public string setor { get; set; }
        public int id_pessoa { get; set; }
        public string sigla_setor { get; set; }
        public string exibir { get { return String.Format("{0} R.{1} {2}", nome, ramal, setor); } }
        public string exibirSetor { get { return String.Format("{0} {1} {2} R.{3}", sigla_setor, setor, nome, ramal); } }

    }

    
    public class UsuarioFuspBLL: PessoaFisBLL<UsuarioFusp>
    {
        public bool IsAdministrador(Int32 id_usu)
        {
            Get(id_usu);
            return (ObjEF.nivel==1);
        }

        public IEnumerable<RamaisDoUsuario> Ramais()
        {
            return (from ct in _dbContext.Usuarios.OfType<UsuarioFusp>()
                    where ct.status == true
                    orderby ct.PessoaFisica.nome
                    select new RamaisDoUsuario
                    {
                        id_pessoa = ct.id_usuario,
                        ramal = ct.ramal,
                        setor = ct.Setor.nome,
                        nome = ct.PessoaFisica.nome,
                        sigla_setor = ct.Setor.sigla
                    }).ToList();
        }

        public IEnumerable<RamaisDoUsuario> RamaisPorSetor()
        {
            return (from ct in _dbContext.Usuarios.OfType<UsuarioFusp>()
                    where ct.status == true
                    orderby ct.Setor.sigla,ct.PessoaFisica.nome
                    select new RamaisDoUsuario
                    {
                        id_pessoa = ct.id_usuario,
                        ramal = ct.ramal,
                        setor = ct.Setor.nome,
                        nome = ct.PessoaFisica.nome,
                        sigla_setor = ct.Setor.sigla
                    }).ToList();
        }

        public void GetUsuarioFuspPorCpf(string strCpf)
        {
            ObjEF = (from u in _dbContext.Usuarios.OfType<UsuarioFusp>()
                     where u.PessoaFisica.cpf.Value == strCpf
                     select u).FirstOrDefault();
        }

        public IEnumerable<UsuarioFusp> GetUsuariosDisponiveis(int intId_tipo_lcto)
        {
            var ds = from tlUsu in _dbContext.UsuarioTipoLctos
                     where tlUsu.id_tipo_lcto == intId_tipo_lcto
                     select tlUsu.id_usuario;

            return (from u in _dbContext.Usuarios.OfType<UsuarioFusp>()
                    where u.status == true &
                         !ds.Contains(u.id_usuario)
                    orderby u.PessoaFisica.nome
                    select u).ToList();
        }

        public IEnumerable GetAllUsersEntrada()
        {
            return from u in _dbSet.Where(it => !SetorBLL.NaoParticipantes.Contains(it.id_setor)).ToList()
                       where u.status
                       orderby u.PessoaFisica.nome
                       select new {id_usuario = u.id_usuario, nome = u.PessoaFisica.nome};
        }
    }
}
