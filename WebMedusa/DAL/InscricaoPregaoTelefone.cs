using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Medusa.DAL
{
    public class InscricaoPregaoTelefone
    {
        [Key]
        public int id_telefone { get; set; }

        public Telefone telefone { get; set; }

        [Required]        
        public int id_inscricao_pregao { get; set; }       

        public InscricaoPregaoTelefone()
        {
        }

        public InscricaoPregaoTelefone(Telefone tel)
        {
            telefone = tel;
        }
    }
}