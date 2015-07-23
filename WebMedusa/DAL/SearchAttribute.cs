using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    using System;
    using System.Reflection;
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchAttribute : System.Attribute
    {
        public string DisplayName { get; private set; }
        //= "DisplayName";

        public string ValueName { get; private set; }
        //= "ValueName";

        public SearchAttribute(string strDisplayName)     
        {
            DisplayName = strDisplayName;
        }

        public SearchAttribute(string strDisplayName, string strValueName)
        {
            DisplayName = strDisplayName;
            ValueName = strValueName;
        }
    }
}