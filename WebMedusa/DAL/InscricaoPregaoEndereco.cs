using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Medusa.DAL;

namespace WebFusp.DAL
{
    [Serializable]
    public class InscricaoPregaoEndereco
    {
        [Key]
        public int id_endereco { get; set; }

        private Endereco ender;

        public Endereco endereco
        {
            get
            {
                if (ender == null)
                    ender = new Endereco();
                return ender;
            }
            set { ender = value; }
        }

        public int id_inscricao_pregao { get; set; }


        [NonSerialized()]
        private InscricaoPregao inscricaoPregao;

        [ForeignKey("id_inscricao_pregao")]
        public virtual InscricaoPregao InscricaoPregao
        {
            get { return inscricaoPregao; }
            set { inscricaoPregao = value; }
        }

        public InscricaoPregaoEndereco()
        {

        }

        public InscricaoPregaoEndereco(Endereco ender)
        {
            endereco = ender;
        }
    }
}

