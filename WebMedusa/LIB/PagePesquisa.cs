using System.Web;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Linq.Expressions;
using Medusa.DAL;
using Medusa.BLL;
using System.Web.UI;
using System.Collections.Generic;
using System;

namespace Medusa.LIB
{
    public abstract class PagePesquisa<T> : System.Web.UI.UserControl where T : IBaseCrud, new()
    {
        public string PRIMARY_KEY { get; set; }
        public Panel pGrid { get; set; }
        public Label lbMsg { get; set; }
        public Button _btProcurar { get; set; }
        public ImageButton _btExcluiFiltro { get; set; }
        public GridView _grid { get; set; }
        public DropDownList _ddlSize { get; set; }
        public DropDownList _ddlOptions { get; set; }
        public DropDownList _ddlMode { get; set; }
        public Dictionary<string, string> dicProperties { get; set; }
        public TextBox _txtProcura { get; set; }
        private CheckBox ck;
        public CheckBox _ckFilter
        {
            get 
            {
                if (ck == null)
                    ck = new CheckBox();
                return ck; 
            }
            set { ck = value; }
        }
        public DataList _dataListFiltros { get; set; }
        protected List<Filter> filtros
        {
            get
            {
                if (ViewState["filtros"] == null)
                    filtros = new List<Filter>();
                return (List<Filter>)ViewState["filtros"];
            }
            set
            {
                ViewState["filtros"] = value;
            }
        }

        private T objBLL;

        public T ObjBLL
        {
            get
            {
                if (objBLL == null)
                    objBLL = new T();
                return objBLL;
            }
            set { objBLL = value; }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ViewState["SortExpression"] = PRIMARY_KEY;
                ViewState["SortDirection"] = "ASC";
                
                SetGrid();
                this.ddlOptions_SelectedIndexChanged(null, null);
                
            }
        }

        //protected virtual void msg(string msg)
        //{
        //    lbMsg.BackColor = System.Drawing.Color.Green;
        //    lbMsg.ForeColor = System.Drawing.Color.White;
        //    lbMsg.Text = string.Format("* {0} !", msg);
        //}

        //protected virtual void msgError(string msg)
        //{
        //    lbMsg.BackColor = System.Drawing.Color.Red;
        //    lbMsg.ForeColor = System.Drawing.Color.White;
        //    lbMsg.Text = string.Format("* {0} !?", msg);
        //}


        protected virtual void btProcurar_Click(object sender, EventArgs e)
        {
            if (!_ckFilter.Checked)
                filtros.Clear();

            if(!String.IsNullOrEmpty(this._txtProcura.Text))
                filtros.Add(setFilter());

            if (_ckFilter.Checked)
            {
                _dataListFiltros.DataBind(); 
                this._txtProcura.Text = String.Empty;
            }

            SetGrid();
        }


        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _grid.PageIndex = e.NewPageIndex;
            SetGrid();
        }

        protected void grid_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (ViewState["SortExpression"].Equals(e.SortExpression))
            {
                if (ViewState["SortDirection"].Equals("ASC"))
                    ViewState["SortDirection"] = "DESC";
                else
                    ViewState["SortDirection"] = "ASC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
                ViewState["SortExpression"] = e.SortExpression;
            }
            SetGrid();
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ddlSize.SelectedValue == "0")
                _grid.PageSize = 50;
            else
                _grid.PageSize = Convert.ToInt32(_ddlSize.SelectedValue);
            SetGrid();
        }

        protected void grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (!e.Row.Equals(null) & e.Row.RowType.Equals(DataControlRowType.Header))
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.HasControls())
                    {
                        LinkButton button = (LinkButton)cell.Controls[0];

                        if (!button.Equals(null))
                        {
                            Image image = new Image();
                            if (ViewState["SortExpression"].Equals(button.CommandArgument))
                            {
                                if (ViewState["SortDirection"].Equals("ASC"))
                                    image.ImageUrl = @"~/Styles/img/SortAsc.png";
                                else
                                    image.ImageUrl = @"~/Styles/img/SortDesc.png";

                                cell.Controls.Add(image);
                            }
                        }
                    }
                }
            }

        }

        protected virtual void SetGrid()
        {
            int size = 10 * Convert.ToInt32(this._ddlSize.SelectedValue);
            _grid.DataKeyNames = new string[] { PRIMARY_KEY };
            _grid.DataSource = ObjBLL.Find(
                filtros,
                (string)ViewState["SortExpression"],
                (string)ViewState["SortDirection"], size);
            _grid.DataBind();
        }

        protected void GetModes(Type tipo)
        {
            _ddlMode.DataTextField = "Value";
            _ddlMode.DataValueField = "Key";
            _ddlMode.DataSource = Filter.SearchModes[tipo];
            _ddlMode.DataBind(); 
        }

        protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetModes(ObjBLL.GetPropertyType(this._ddlOptions.SelectedValue));
        }

        protected void DataListFiltros_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            filtros.RemoveAt(e.Item.ItemIndex);
            _dataListFiltros.DataBind();
            SetGrid();
        }

        protected void DataListFiltros_DataBinding(object sender, EventArgs e)
        {
            _dataListFiltros.DataSource = filtros;
        }

        protected Filter setFilter()
        {
            try
            {
                Filter f = new Filter();
                f.property = dicProperties == null ? _ddlOptions.SelectedValue : dicProperties[_ddlOptions.SelectedValue];
                f.property_name = _ddlOptions.SelectedItem.Text;
                f.value = this._txtProcura.Text.ToUpper();
                f.mode = this._ddlMode.SelectedValue;
                f.mode_name = this._ddlMode.SelectedItem.Text;
                return f;
            }

            catch (Exception)
            {
                return new Filter();
            }
        }

        protected virtual void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var PkValue = _grid.DataKeys[e.NewEditIndex][PRIMARY_KEY];           
           filtros.Add(setFilterSelectedId(PkValue));
           SetGrid();
            _grid.DataBind();
            _dataListFiltros.DataBind();
            this._txtProcura.Text = String.Empty;
            e.Cancel = true;
        }

        protected Filter setFilterSelectedId(object value)
        {
            try
            {
                Filter f = new Filter();
                f.property = PRIMARY_KEY;
                f.property_name = "ID";
                f.value = Convert.ToString(value);
                f.mode = "==";
                f.mode_name = "igual";
                return f;
            }

            catch (Exception)
            {
                return new Filter();
            }
        }

        protected void ckFilter_CheckedChanged(object sender, EventArgs e)
        {          
            filtros.Clear();
            _dataListFiltros.DataBind();
            _btProcurar.Text = _ckFilter.Checked ? "adicionar filtro" : "procurar";
            _btProcurar.DataBind();
        }

    }
}

