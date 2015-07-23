using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data.SqlClient;


namespace Medusa.BLL
{
    public class old_empresaBLL : Abstract_Crud<old_empresa>
    {
        StringBuilder commandSQL = new StringBuilder();

        public override void Update()
        {
            commandSQL.Append("update old_empresa set CNPJ=@cnpj,RAZAOSOCIAL=@razaosocial,email=@email, bloqueado=@bloqueado where id=@id");
        }

        public override bool SaveChanges()
        {
            return _dbContext.Database.ExecuteSqlCommand(commandSQL.ToString(), new SqlParameter("@id", ObjEF.id),
                new SqlParameter("@cnpj", ObjEF.cnpj),
                new SqlParameter("@razaosocial", ObjEF.razaosocial),
                new SqlParameter("@bloqueado", ObjEF.bloqueado),
                new SqlParameter("@email", ObjEF.email)) > 0;
        }

        public bool podeImportar()
        {
            var cnpj = new CNPJ(ObjEF.cnpj);
            return (cnpj.IsValid) & (_dbContext.Fornecedores.Where(it => it.cnpj.Value == ObjEF.cnpj).Count() == 0) & (ObjEF.validade >= DateTime.Now);
        }

        //public bool importar()
        //{
        //    var ctx = new MedusaEntities();
        //    var contexto = new Contexto();
        //    var item = ctx.importOldFornecedor.SingleOrDefault(it => it.CNPJ == ObjEF.cnpj);

        //    if (podeImportar())
        //    {
        //        var cnpj = new CNPJ(item.CNPJ);
        //        var f = contexto.Fornecedores.Where(it => it.cnpj.Value == cnpj.Value).FirstOrDefault();
        //        if (f == null)
        //        {
        //            f = new Fornecedor();

        //            var usu = contexto.Usuarios.Where(it => it.PessoaFisica.cpf.Value == item.CPFREPR).FirstOrDefault();

        //            if (usu == null)
        //            {
        //                usu = new Usuario();
        //                usu.login = cnpj.Value;
        //                usu.senha = SecurityBLL.GetSha1Hash(item.SENHA);
        //                usu.status = true;
        //                usu.nivel = 5;

        //                var pes = contexto.Pessoas.OfType<PessoaFisica>().Where(it => it.cpf.Value == item.CPFREPR).FirstOrDefault();

        //                if (pes == null)
        //                {
        //                    pes = new PessoaFisica();
        //                    pes.nome = item.REPR;
        //                    pes.cpf = new CPF(item.CPFREPR);
        //                    pes.rg = item.RGREPR;
        //                    pes.sexo = "M";

        //                    var em = new Email(item.EMAIL);
        //                    var pesEm = new PessoaEmail(em);
        //                    pes.Emails.Add(pesEm);
        //                }

        //                usu.UsuarioSistema = new List<UsuarioSistema>();
        //                usu.PessoaFisica = pes;
        //                f.Usuario = usu;
        //            }
        //            else
        //                f.id_usuario = usu.id_usuario;

        //            usu.UsuarioSistema.Add(new UsuarioSistema()
        //            {
        //                id_sistema = (int)SistemasDefault.Fornecedor
        //            });


        //            f.cnpj = cnpj;
        //            f.nome = item.RAZAOSOCIAL;
        //            f.nome_fantasia = item.FANTASIA;
        //            f.grupo_economico = item.GRUPOECON;

        //            var ender = new Endereco();
        //            ender.logradouro = item.ENDERECO;
        //            ender.numero = "0";
        //            ender.bairro = item.BAIRRO;
        //            ender.cidade = item.CIDADE;
        //            ender.uf = item.ESTADO;
        //            ender.cep = item.CEP;
        //            f.ender = ender;

        //            f.telefone = new Telefone(item.TELEFONE, "", TipoTelefone.comercial);
        //            f.home_page = item.HOME_PAGE;
        //            f.email = new Email(item.EMAIL);
        //            f.inscr_estadual = item.IE;
        //            f.inscr_municipal = item.CCM;
        //            f.registro_numero = item.REGJUNTACIAL;
        //            f.data_alteracao = item.ULT_ALTERACAO;
        //            f.numero_alteracao = item.NUMERO;
        //            f.capital_social = item.CAPITAL_SOCIAL;
        //            f.ano_patrimonial = item.ANOBAL;

