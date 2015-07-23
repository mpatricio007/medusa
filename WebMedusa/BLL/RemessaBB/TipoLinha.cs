using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.BLL.RemessaBB
{
    /// <summary>
    ///  Tipo de cada linha do Arquivo
    ///  Como não é possível criar enum de strings, criei enta classe que devolve os tipos
    /// </summary>
    public sealed class TipoLinha
    {
        public readonly string name;
        public readonly string value;
        public readonly string codigo;


        public static readonly TipoLinha HeaderArquivo = new TipoLinha("0", "HeaderArquivo");
        public static readonly TipoLinha HeaderLote = new TipoLinha("1", "HeaderLote");
        public static readonly TipoLinha TrailerLote = new TipoLinha("5", "TrailerLote");
        public static readonly TipoLinha TrailerArquivo = new TipoLinha("9", "TrailerArquivo");
        public static readonly TipoLinha SegmentoJ = new TipoLinha("3", "Segmento","J");
        public static readonly TipoLinha SegmentoZ = new TipoLinha("3", "Segmento", "Z");
        public static readonly TipoLinha SegmentoO = new TipoLinha("3", "Segmento", "O");
        public static readonly TipoLinha SegmentoN = new TipoLinha("3", "Segmento", "N");
        public static readonly TipoLinha SegmentoA = new TipoLinha("3", "Segmento", "A");
        public static readonly TipoLinha SegmentoB = new TipoLinha("3", "Segmento", "B");
        public static readonly TipoLinha SegmentoW = new TipoLinha("3", "Segmento", "W");


        public static Dictionary<string, TipoLinha> GetLinha = new Dictionary<string, TipoLinha>()
        {
            { "0", TipoLinha.HeaderArquivo },
            { "1", TipoLinha.HeaderLote },
            { "5", TipoLinha.TrailerLote },
            { "9", TipoLinha.TrailerArquivo },
            { "3J", TipoLinha.SegmentoJ },
            { "3Z", TipoLinha.SegmentoZ },
            { "3O", TipoLinha.SegmentoO },
            { "3A", TipoLinha.SegmentoA },
            { "3N", TipoLinha.SegmentoN },
            { "3B", TipoLinha.SegmentoB },
            { "3W", TipoLinha.SegmentoW }

        };

        private TipoLinha(string value, string name,string codigo = "")
        {
            this.name = name;
            this.value = value;
            this.codigo = codigo;
        }

        public override String ToString()
        {
            return name;
        }

        public static TipoLinha GetTipoLinha(string strLinha)
        {
            string key = strLinha.Substring(13, 1);
            key = String.IsNullOrWhiteSpace(key) || key == "0" ? String.Empty : key;
            key = String.Format("{0}{1}", strLinha.Substring(7, 1), key);
            var x = TipoLinha.GetLinha[key].ToString();
            return TipoLinha.GetLinha[key];
        }

    }
}