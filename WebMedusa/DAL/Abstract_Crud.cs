using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Medusa.LIB;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Medusa.DAL
{
    public abstract class Abstract_Crud<T> : IBaseCrud where T : class,new()
    {
        protected  IDbSet<T> _dbSet;
        protected  Contexto _dbContext;
        
        protected  T objEF;

        public virtual T ObjEF
        {
            get
            {
                if (objEF == null)
                    objEF = new T();

                return objEF;
            }
            set { objEF = value; }
        }      

        public Abstract_Crud()
        {
            _dbContext = new Contexto();
            _dbSet = _dbContext.Set<T>();            
        }

        public Abstract_Crud(Contexto ctx)
        {
            _dbContext = ctx;
            _dbSet = _dbContext.Set<T>();
        }

        #region IRepository<T> Members

        public virtual void Add()
        {
            //if (DataIsValid())
                _dbSet.Add(objEF);
        }

        public virtual void Delete()
        {
            ////if (DataIsValid())
                _dbSet.Remove(objEF);
        }

        public virtual void Get(object Id)
        {
            ObjEF = _dbSet.Find(Id);            
        }

        public virtual void Update() 
        {
            //if (!DataIsValid())
            //    _dbContext.Entry<T>(ObjEF).State = EntityState.Unchanged;
        }

        public virtual void Detach()
        {
            _dbContext.Entry<T>(ObjEF).State = EntityState.Detached;
        }

        public virtual void Attach()
        {
            ObjEF = _dbSet.Attach(ObjEF);
        }

        public virtual void Clone()
        {
            objEF = (T)_dbContext.Entry<T>(ObjEF).GetDatabaseValues().ToObject();
        }

        public virtual IEnumerable GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable GetAll(string sortExpression)
        {
            return _dbSet.OrderBy(sortExpression, "ASC").ToList();
        }

        public virtual IEnumerable GetAll(string sortExpression, string sortDirection)
        {
            return _dbSet.OrderBy(sortExpression, sortDirection).ToList();
        }

        public virtual IEnumerable GetAll(string sortExpression, string sortDirection,int top)
        {
            return _dbSet.OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }

        public virtual bool SaveChanges()
        {            
            try
            {    
                return (_dbContext.Entry<T>(ObjEF).State == EntityState.Unchanged) ? true : _dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                string w = "";
                foreach (System.Data.Entity.Validation.DbEntityValidationResult erro in _dbContext.GetValidationErrors())
                {
                    foreach (System.Data.Entity.Validation.DbValidationError msg in erro.ValidationErrors)
                        w = msg.ErrorMessage;
                }
                return false;
            }
            
        }

        public virtual IEnumerable Find(List<Filter> lstFilters,string sortExpression, string sortDirection, int top)
        {
            if (top == 0)
                return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).ToList();
            else
                 return _dbSet.Where(lstFilters).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }

        public IEnumerable Find(string strContent, string strProperty, string strMode, string sortExpression, string sortDirection, int top)
        {
            if (top == 0)
                return _dbSet.Where(strContent, strProperty, strMode).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbSet.Where(strContent, strProperty, strMode).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }

        public IEnumerable Find(string strContent, string strProperty, string strMode, string sortExpression, string sortDirection, int top, string strContentDefault, string strPropertyDefault, string strModeDefault)
        {            
            
            if(top == 0)
                return _dbSet.Where(strContent, strProperty, strMode).Where(strContentDefault, strPropertyDefault, strModeDefault).OrderBy(sortExpression, sortDirection).ToList();
            else
                return _dbSet.Where(strContent, strProperty, strMode).Where(strContentDefault, strPropertyDefault, strModeDefault).OrderBy(sortExpression, sortDirection).Take(top).ToList();
        }

        public IEnumerable<T> Find(Expression<System.Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }
    
        public Type GetPropertyType(string propertyName)
        {
            //Type tipoCampo = typeof(T);
            //string[] Properties = propertyName.Split('.');

            //foreach (var Property in Properties)
            //    tipoCampo = tipoCampo.GetProperty(Property).PropertyType;

            //return tipoCampo;

            Type tipoCampo = typeof(T);
            string[] Properties = propertyName.Split('.');

            foreach (var Property in Properties)
            {
                tipoCampo = tipoCampo.GetProperty(Property).PropertyType;
                if (tipoCampo.IsGenericType)
                {
                    if (tipoCampo.GetGenericTypeDefinition() == typeof(ICollection<>))
                        foreach (var item in tipoCampo.GetGenericArguments())
                        {
                            tipoCampo = item;
                        }
                }
            }
            return tipoCampo;

        }

        public List<SearchAttribute> GetProperties()
        {
            Type tipoCampo = typeof(T);
            var lst = new List<SearchAttribute>();
            foreach (var prop in tipoCampo.GetProperties())
            {
                if (prop.GetCustomAttributes(typeof(InvisibleAttribute), true).FirstOrDefault() != null)
                    continue;
                var attr = prop.GetCustomAttributes(typeof(SearchAttribute), true).FirstOrDefault() as SearchAttribute;
                attr = attr ?? new SearchAttribute(prop.Name, prop.Name);
                lst.Add(attr);
            }
            return lst.OrderBy(it => it.DisplayName).ToList();
        }
        #endregion


        public virtual bool DataIsValid()
        {
            return true;
        }


        public virtual bool DataIsValid(ref string strMsg)
        {            
            return true;
        }
    }    
}