        //            var socio1 = new Socio();
        //            socio1.nome = item.SOC1;
        //            socio1.cpf = new CPF(item.CPFSOC1);
        //            socio1.rg = item.RGSOC1;
        //            socio1.tipo = TipoInscricao.CPF;
        //            socio1.cota = item.PERCSOC1 < 100 ? item.PERCSOC1 : 0;
        //            socio1.cnpj = new CNPJ();
        //            socio1.email = new Email(item.EMAIL);


        //            if (socio1.cpf.IsValid)
        //                f.Socios.Add(socio1);

        //            var socio2 = new Socio();
        //            socio2.nome = item.SOC2;
        //            socio2.cpf = new CPF(item.CPFSOC2);
        //            socio2.rg = item.RGSOC2;
        //            socio2.tipo = TipoInscricao.CPF;
        //            socio2.cota = item.PERCSOC2 < 100 ? item.PERCSOC2 : 0;
        //            socio2.cnpj = new CNPJ();
        //            socio2.email = new Email(item.EMAIL);

        //            if (socio2.cpf.IsValid)
        //                f.Socios.Add(socio2);

        //            var socio3 = new Socio();
        //            socio3.nome = item.SOC3;
        //            socio3.cpf = new CPF(item.CPFSOC3);
        //            socio3.rg = item.RGSOC3;
        //            socio3.tipo = TipoInscricao.CPF;
        //            socio3.cota = item.PERCSOC3 < 100 ? item.PERCSOC3 : 0;
        //            socio3.cnpj = new CNPJ();
        //            socio3.email = new Email(item.EMAIL);

        //            if (socio3.cpf.IsValid)
        //                f.Socios.Add(socio3);

        //            var socio4 = new Socio();
        //            socio4.nome = item.SOC4;
        //            socio4.cpf = new CPF(item.CPFSOC4);
        //            socio4.rg = item.RGSOC4;
        //            socio4.tipo = TipoInscricao.CPF;
        //            socio4.cota = item.PERCSOC4 < 100 ? item.PERCSOC4 : 0;
        //            socio4.cnpj = new CNPJ();
        //            socio4.email = new Email(item.EMAIL);

        //            if (socio4.cpf.IsValid)
        //                f.Socios.Add(socio4);

        //            var diretor1 = new Diretor();
        //            diretor1.nome = item.DIR1;
        //            diretor1.cpf = new CPF(item.CPFDIR1);
        //            diretor1.rg = item.RGDIR1;
        //            diretor1.email = new Email(item.EMAIL);

        //            if (diretor1.cpf.IsValid)
        //                f.Diretores.Add(diretor1);


        //            var diretor2 = new Diretor();
        //            diretor2.nome = item.DIR2;
        //            diretor2.cpf = new CPF(item.CPFDIR2);
        //            diretor2.rg = item.RGDIR2;
        //            diretor2.email = new Email(item.EMAIL);

        //            if (diretor2.cpf.IsValid)
        //                f.Diretores.Add(diretor2);

        //            var diretor3 = new Diretor();
        //            diretor3.nome = item.DIR3;
        //            diretor3.cpf = new CPF(item.CPFDIR3);
        //            diretor3.rg = item.RGDIR3;
        //            diretor3.email = new Email(item.EMAIL);

        //            if (diretor3.cpf.IsValid)
        //                f.Diretores.Add(diretor3);

        //            var diretor4 = new Diretor();
        //            diretor4.nome = item.DIR3;
        //            diretor4.cpf = new CPF(item.CPFDIR3);
        //            diretor4.rg = item.RGDIR3;
        //            diretor4.email = new Email(item.EMAIL);

        //            if (diretor4.cpf.IsValid)
        //                f.Diretores.Add(diretor4);

        //            var rep = new RepresentanteLegal();
        //            rep.nome = item.REPR;
        //            rep.cpf = new CPF(item.CPFREPR);
        //            rep.rg = item.RGREPR;
        //            rep.email = new Email(item.EMAIL);

        //            if (rep.cpf.IsValid)
        //                f.RepresentantesLegais.Add(rep);

        //            var referencia1 = new ReferenciaBancaria();
        //            referencia1.banco = item.BCO1;
        //            referencia1.agencia = item.AGBCO1;
        //            referencia1.contato = item.CONTBCO1;
        //            referencia1.telefone = new Telefone(item.FONEBCO1, "", TipoTelefone.comercial);

        //            if (!String.IsNullOrEmpty(referencia1.contato))
        //                f.ReferenciasBancarias.Add(referencia1);


