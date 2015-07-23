using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Medusa.DAL
{
    public static class StringExtensionMethods
    {
        private static byte[] chave = { };
        private static byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

        private const string initVector = "tu89geji340t89u2";
       
        private const int keysize = 256;

        public static string Criptografar(this string valor)
        {

            string rt = valor;
          
            try
            {
                var s = new LIB.SimpleAES();
                return s.EncryptToString(valor);
            }
            catch (Exception)
            {
                return rt;
            }
        }

        public static string DesCriptografar(this string valor)
        {
            string rt = valor;
            
            try
            {

                var s = new LIB.SimpleAES();
                return s.DecryptString(valor);
            }
            catch (Exception)
            {
                return rt;
            }
        }

        public static string OldCriptografar(this string valor)
        {
            string rt = valor;
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs;
            byte[] input;
            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = Encoding.UTF8.GetBytes(valor);
                chave = Encoding.UTF8.GetBytes(LIB.Constantes.CHAVE_CRIPTOGRAFICA);
                cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception)
            {
                return rt;
            }
        }


        public static string somenteNumeros(this string text)
        {
            var numeros = "0123456789";
            var saida="";
            for (int i = 0; i < text.Length; i++)
            {
                if (numeros.Contains(text[i])) saida = String.Format("{0}{1}",saida,text[i]);
            }
            return saida;
        }


        public static string parsetext(this string text)
        {
            bool allow = true;
            //Create a StringBuilder object from the string input
            //parameter
            StringBuilder sb = new StringBuilder(text);
            //Replace all double white spaces with a single white space
            //and &nbsp;
            sb.Replace("  ", " &nbsp;");
            //Check if HTML tags are not allowed
            if (!allow)
            {
                //Convert the brackets into HTML equivalents
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                //Convert the double quote
                sb.Replace("\"", "&quot;");
            }
            //Create a StringReader from the processed string of
            //the StringBuilder
            StringReader sr = new StringReader(sb.ToString());
            StringWriter sw = new StringWriter();
            //Loop while next character exists
            while (sr.Peek() > -1)
            {
                //Read a line from the string and store it to a temp
                //variable
                string temp = sr.ReadLine();
                //write the string with the HTML break tag
                //Note here write method writes to a Internal StringBuilder
                //object created automatically
                sw.Write(temp + "<br>");
            }
            //Return the final processed text
            return sw.GetStringBuilder().ToString();
        }

        

        public static string OldDesCriptografar(this string valor)
        {
            string rt = valor;
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs;
            byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();
                input = new byte[valor.Length];
                input = Convert.FromBase64String(valor.Replace(" ", "+"));
                chave = Encoding.UTF8.GetBytes(LIB.Constantes.CHAVE_CRIPTOGRAFICA);
                cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return rt;
            }
        }

        public static string Left(this string text, int length)
        {
            try
            {
                return text.Substring(0, length);
            }
            catch (Exception)
            {
                return text;
            }
        }
        public static string Right(this string text, int length)
        {
            try
            {
                return text.Substring(text.Length - length, length);
            }
            catch (Exception)
            {   
                return text;
            }
        }

        public static string RemoverAcentos(this string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(text);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}