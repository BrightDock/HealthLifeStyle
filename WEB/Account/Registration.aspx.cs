using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Account
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            if (User.Identity.IsAuthenticated.Equals(true))
            {
                this.Page.Response.Redirect("~/News");
            }
        }

        protected void Add_account(object sender, EventArgs e)
        {
            Error_label.Text = String.Empty;
            if (Page.IsValid)
            {
                if (!user_name.Value.Equals(String.Empty))
                {
                    if (!user_surname.Value.Equals(String.Empty))
                    {
                        if (!user_login.Value.Equals(String.Empty))
                        {
                            if (!user_password.Value.Equals(String.Empty) && user_password.Value.Equals(user_password_check.Value))
                            {
                                try
                                {
                                    if (check_login_available(user_login.Value))
                                    {
                                        Create_user();
                                    }
                                    else
                                    {
                                        Error_label.ForeColor = System.Drawing.Color.Red;
                                        Error_label.Text += "Логин уже используется!<br />";
                                        user_login.Value = String.Empty;
                                        user_login.Focus();
                                    }

                                }
                                catch (Exception exeption)
                                {
                                    Error_label.ForeColor = System.Drawing.Color.Red;
                                    Error_label.Text += exeption.Message;
                                }
                            }
                            else
                            {
                                Error_label.ForeColor = System.Drawing.Color.Red;
                                Error_label.Text += "Пароль не введён или пароли не совпадают!<br />";
                                user_password.Value = String.Empty;
                                user_password_check.Value = String.Empty;
                                user_password.Focus();
                            }
                        }
                        else
                        {
                            Error_label.ForeColor = System.Drawing.Color.Red;
                            Error_label.Text += "Пожалуйста, введите логин!<br />";
                            user_login.Focus();
                        }
                    }
                    else
                    {
                        Error_label.ForeColor = System.Drawing.Color.Red;
                        Error_label.Text += "Пожалуйста, введите Фамилию!<br />";
                        user_surname.Focus();
                    }
                }
                else
                {
                    Error_label.ForeColor = System.Drawing.Color.Red;
                    Error_label.Text += "Пожалуйста, введите Имя!<br />";
                    user_name.Focus();
                }
            }
            
        }

        private bool check_login_available(String login) {
            String commandstr = "SELECT Users.Login FROM Users WHERE(Users.Login = ''+?+'')";
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            DataTable DT = new DataTable();
            Connection.Open();
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            Command.Parameters.Add("Users.Login", OleDbType.VarWChar).Value = login;
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            Adapter.Fill(DT);

            Connection.Close();
            if (DT.Rows.Count != 0)
                return false;
            else
                return true;
        }

        private void Create_user() {
            Error_label.Text += "Начинаем создавать!<br />";
            DateTime birthday = new DateTime( 1899, 1, 1 );
            DateTime.TryParse(user_birthday.Value, out birthday);


            Boolean sex = false;
            Boolean.TryParse(gender.Checked.ToString(), out sex);

            String Photo = "~/img/default_user.png";
            if (FileUpload.HasFile)
            {
                upload_avatar(user_login.Value, FileUpload);
                Photo = String.Format("~/UserAccounts/{0}/Images/{0}_avatar{1}", user_login.Value, new FileInfo(FileUpload.FileName).Extension);
//                Error_label.Text += Photo + "<br />";
            }

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("INSERT INTO Users (Name, Surname, Login, Password, Sex, Date_of_birth, Account_type_id, Avatar) " +
                "VALUES ( ''+?+'', ''+?+'', ''+?+'', ''+?+'', ?, ?, 2, ''+?+'' )", Connection);
            Command.Parameters.Add("Name", OleDbType.VarWChar).Value = user_name.Value;
            Command.Parameters.Add("Surname", OleDbType.VarWChar).Value = user_surname.Value;
            Command.Parameters.Add("Login", OleDbType.VarWChar).Value = user_login.Value;
            Command.Parameters.Add("Password", OleDbType.VarWChar).Value = user_password.Value;
            Command.Parameters.Add("Sex", OleDbType.Boolean).Value = sex;
            Command.Parameters.Add("Date_of_birth", OleDbType.DBDate).Value = birthday.Date;
            Command.Parameters.Add("Avatar", OleDbType.VarWChar).Value = Photo;

            
            Connection.Open();

//            Error_label.Text += Command.CommandText + "<br />" + Command.Parameters;
            Command.ExecuteNonQuery();
            Connection.Close();
            new Login().login(user_login.Value, user_password.Value);
        }

        private void upload_avatar(String user_login, FileUpload fu) {
            if (new FileInfo(fu.FileName).Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                new FileInfo(fu.FileName).Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                new FileInfo(fu.FileName).Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                String file_name_path = String.Empty;
                Directory.CreateDirectory(String.Format("{0}\\UserAccounts\\{1}\\Images", Request.PhysicalApplicationPath, user_login));

                file_name_path = String.Format("{0}\\UserAccounts\\{1}\\Images\\{1}_avatar{2}", Request.PhysicalApplicationPath, user_login, new FileInfo(fu.FileName).Extension);

                fu.SaveAs(file_name_path);
            }
            else
            {
                throw new Exception("Формат картинки не поддерживается.");
            }
        }
    }
}