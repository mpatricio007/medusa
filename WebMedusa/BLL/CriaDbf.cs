using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using System.Data.Entity;
using System.Data;
using System.Data.OleDb;
using Medusa.LIB;


namespace Medusa.BLL
{
    public class CriaDbf
    {
        public DateTime dt { get; set; }
        public List<DadosDBF> listaDBF { get; set; }
        public string nomearquivo { get; set; }
        public string diretorio { get; set; }


        public CriaDbf()
        {
            nomearquivo = "cmm";
            diretorio = "c:\bancobrasil";
        }

        public void CriarDBF()
        {
            try
            {
                //Microsoft.Jet.OLEDB.4.0

                string t = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties=dBASE IV", diretorio);
                OleDbConnection oConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=c:\bancobrasil;Extended Properties=dBASE IV");
                //OleDbConnection oConn = new OleDbConnection(string.Format(@t));
                oConn.Open();

                OleDbCommand cmd = new OleDbCommand(string.Format("delete from {0}",nomearquivo));
                cmd.Connection = oConn;
                cmd.ExecuteNonQuery();

                foreach (var item in listaDBF)
                {

                    StringBuilder commandSQL = new StringBuilder();
                    commandSQL.Append("INSERT INTO CMM(RD,PROJETO,ITEM,ISS,CALCINSS,PENSAO,VALINSS,INSS11,VALIR,DATA,HISTORICO,VALPAGO,VALCC,CPMF) VALUES (@RD,@PROJETO,@ITEM,@ISS,@CALCINSS,@PENSAO,@VALINSS,@INSS11,@VALIR,@DATA,@HISTORICO,@VALPAGO,@VALCC,@CPMF) ");
                    OleDbCommand cmdInsert = new OleDbCommand();
                    cmdInsert.Connection = oConn;
                    cmdInsert.CommandText = commandSQL.ToString();
                    cmdInsert.Parameters.AddWithValue("@RD", item.rp_rd);
                    cmdInsert.Parameters.AddWithValue("@PROJETO", item.projeto);
                    cmdInsert.Parameters.AddWithValue("@ITEM", item.item);
                    cmdInsert.Parameters.AddWithValue("@ISS", item.iss);
                    cmdInsert.Parameters.AddWithValue("@CALCINSS", item.calcinss);
                    cmdInsert.Parameters.AddWithValue("@PENSAO", item.pensao);
                    cmdInsert.Parameters.AddWithValue("@VALINSS", item.valinss);
                    cmdInsert.Parameters.AddWithValue("@INSS11", item.inss11);
                    cmdInsert.Parameters.AddWithValue("@VALIR", item.valir);
                    cmdInsert.Parameters.AddWithValue("@DATA", item.data);
                    cmdInsert.Parameters.AddWithValue("@HISTORICO", item.historico);
                    cmdInsert.Parameters.AddWithValue("@VALPAGO", item.valpago);
                    cmdInsert.Parameters.AddWithValue("@VALCC", item.valcc);
                    cmdInsert.Parameters.AddWithValue("@CPMF", item.cpmf);
                    cmdInsert.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                var log = new LogSistemaBLL();
                log.ObjEF.entidade = "CriarDBF";
                log.ObjEF.descricao = ex.Message;
                log.Add();
                log.SaveChanges();
            }
        }


    }

           
}