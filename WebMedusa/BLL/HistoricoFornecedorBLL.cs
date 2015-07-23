using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data;
using Medusa.LIB;
using System.Data.Entity;

namespace Medusa.BLL
{
    public class HistoricoFornecedorBLL : AbstractCrudWithLog<HistoricoFornecedor>
    {
        public override void Add()
        {
            ObjEF.id_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
            ObjEF.data = DateTime.Now;
            base.Add();
        }

        public override void Update()
        {
            base.Update();
        }

        public override bool SaveChanges()
        {
            try
            {
                var fornecedor = ObjEF.fornecedor;
                fornecedor = fornecedor ?? _dbContext.Fornecedores.Single(it => it.id_fornecedor == ObjEF.id_fornecedor);
                fornecedor.StatusFornecedor = _dbContext.StatusFornecedores.Single(it => it.id_status_fornecedor == ObjEF.id_status_fornecedor);

                if (fornecedor.StatusFornecedor.gerar_validade)
                    fornecedor.validade = DateTime.Now.AddYears(1);

                _dbContext.Entry(fornecedor).State = EntityState.Modified;
                return base.SaveChanges();           
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool EnviarEmail()
        {
             SendEmail sendmail = new SendEmail(ContaEmail.fusp);
             sendmail.Destinatarios = ObjEF.fornecedor.Usuario.PessoaFisica.Emails.Select(it => it.email.value).ToArray();
             sendmail.Subject = "Mudança de status no cadastro de fornecedor da FUSP";

             StringBuilder body = new StringBuilder();
             body.Append("Esta é uma mensagem automática do sistema. Não responda este e-mail.<br />");
             body.AppendFormat("Informamos a ocorrências de alterações no status desta empresa {0} CNPJ: {1} no cadastro de pessoa jurídica da FUSP.", ObjEF.fornecedor.nome, ObjEF.fornecedor.cnpj.Value);
             sendmail.Body = body.ToString();
             return sendmail.Send();
        }
    }
}
