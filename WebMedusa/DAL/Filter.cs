using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Medusa.DAL
{
    [Serializable]
    public class Filter
    {
        public string property { get; set; }
        public string property_name { get; set; }
        public string value { get; set; }

        private string DisplayValue;
        
        public string displayValue {
            get
            {
                if (String.IsNullOrEmpty(DisplayValue))
                    return value;
                else
                    return DisplayValue;
            }

            set
            {
                DisplayValue = value;
            } 
        }
        public string mode { get; set; }
        public string mode_name { get; set; }


        public static Dictionary<String, Func<Expression, Expression, Expression>> Expressions = new Dictionary<String, Func<Expression, Expression, Expression>>()
        {
            {"==", Expression.Equal                 },
            {">",  Expression.GreaterThan           },
            {"<",  Expression.LessThan              },
            {">=", Expression.GreaterThanOrEqual    },
            {"<=", Expression.LessThanOrEqual       }, 
            {"!=", Expression.NotEqual              },
            {"&&", Expression.And                   },
            {"||", Expression.Or                    }            
        };

        public static Dictionary<string, string> StrModes = new Dictionary<string, string>()
        {       
            {"Contains","que contém"                 },
            {"StartsWith" ,"começando com"           },
            {"EndsWith","terminando com"             },
            {"Equals","igual"                        },
      
        };

        public static Dictionary<string, string> Modes = new Dictionary<string, string>()
        {
                {"==","igual"               },
                {">","maior"                },     
                {"<","menor"                },
                {">=","maior ou igual"           },
                {"<=","menor ou igual"           },
                {"!=","diferente"           },
                
        };

        public static Dictionary<string, string> BoolModes = new Dictionary<string, string>()
        {
                {"==","igual"               },                
                {"!=","diferente"           },
                
        };

        public static Dictionary<Type, Dictionary<string, string>> SearchModes = new Dictionary<Type, Dictionary<string, string>>()
        {
            {typeof(string),StrModes      },     
            {typeof(int),Modes            },
            {typeof(DateTime),Modes       },
            {typeof(decimal),Modes        },
            {typeof(int?),Modes           },
            {typeof(DateTime?),Modes      },
            {typeof(decimal?),Modes       },
            {typeof(bool),BoolModes       },
            {typeof(bool?),BoolModes      },
            {typeof(ICollection<>),StrModes},
        };

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            var f = obj as Filter;
            if ((System.Object)f == null)

                return false;

            return
              (property == f.property)
              //&& (property_name == f.property_name)
              && (value == f.value)
              //&& (displayValue == f.displayValue)
              && (mode == f.mode);
              //&& (mode_name == f.mode_name);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}