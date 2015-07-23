using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;
using Medusa.LIB;

namespace Medusa.BLL
{
    public class DespesaImpostoConsumoBLL<T> : DespesaBLL<T> where T : DespesaImpostoConsumo, new()
    {
        public override bool DataIsValid(ref string strMsg)
        {
            bool rt = base.DataIsValid(ref strMsg);
            if ((!((int[])Enum.GetValues(typeof(IdentificacoesSegmentoImposto))).Contains(Util.StringToInteiro(ObjEF.Guia.IdentificacaoSegmento.codigo).GetValueOrDefault())) & !rt)
            {
                strMsg = "tipo de guia inválida";
                return false;
            }
            else
                return true;
        }
    }

    public enum IdentificacoesSegmentoImposto
    {
        Prefeituras = 1,
        OrgaosGovernamentais = 5
    }

    public enum IdentificacoesSegmentoConsumo
    {
        Saneamento = 2,
        LuzGas = 3,
        Telefone = 4
    }
}
