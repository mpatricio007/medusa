using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medusa.DAL
{
    using System;
    using System.Reflection;
    [AttributeUsage(AttributeTargets.Property)]
    public class InvisibleAttribute : System.Attribute
    {       

    }
}