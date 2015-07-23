using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Medusa.LIB
{
    public class GridViewTemplate : ITemplate
    {
        private DataControlRowType templateType;
        private string columnName;
        public GridViewTemplate(DataControlRowType type, string colname)
        {
            templateType = type;
            columnName = colname;
        }

        public GridViewTemplate(string colname)
        {
            templateType = DataControlRowType.DataRow;
            columnName = colname;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:                    
                    var lbItem = new Control();                    
                    lbItem.DataBinding += new EventHandler(this.LbItem_DataBinding1);
                    container.Controls.Add(lbItem);
                    break;


                default:
                    break;
            }
        }

        private void LbItem_DataBinding(Object sender, EventArgs e)
        {
            var l = (Label)sender;
            GridViewRow row = (GridViewRow)l.NamingContainer;
            var db = DataBinder.Eval(row.DataItem, columnName);
            l.Text = Convert.ToString(db);
        }   

        private void LbItem_DataBinding1(Object sender, EventArgs e)
        {
            var c = (Control)sender;
            GridViewRow row = (GridViewRow)c.NamingContainer;

            var tipoCampo = row.DataItem.GetType();
            string[] Properties = columnName.Split('.');


            tipoCampo = tipoCampo.GetProperty(Properties[0]).PropertyType;
            if (tipoCampo.IsGenericType)
            {
                if (tipoCampo.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var grid = new GridView();
                    grid.ShowHeader = false;
                    grid.AutoGenerateColumns = false;

                    var t = new TemplateField();
                    t.ItemTemplate = new GridViewTemplate(columnName.Replace(Properties[0] + ".", ""));

                    grid.Columns.Add(t);
                    grid.DataSource = DataBinder.Eval(row.DataItem, Properties[0]);
                    c.Controls.Add(grid);
                }
            }
            else
            {
                var l = new Label();
                var db = DataBinder.Eval(row.DataItem, columnName);
                l.Text = Convert.ToString(db);
                c.Controls.Add(l);
            }

            //foreach (var Property in Properties)
            //{
            //    tipoCampo = tipoCampo.GetProperty(Property).PropertyType;

            //    if (tipoCampo.IsGenericType)
            //    {
            //        if (tipoCampo.GetGenericTypeDefinition() == typeof(ICollection<>))
            //        {
            //            var grid = new GridView();
            //            grid.AutoGenerateColumns = false;

            //            var t = new TemplateField();
            //            t.ItemTemplate = new GridViewTemplate(columnName.Replace(Property + ".",""));

            //            grid.Columns.Add(t);
            //            grid.DataSource = DataBinder.Eval(row.DataItem, Property);
            //            c.Controls.Add(grid);

            //            break;
            //        }
            //    }
            //    else
            //    {

            //        var l = new Label();
            //        var db = DataBinder.Eval(row.DataItem, columnName);
            //        l.Text = Convert.ToString(db);
            //        c.Controls.Add(l);
            //        break;

            //    }
            //}
            
        }       
    }
}