using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        void Page_Load(Object sender, EventArgs e)
        {
            CompanyName.Text += ' ' + Master.CompanyName;
        }
    }
}