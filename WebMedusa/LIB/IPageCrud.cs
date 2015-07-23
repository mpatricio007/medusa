using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Linq.Expressions;
using Medusa.DAL;
using Medusa.BLL;
using System.Web.UI;

namespace Medusa.LIB
{
    public interface IPageCrud
    {   
        void GetExternal();
    }
}
