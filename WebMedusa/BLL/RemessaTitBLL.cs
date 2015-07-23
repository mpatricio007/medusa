using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medusa.DAL;
using Medusa.BLL.RemessaBB;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class RemessaTitBLL : RemessaBLL<RemessaTit>
    {
        public bool BoletoJaFoiCadastrado()
        {
            try
            {
                return Find(b => b.codbarra == ObjEF.Boleto.RepresentacaoNumerica &  (b.id_tipo_ret != TipoRetornoBLL.Estornado & b.id_tipo_ret != TipoRetornoBLL.Rejeitado) & b.id_remessa != ObjEF.id_remessa).Count() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RetornoEhValido(SegmentoJ segJ)
        {            
            bool rt = Exists();
            if (rt)
            {
                rt = segJ.campo_livre == ObjEF.Boleto.CampoLivre() & Util.StringSemBarrasToDate(segJ.data_vencto) == ObjEF.dataVencto
                    & Util.StringSemPontosToDecimal(segJ.valor_titulo) == ObjEF.valor;
            }
            return rt;
        }

        public override bool DataIsValid(ref string strMsg)
        {
            if (BoletoJaFoiCadastrado())
            {
                strMsg = String.Format("Este boleto já foi cadastrado!");
                return false;
            }

            return base.DataIsValid(ref strMsg);
        }

        public override bool Agendar(ref string msg)
        {
            ObjEF.id_forma_pagto = ObjEF.id_banco_destino ==/* ObjEF.Lote.Conta.BancoAgencia.id_banco*/ BancoBLL.BANCO_DO_BRASIL ?
                FormaPagtoBLL.BOLETO_MESMO_BANCO : FormaPagtoBLL.BOLETO_OUTRO_BANCO;

            return base.Agendar(ref msg);
        }

        public RemessaTitBLL(Contexto _dbContext)
        {
            // TODO: Complete member initialization
            this._dbContext = _dbContext;
            this._dbSet = _dbContext.RemessaTits;
        }

        public RemessaTitBLL()
        {
        }
    }
}
                       