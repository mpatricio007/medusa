using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;

namespace Medusa.DAL
{
    public static class DbSetExtensionsMethods
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortExpression, string sortDirection)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "it");

            string[] properties = sortExpression.Split('.');
            Expression body = Expression.PropertyOrField(parameterExpression, properties[0]);

            for (int i = 1; i < properties.Length; i++)
                body = Expression.Property(body, properties[i]);

            Type tipo = typeof(T);

            string[] Properties = sortExpression.Split('.');
            foreach (var Property in Properties)
                tipo = tipo.GetProperty(Property).PropertyType;
            body = Expression.Convert(body, tipo);
           
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), tipo);
            var expression = Expression.Lambda(delegateType, body, parameterExpression);
            var methodName = (sortDirection == "ASC") ? "OrderBy" : "OrderByDescending";

            return (IOrderedQueryable<T>)typeof(Queryable).GetMethods().Single(method =>
                method.Name == methodName
                && method.IsGenericMethodDefinition
                && method.GetGenericArguments().Length == 2
                && method.GetParameters().Length == 2).MakeGenericMethod(typeof(T), tipo).Invoke(null, new object[] { source, expression });
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, object obj, string strProperty, string strMode)
        {
            try
            {
                ParameterExpression entity = Expression.Parameter(typeof(T), "it");
                string[] properties = strProperty.Split('.');
                Expression keyValue = Expression.PropertyOrField(entity, properties[0]);
                
                for (int i = 1; i < properties.Length; i++)
                    keyValue = Expression.Property(keyValue, properties[i]);
          
                Expression pkValue;
                Expression body;
               
                if (keyValue.Type != typeof(string))
                {
                    var t = keyValue.Type;
                    t = Nullable.GetUnderlyingType(t) ?? t ;
                    pkValue = Expression.Constant(Convert.ChangeType(obj, t), keyValue.Type);
                    body = Filter.Expressions[strMode](keyValue, pkValue);
                }
                else
                {
                    pkValue = Expression.Constant(obj, keyValue.Type);
                    MethodInfo method = keyValue.Type.GetMethod(strMode, new[] { keyValue.Type });
                    body = Expression.Call(keyValue, method, pkValue);
                }

                var expression = Expression.Lambda<Func<T, bool>>(body, entity);
                return source.Where(expression).AsQueryable<T>();
            }
            catch (Exception)
            {
                return source.AsQueryable<T>();
            }

        }

        private static Expression GetExpression(ParameterExpression parenty, Filter property)
        {
            string[] properties = property.property.Split('.');
            Expression keyValue = Expression.PropertyOrField(parenty, properties[0]);
            Expression pkValue;

            for (int j = 1; j < properties.Length; j++)
                keyValue = Expression.Property(keyValue, properties[j]);

            var t = keyValue.Type;
        
            if (t != typeof(string))
            {
                var n = Nullable.GetUnderlyingType(t);
                pkValue = Expression.Constant(Convert.ChangeType(property.value, n != null ? n : t), t);
                pkValue = Filter.Expressions[property.mode](keyValue, pkValue);
            }
            else
            {

                pkValue = Expression.Constant(property.value, t);
                MethodInfo method = keyValue.Type.GetMethod(property.mode, new[] { t });
                pkValue = Expression.Call(keyValue, method, pkValue);
            }
            return pkValue;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, List<Filter> lstFiltros)
        {
            try
            {
                Expression exp = null;
                Expression block = null;
                ParameterExpression entity = Expression.Parameter(typeof(T));//, "it");
                var expressions = new List<Expression>();
                var ds = lstFiltros.GroupBy(it => it.property).ToList();
                foreach (var list in ds)
                {
                    var countModes = list.GroupBy(it => it.mode).Count();
                    for (int i = 0; i < list.Count(); i++)
                    {
                        var body = GetExpression(entity, list.ElementAt(i));
                        exp = i != 0 ? exp = countModes > 1 ? Expression.And(exp, body) : Expression.Or(exp, body) : body;
                    }
                    expressions.Add(exp);
                    exp = null;
                }
                for (int j = 0; j < expressions.Count(); j++)
                    block = j != 0 ? Expression.And(block, expressions[j]) : expressions[j];
                var expression = Expression.Lambda<Func<T, bool>>(block, entity);
                return source.Where(expression).AsQueryable<T>();
            }
            catch (Exception)
            {
                return source.AsQueryable<T>();
            }
        }



        //backup

        //public static IQueryable<T> Where<T>(this IQueryable<T> source, List<Filter> lstFiltros)
        //{
        //    try
        //    {
        //        Expression exp = null;
        //        Expression block = null;
        //        ParameterExpression entity = Expression.Parameter(typeof(T));
        //        var expressions = new List<Expression>();
        //        var ds = lstFiltros.GroupBy(it => it.property).ToList();
        //        foreach (var list in ds)
        //        {
        //            var countModes = list.GroupBy(it => it.mode).Count();
        //            for (int i = 0; i < list.Count(); i++)
        //            {
        //                var body = GetExpression2<T>(source, entity, list.ElementAt(i));
        //                exp = i != 0 ? exp = countModes > 1 ? Expression.And(exp, body) : Expression.Or(exp, body) : body;
        //            }
        //            expressions.Add(exp);
        //            exp = null;
        //        }
        //        for (int j = 0; j < expressions.Count(); j++)
        //            block = j != 0 ? Expression.And(block, expressions[j]) : expressions[j];
        //        var expression = Expression.Lambda<Func<T, bool>>(block, entity);
        //        return source.Where(expression).AsQueryable<T>();
        //    }
        //    catch (Exception)
        //    {
        //        return source.AsQueryable<T>();
        //    }
        //}

        //private static Expression GetSelectMany(ParameterExpression parenty, Filter f)
        //{

        //}



        //private static Expression GetExpression2<T>(IQueryable<T> source, ParameterExpression parenty, Filter f)
        //{
        //    string[] properties = f.property.Split('.');
        //    Expression keyValue = Expression.PropertyOrField(parenty, properties[0]);
        //    Expression pkValue;
            
        //    var t = keyValue.Type;
        //    if (t.IsGenericType)
        //    {               
        //        var z = t.GetGenericTypeDefinition();
        //        if (z == typeof(ICollection<>))
        //        {

                  
                   
        //            var func = typeof(Func<,>);
        //            var selector = func.MakeGenericType(new Type[] { typeof(T), t.GetGenericArguments()[0] });
        //            //var tmp = Activator.CreateInstance(selector);
        //            //source.SelectMany(tmp);
                           
        //            //foreach (PropertyInfo pi in tmp.GetType().GetProperties())
        //            //{
        //            //    try
        //            //    {

        //            //        //get the value of property and try 
        //            //        //to assign it to the property of T type object:
        //            //        tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
        //            //                                  pi.GetValue(tmp, null), null);
        //            //    }
        //            //    catch { }
        //            //}

        //            //var sss = tmp;
        //            //source.SelectMany(
        //            //var ds = source.GetType().InvokeMember("SelectMany", 
        //            //    BindingFlags.DeclaredOnly |
        //            //    BindingFlags.Public | BindingFlags.NonPublic |
        //            //    BindingFlags.Instance | BindingFlags.InvokeMethod, null, source, new object[] { selector });

        //            z = t.GetGenericArguments()[0];
        //            ParameterExpression entity = Expression.Parameter(z);
        //            var xxx = Expression.Lambda(selector.GetType(), entity);
        //            f.property = f.property.Replace(properties[0] + ".", "");
        //            //var x = Expression.Lambda<Func<T, (keyValue, entity);

        //            var body = GetExpression2<T>(source, entity, f);



        //        }
        //    }
        //    else
        //    {
        //        for (int j = 1; j < properties.Length; j++)
        //            keyValue = Expression.Property(keyValue, properties[j]);
        //        t = keyValue.Type;
        //    }

        //    if (t != typeof(string))
        //    {
        //        var n = Nullable.GetUnderlyingType(t);
        //        pkValue = Expression.Constant(Convert.ChangeType(f.value, n != null ? n : t), t);
        //        pkValue = Filter.Expressions[f.mode](keyValue, pkValue);
        //    }
        //    else
        //    {

        //        pkValue = Expression.Constant(f.value, t);
        //        MethodInfo method = keyValue.Type.GetMethod(f.mode, new[] { t });
        //        pkValue = Expression.Call(keyValue, method, pkValue);
        //    }
        //    return pkValue;
        //}


        //private static Expression<Func<T, K>> CreateExpression<T, K>(String propertyName)
        //{
        //    Type type = typeof(T);
        //    PropertyInfo pi = type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        //    if (pi == null) throw new ArgumentException("propertyName is not valid.");

        //    ParameterExpression argumentExpression = Expression.Parameter(type, "x");
        //    MemberExpression memberExpression = Expression.Property(argumentExpression, pi);
        //    LambdaExpression lambda = Expression.Lambda(memberExpression, argumentExpression);

        //    Expression<Func<T, K>> expression = (Expression<Func<T, K>>)lambda;

        //    return expression;
        //}
    }
}