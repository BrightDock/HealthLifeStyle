using System;
using System.Web;
using System.Web.Routing;

namespace WEB
{
    public partial class NotFound : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void search_box_TextChanged(object sender, EventArgs e)
        {

        }

        protected void search_button_Click(object sender, EventArgs e)
        {
            VirtualPathData search_vpd;
            RouteValueDictionary parameters_Search = new RouteValueDictionary{
                        { "words", search_box.Text.Replace('?', new char()) }
                    };
            search_vpd = RouteTable.Routes.GetVirtualPath(null, "Search", parameters_Search);
            HttpContext.Current.Response.Redirect(search_vpd.VirtualPath);
        }
    }
}