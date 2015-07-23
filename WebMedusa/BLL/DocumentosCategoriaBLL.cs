using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medusa.DAL;

namespace Medusa.BLL
{
    public class DocumentosCategoriaBLL : AbstractCrudWithLog<DocumentoCategoria>
    {
        public IEnumerable<DocumentoCategoria> GetDocumentosPorCategoria(int id_categoria)
        {
            var cat = _dbContext.Categorias.SingleOrDefault(it => it.id_categoria == id_categoria);
            if (cat != null)
                return cat.CategoriaDocumentos.OrderBy(it => it.Documento.nome).Select(it => it.Documento).ToList();
            else
                return new List<DocumentoCategoria>();
        }
    }
}
