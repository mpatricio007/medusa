using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Medusa.LIB;
using Medusa.BLL;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace Medusa.DAL
{
    public class AbstractCrudWithLog<T> : Abstract_Crud<T> where T : class, new()
    {
        Dictionary<EntityState, string> dicTranslateAction = new Dictionary<EntityState, string>()
        {
            {EntityState.Added , "inclusão"},
            {EntityState.Deleted, "exclusão"},
            {EntityState.Modified, "alteração"},            
            {EntityState.Detached, "desatachado"},
            {EntityState.Unchanged, "não modificado"},            
        };

        public override bool SaveChanges()
        {
            LogSistemaBLL logDAL = new LogSistemaBLL(_dbContext);

            bool rt = false;
            var p = new StringBuilder();
            var dic = new Dictionary<string, string[]>();
            try
            {
                var dbEntry = _dbContext.Entry<T>(ObjEF);
                int index = ObjEF.GetType().Name.LastIndexOf("_");
                logDAL.ObjEF.entidade = index == -1 ? ObjEF.GetType().Name : ObjEF.GetType().Name.Substring(0, index);
                logDAL.ObjEF.acao = dicTranslateAction[dbEntry.State];

                var entries = _dbContext.ChangeTracker.Entries().Where(it => it.State == EntityState.Modified || it.State == EntityState.Deleted);

                GetChanges(entries,p);

                logDAL.ObjEF.alteracoes = p.ToString();
                string property = _dbContext.Entry<T>(ObjEF).State == EntityState.Added ? dbEntry.CurrentValues.PropertyNames.First() :
                   dbEntry.OriginalValues.PropertyNames.FirstOrDefault();
                DbPropertyEntry ds = dbEntry.Property(property);

                if ((int)ds.CurrentValue != 0)
                {
                    logDAL.ObjEF.id_entidade = Convert.ToInt32(ds.CurrentValue);
                    logDAL.Add();
                    rt = base.SaveChanges();
                }
                else
                {
                    rt = base.SaveChanges();
                    logDAL.ObjEF.id_entidade = Convert.ToInt32(ds.CurrentValue);
                    logDAL.Add();
                    logDAL.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logDAL.ObjEF.acao = "erro banco de dados";
                while (ex != null)
                {
                    logDAL.ObjEF.descricao = ex.Message;
                }
                logDAL.Add();
                logDAL.SaveChanges();
            }            
            return rt;
        }

        public AbstractCrudWithLog()
            : base()
        {

        }

        public AbstractCrudWithLog(Contexto ctx)
            : base(ctx)
        { 
        }

        private void GetChanges(IEnumerable<DbEntityEntry> entries, StringBuilder changes)
        {
            foreach (var e in entries)
            {
                var propertyValues = e.State == EntityState.Modified ? e.CurrentValues : e.OriginalValues;
                writeChangedValues("", propertyValues, e, changes);
            }
        }

        private void writeChangedValues(string parentProperty,DbPropertyValues propertyValues, DbEntityEntry dbEntry, StringBuilder changes)
        {
            
            foreach (var propertyName in propertyValues.PropertyNames)
            {

                var nestedValues = propertyValues[propertyName] as DbPropertyValues;
                if (nestedValues != null)
                {                    
                    writeChangedValues(String.Format("{0}.",propertyName), nestedValues, dbEntry, changes);
                }
                else
                {
                    var dbpropertyEntry = dbEntry.Property(String.Format("{0}{1}", parentProperty, propertyName));
                    writePropertyValues(parentProperty, dbpropertyEntry, changes);
                }
            }
        }

        private void writePropertyValues(string parentProperty,DbPropertyEntry dbpropertyEntry, StringBuilder changes)
        {
            //if (dbpropertyEntry.EntityEntry.State == EntityState.Modified)
            //{
            //if (dbpropertyEntry.IsModified)
            //{
            //    var z = dbpropertyEntry.EntityEntry.Entity.GetType();
            //    var t = z.GetProperty(dbpropertyEntry.Name);
            //    var x = t.GetCustomAttributes(typeof(SearchAttribute), true);
            //    var displayAttribute = x.FirstOrDefault() as SearchAttribute;
                
            //    //var displayAttribute = typeof(T).GetProperty(dbpropertyEntry.Name).GetCustomAttributes(typeof(SearchAttribute), true).FirstOrDefault() as SearchAttribute;
            //    var propertyName = displayAttribute == null ? /*String.Format("{0}{1}",parentProperty,*/dbpropertyEntry.Name : displayAttribute.DisplayName;
            //    changes.AppendFormat("{0} -> {1} -> {2} <br/>", propertyName, dbpropertyEntry.OriginalValue, dbpropertyEntry.CurrentValue);
            //}
            //}
            //else
            //    changes.AppendFormat("{0}{1} -> {2} <br>", parentProperty, dbpropertyEntry.Name, dbpropertyEntry.OriginalValue);
        }
    }
}
    