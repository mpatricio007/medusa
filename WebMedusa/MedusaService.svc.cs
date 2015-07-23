using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using Medusa.DAL;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Medusa
{

    [AspNetCompatibilityRequirements(RequirementsMode =
        AspNetCompatibilityRequirementsMode.Allowed)]
    public class MedusaService : DataService<Contexto>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.UseVerboseErrors = true;
            config.SetServiceOperationAccessRule("GetTipoDocEntrada", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetCoordenadores", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetFinanciadores", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetCedentesTitulos", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetCedentesGuias", ServiceOperationRights.AllRead);
        }


        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public IQueryable<string> GetTipoDocEntrada(string name)
        {
            return CurrentDataSource.Entradas.Where(it => it.tipodocent.ToUpper().StartsWith(name.ToUpper())).Select(it => it.tipodocent).Distinct();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public IQueryable<string> GetCoordenadores(string name)
        {
            return CurrentDataSource.Coordenadores.Where(it => it.PessoaFisica.nome.StartsWith(name.ToUpper())).Select(it => it.PessoaFisica.nome).Distinct();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public IQueryable<string> GetFinanciadores(string name)
        {
            return CurrentDataSource.Financiadores.Where(it => it.nome.StartsWith(name.ToUpper())).Select(it => it.nome).Distinct();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public IQueryable<string> GetCedentesTitulos(string name)
        {
            return CurrentDataSource.RemessaTits.Where(it => it.nome_fav_cedente.StartsWith(name.ToUpper())).OrderBy(it => it.nome_fav_cedente).Select(it => it.nome_fav_cedente).Distinct();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public IQueryable<string> GetCedentesGuias(string name)
        {
            return CurrentDataSource.RemessaCons.Where(it => it.nome_fav_cedente.StartsWith(name.ToUpper())).OrderBy(it => it.nome_fav_cedente).Select(it => it.nome_fav_cedente).Distinct();
        }
    }
}