        //            var referencia2 = new ReferenciaBancaria();
        //            referencia2.banco = item.BCO2;
        //            referencia2.agencia = item.AGBCO2;
        //            referencia2.contato = item.CONTBCO2;
        //            referencia2.telefone = new Telefone(item.FONEBCO2, "", TipoTelefone.comercial);

        //            if (!String.IsNullOrEmpty(referencia2.contato))
        //                f.ReferenciasBancarias.Add(referencia2);
        //            f.id_ultimo_status = 3;
        //            f.id_categoria = item.MODALIDADE == "S" ? 1 : 2;
        //            f.validade = item.VALIDADE;

        //            f.Documentos = new List<DocumentoFornecedor>();

        //            var doc102 = new DocumentoFornecedor();
        //            doc102.data = DateTime.Now;
        //            doc102.id_usuario = 17;
        //            doc102.id_documento = 102;
        //            doc102.validade = item.VALIDADE;

        //            f.Documentos.Add(doc102);


        //            var doc103 = new DocumentoFornecedor();
        //            doc103.data = DateTime.Now;
        //            doc103.id_usuario = 17;
        //            doc103.id_documento = 103;
        //            doc103.validade = item.VALIDADE;

        //            f.Documentos.Add(doc103);


        //            var doc104 = new DocumentoFornecedor();
        //            doc104.data = DateTime.Now;
        //            doc104.id_usuario = 17;
        //            doc104.id_documento = 104;
        //            doc104.validade = item.VALIDADE;

        //            f.Documentos.Add(doc104);


        //            var doc105 = new DocumentoFornecedor();
        //            doc105.data = DateTime.Now;
        //            doc105.id_usuario = 17;
        //            doc105.id_documento = 105;
        //            doc105.validade = item.VALIDADE;

        //            f.Documentos.Add(doc105);


        //            var doc106 = new DocumentoFornecedor();
        //            doc106.data = DateTime.Now;
        //            doc106.id_usuario = 17;
        //            doc106.id_documento = 106;
        //            doc106.validade = item.DAT5;
        //            doc106.numero = item.CN5;

        //            f.Documentos.Add(doc106);

        //            var doc107 = new DocumentoFornecedor();
        //            doc107.data = DateTime.Now;
        //            doc107.id_usuario = 17;
        //            doc107.id_documento = 107;
        //            doc107.validade = item.DAT4;
        //            doc107.numero = item.CN4;

        //            f.Documentos.Add(doc107);


        //            var doc108 = new DocumentoFornecedor();
        //            doc108.data = DateTime.Now;
        //            doc108.id_usuario = 17;
        //            doc108.id_documento = 108;
        //            doc108.validade = item.VALIDADE;

        //            f.Documentos.Add(doc108);


        //            if (f.id_categoria == 2)
        //            {
        //                var doc109 = new DocumentoFornecedor();
        //                doc109.data = DateTime.Now;
        //                doc109.id_usuario = 17;
        //                doc109.id_documento = 109;
        //                doc109.validade = item.DAT6;
        //                doc109.numero = item.CN6;

        //                f.Documentos.Add(doc109);

        //                var doc110 = new DocumentoFornecedor();
        //                doc110.data = DateTime.Now;
        //                doc110.id_usuario = 17;
        //                doc110.id_documento = 110;
        //                doc110.validade = item.DAT9;
        //                doc110.numero = item.CN9;

        //                f.Documentos.Add(doc110);

        //                var doc111 = new DocumentoFornecedor();
        //                doc111.data = DateTime.Now;
        //                doc111.id_usuario = 17;
        //                doc111.id_documento = 111;
        //                doc111.validade = item.DAT8;
        //                doc111.numero = item.CN8;

        //                f.Documentos.Add(doc111);

        //                var doc112 = new DocumentoFornecedor();
        //                doc112.data = DateTime.Now;
        //                doc112.id_usuario = 17;
        //                doc112.id_documento = 112;
        //                doc112.validade = item.DAT1;
        //                doc112.numero = item.CN1;

        //                f.Documentos.Add(doc112);
        //            }


        //            contexto.Fornecedores.Add(f);

        //            try
        //            {
        //                return contexto.SaveChanges() > 0;
        //            }
        //            catch (Exception)
        //            {
        //                string w = "";
        //                foreach (System.Data.Entity.Validation.DbEntityValidationResult erro in contexto.GetValidationErrors())
        //                {
        //                    foreach (System.Data.Entity.Validation.DbValidationError msg in erro.ValidationErrors)
        //                        w = msg.ErrorMessage;
        //                }
        //                return false;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}