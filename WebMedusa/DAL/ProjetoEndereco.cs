using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration;


namespace Medusa.DAL
{
    [Serializable]
    public class ProjetoEndereco
    {
        [Key]
        public int id_ender_projeto { get; set; }

        public int id_projeto { get; set; }

        [NonSerialized()]
        private Projeto projeto;

        [ForeignKey("id_projeto")]
        public virtual Projeto Projeto
        {
            get { return projeto; }
            set { projeto = value; }
        }
        public int tipo { get; set; } // 1 - correspondencia 2- execução

        private Endereco ender;

     }

    public class ProjetoEnderecoConfiguration : EntityTypeConfiguration<ProjetoEndereco>
    {
        public ProjetoEnderecoConfiguration()
        {
            ToTable("ProjetoEndereco");
        }
    }        


}