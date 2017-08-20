using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated.Equals(true))
            {
                this.Page.Response.Redirect("~/News");
            }
            this.Form.DefaultButton = Enter.ID;
            
        }

        public void LoginAction_Click(object sender, EventArgs e)
        {
            if (!IsValid) // проверяем правильность введенных данных 
                return;

            try
            {
                login(name.Text, password.Text);
                
            }
            catch (Exception ex)
            {
                Status_label.ForeColor = System.Drawing.Color.Red;
                Status_label.Attributes.Add("style", "font-size:18px;");
                Status_label.Text = "Неверный логин или пароль<br />";
            }
        }

        public void login(String login, String pass) {
            if (User.Identity.IsAuthenticated.Equals(false))
            {
                OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                String command = "SELECT ID FROM Users WHERE(Login = ''+?+'') AND (Password = ''+?+'')";
                OleDbCommand Command = new OleDbCommand(command, Connection);
                Command.Parameters.Add("Login", OleDbType.VarWChar).Value = login;
                Command.Parameters.Add("Password", OleDbType.VarWChar).Value = pass;
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();

                Connection.Open();

                Adapter.Fill(DT);
                Connection.Close();

                if (DT.Rows.Count == 0) // пароль или логин неверны 
                {
                    //                    Label1.Text = "Неверный пароль или логин – попробуйте еще раз";
                    HttpContext.Current.Response.Redirect("~/Account/Login");
                    return;
                }

                string id = DT.Rows[0][0].ToString();

                FormsAuthentication.RedirectFromLoginPage(id, true);
                HttpContext.Current.Response.Redirect("~/News");
            }
            else {
                HttpContext.Current.Response.Redirect("~/News");
            }
        }

        public void logOff()
        {
            if (User.Identity.IsAuthenticated.Equals(true))
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Response.Redirect("~/News");
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/News");
            }
        }
    }
}