using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using Medusa.DAL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Medusa.BLL;

namespace Medusa.DAL
{
    public class HistoricoEntrada
    {
        [Key]
        public int id_historico_entrada{ get; set; }

        public int? id_entrada { get; set; }
        [ForeignKey("id_entrada")]
        public virtual Entrada Entrada { get; set; }

        public int id_status_entrada { get; set; }
        [ForeignKey("id_status_entrada")]
        public virtual StatusEntrada StatusEntrada { get; set; }

        public string obs { get; set; }

        public DateTime data { get; set; }

        public int? id_usuario_de { get; set; }
        [ForeignKey("id_usuario_de")]
        public virtual UsuarioFusp UsuarioDe { get; set; }

        public int? id_usuario_para { get; set; }
        [ForeignKey("id_usuario_para")]
        public virtual UsuarioFusp UsuarioPara { get; set; }

        private ICollection<DestinatarioEntrada> destinatariosEntrada;
        [Invisible]
        public virtual ICollection<DestinatarioEntrada> DestinatariosEntrada
        {
            get
            {
                if (destinatariosEntrada == null)
                    destinatariosEntrada = new List<DestinatarioEntrada>();
                return destinatariosEntrada;
            }
            set { destinatariosEntrada = value; }
        }

        [NotMapped]
        public string StrDestinatarios
        {
            get
            {
                StringBuilder rt = new StringBuilder();
                if (DestinatariosEntrada.Count() > 1)
                    foreach (var item in DestinatariosEntrada.Select(it=>it.UsuarioFusp.Setor.nome).Distinct())
                    {
                        rt.AppendFormat("-{0} ", item);
                    }
                else
                    foreach (var item in DestinatariosEntrada)
                    {
                        rt.AppendFormat("-{0} ({1}); ", item.UsuarioFusp.PessoaFisica.nome, SecurityBLL.GetCurrentSetor(item.id_usuario).sigla);
                    }

                return rt.AppendLine().ToString();
            }
        }

        public HistoricoEntrada()
        {

        }

        public HistoricoEntrada(List<DestinatarioEntrada> destinatarios)
        {
            destinatariosEntrada = destinatarios;
        }
    }

    public class HistoricoEntradaConfiguration : EntityTypeConfiguration<HistoricoEntrada>
    {
        public HistoricoEntradaConfiguration()
        {
            ToTable("HistoricoEntrada");
        }
    }
}