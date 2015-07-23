using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Web.UI.WebControls;
using Medusa.LIB;
using System.Web.Security;
using System.Security;

namespace Medusa.BLL
{
    public class SecurityBLL
    {
        public static string GetSha1Hash(string input)
        {   
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = sha1.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public static string GeraSenha()
        {
            Random rd = new Random(System.DateTime.Now.Millisecond);
            StringBuilder senha = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                char ch = Convert.ToChar(rd.Next('A', 'Z' + 1));
                if (i % 2 == 0)
                    ch = Convert.ToChar(ch.ToString().ToLower());
                senha.Append(ch);
            }
            return senha.ToString();
        }

        public static bool GetPermission(string strUrl, out bool bGravacao)
        {  
            var usu = SecurityBLL.GetCurrentUsuario();
            bGravacao = false;
            if (usu == null)
                return false;
            else if (usu.nivel == 1)
            {
                bGravacao = true;
                return true;
            }
            else
            {
                strUrl = strUrl.ToUpper().Replace("ASP.", "");
                strUrl = strUrl.Contains("_ASPX") ? strUrl.Replace("_ASPX", ".ASPX") : strUrl.Replace("_ASCX", ".ASCX");
                strUrl = strUrl.Replace("_", "/");  

                var ds = from us in usu.UsuarioSistema
                        from m in us.Sistema.Menus
                        from mp in m.MenuPaginas                        
                        where mp.Pagina.url.ToUpper() == strUrl
                        select mp;

                MenuPagina objMp = ds.FirstOrDefault();
                if (objMp == null)
                {
                    bGravacao = false;
                    return false;
                }
                else
                {
                    bGravacao = objMp.gravacao;
                    return objMp.leitura;
                }
            }
        }

        public static int Login(string strLogin, string strSenha, out string saida, out string url)
        {
            var ctx = new Contexto();
            strSenha = SecurityBLL.GetSha1Hash(strSenha);
            var user = ctx.Usuarios.Where(it => it.login == strLogin & it.senha == strSenha & it.status == true).FirstOrDefault();

            url = String.Empty;
            
            if (user is UsuarioFusp)            
                url = "Principal.aspx";
            else if (user != null)
            {
                url = user.UsuarioSistema.FirstOrDefault().Sistema.Pagina.url;
                System.Web.HttpContext.Current.Session["id_sistema"] = user.UsuarioSistema.FirstOrDefault().Sistema.id_sistema;
            }

            saida = user == null ? "Senha e/ou Login inválidos!" : String.Empty;
            return user != null ? user.id_usuario : 0;
        }

        public static int Login(string strLogin, string strSenha)
        {
            var ctx = new Contexto();
            strSenha = SecurityBLL.GetSha1Hash(strSenha);
            var user = ctx.Usuarios.Where(it => it.login == strLogin & it.senha == strSenha & it.status == true).FirstOrDefault();        
            return user != null ? user.id_usuario : 0;
        }


        public static string AlterarSenha(string strOldSenha, string strNewSenha)
        {
            string saida = String.Empty;            
            try
            {
                int intId_usuario = SecurityBLL.GetCurrentUsuario().id_usuario;
                Contexto ctx = new Contexto();
                Usuario usu = ctx.Usuarios.Where(it => it.id_usuario == intId_usuario).FirstOrDefault();
                if (usu != null)
                {
                    if (usu.senha == SecurityBLL.GetSha1Hash(strOldSenha))
                    {
                        usu.primeiro_acesso = false;
                        usu.senha = SecurityBLL.GetSha1Hash(strNewSenha);
                        if (ctx.SaveChanges() > 0)
                        {
                            saida = "Senha alterada com sucesso!";
                            Medusa.LIB.Util.ResponseToLogin(saida);
                            System.Web.HttpContext.Current.Session.Clear();
                            FormsAuthentication.SignOut();
                        }
                    }
                    else
                    {
                        saida = "Senha atual incorreta!";
                    }
                }
                else
                    saida = "Usuário não logado!";
            }
            catch (Exception ex)
            {
                saida = ex.Message;
            }          
            return saida;
        }

        public static Usuario GetCurrentUsuario()
        {
            UsuarioBLL usuBLL = new UsuarioBLL();
            usuBLL.Get(Convert.ToInt32(System.Web.HttpContext.Current.Session["id_usuario"]));
            if (usuBLL.ObjEF.id_usuario == 0)
                Medusa.LIB.Util.ResponseToLogin("Sua sessão acabou!");
            return usuBLL.ObjEF;
        }

        public static int GetCurrentSistema()
        {
            try 
	        {	        
		         return Convert.ToInt32(System.Web.HttpContext.Current.Session["id_sistema"]);
	        }
	        catch (Exception)
	        {
		
		        return 0;
	        }
        }


        public static int GetIdUsuarioAutomatic()
        {
            try
            {
                return Login("automatic", "pankada");
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public static string SendPasswordEmail(Usuario usu)
        {
            string rt = String.Empty;

            try
            {
                if (usu.id_usuario != 0)
                {
                    if (usu.status)
                    {
                        var email = usu.PessoaFisica.Emails.FirstOrDefault();
                        if (email != null)
                        {
                            string pass = SecurityBLL.GeraSenha();
                            SendEmail sendmail = new SendEmail(ContaEmail.fusp);
                            sendmail.Destinatarios = new string[] { email.email.value };
                            sendmail.Subject = "Recuperação de acesso ao Sistema da FUSP";

                            StringBuilder body = new StringBuilder();
                            body.Append("Esta é uma mensagem automática do sistema. Não responda este e-mail.<br />");
                            body.AppendLine();
                            //body.AppendFormat("Sua senha para acesso ao <a href='http://medusa.intranet.fusp.br'>Sistema Medusa</a> da FUSP é: {0}.<br />", pass);
                            body.Append("Seus dados para acesso ao sistema da FUSP são:<br />");
                            body.AppendFormat("<b>Usuário:</b> {0}<br />", usu.login);
                            body.AppendFormat("<b>Senha:</b> {0}<br />", pass);
                            body.AppendLine();
                            body.Append("Para acessar o sistema digite o seu login e em seguida informe a senha.");

                            sendmail.Body = body.ToString();

                            if (sendmail.Send())
                            {
                                var usuBLL = new AbstractCrudWithLog<Usuario>();
                                usuBLL.Get(usu.id_usuario);
                                System.Web.HttpContext.Current.Session["id_usuario"] = usuBLL.ObjEF.id_usuario;
                                usuBLL.ObjEF.senha = SecurityBLL.GetSha1Hash(pass);
                                usuBLL.ObjEF.primeiro_acesso = true;
                                usuBLL.SaveChanges();
                                rt = String.Format("Uma nova senha foi enviada para seu e-mail {0}.", email.email.value);
                                System.Web.HttpContext.Current.Session.Clear();
                                FormsAuthentication.SignOut();
                            }
                        }
                        else
                            rt = "E-mail não cadastrado!";
                    }
                    else
                        rt = "Usuário inativo!";
                }
                

                else
                {
                    rt = "Usuário inexistente!";
                }
            }
            catch (Exception ex)
            {
                rt = "Erro! " + ex.Message;
            }
            return rt;
        }


        public static Setor GetCurrentSetor(int id_usuario)
        {
            SetorBLL s = new SetorBLL();
            return s.Find(it => it.UsuarioFusp.Select(x => x.id_usuario).Contains(id_usuario)).FirstOrDefault();
        }
    }
}