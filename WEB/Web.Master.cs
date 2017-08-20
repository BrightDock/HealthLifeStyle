using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class Web : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Context.User.Identity.IsAuthenticated)
                {
                    load_login(true);
                    update_online_status("update");
                }
                else
                {
                    load_login(false);
                }
                load_sliders();
                this.Page.Header.Title = SiteMap.CurrentNode.Title;
            }
            catch
            {

            }
        }

        public String CompanyName
        {
            get { return (String)ViewState["companyName"]; }
            set { ViewState["companyName"] = value; }
        }

        void Page_Init(Object sender, EventArgs e)
        {
            this.CompanyName = "HealthyLifeStyle";
        }

        public void update_online_status(String login_status)
        {
            String commandstr = "UPDATE Users SET Users.Latest_online = ? WHERE (Users.ID = ?)";
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);

            if (Page.User.Identity.IsAuthenticated && login_status.Equals("update"))
            {
                Command.Parameters.Add("User.Latest_online", OleDbType.Date).Value = DateTime.Now.AddMinutes(15);
                Command.Parameters.Add("User.ID", OleDbType.BigInt).Value = get_user_id();
            }
            else if (Page.User.Identity.IsAuthenticated && login_status.Equals("logout"))
            {
                Command.Parameters.Add("User.Latest_online", OleDbType.Date).Value = DateTime.Now;
                Command.Parameters.Add("User.ID", OleDbType.BigInt).Value = get_user_id();
            }

            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        private String get_user_id()
        {
            return Page.User.Identity.Name;
        }

        protected void load_login(Boolean is_authorized)
        {
            if (is_authorized)
            {
                Panel user_data_logined_wrapper = new Panel() { CssClass = "user_data_logined_wrapper" };
                Panel user_data_name = new Panel() { CssClass = "user_data_name" };
                //                Panel user_data_surname = new Panel() { CssClass = "user_data_surname" };
                Panel user_info_avatar_wrapper = new Panel() { CssClass = "post_author_image_wrapper_prev " };
                Image user_avatar = new Image() { CssClass = "post_author_image_prev" };
                HyperLink user_page_link = new HyperLink() { CssClass = "user_page_link", Text = "Мой аккаунт" };
                HyperLink user_publication_link = new HyperLink() { CssClass = "user_page_link", Text = "Мои публикации" };
                HyperLink user_new_publication = new HyperLink() { CssClass = "user_page_link", Text = "Написать публикацию" };
                HyperLink user_chronologys_link = new HyperLink() { CssClass = "user_page_link", Text = "Мои результаты сегодня" };
                HyperLink user_settings_link = new HyperLink() { CssClass = "user_page_link", Text = "Мои результаты сегодня" };
                LinkButton Logoff = new LinkButton() { CssClass = "user_page_link", Text = "Выйти" };
                Logoff.Click += Logoff_Click;

                OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                String command = string.Format("SELECT Users.ID, Users.Name, Users.Surname, Users.Avatar FROM Users WHERE(ID = '{0}')", this.Page.User.Identity.Name);

                login_panel.Controls.Clear();

                try
                {
                    Connection.Open();

                    OleDbCommand Command = new OleDbCommand(command, Connection);
                    OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                    DataTable DT = new DataTable();
                    Adapter.Fill(DT);
                    Connection.Close();

                    user_data_name.Controls.Add(new Literal() { Text = DT.Rows[0]["Name"].ToString() });
                    //                    user_data_surname.Controls.Add(new Literal() { Text = DT.Rows[0]["Surname"].ToString() });

                    if (!(DT.Rows[0]["Avatar"].ToString().Equals(String.Empty)))
                        user_avatar.ImageUrl = DT.Rows[0]["Avatar"].ToString();
                    else
                        user_avatar.ImageUrl = "~/img/default_user.png";

                    loginBut.Controls.Add(user_info_avatar_wrapper);
                    user_info_avatar_wrapper.Controls.Add(user_avatar);

                    loginBut.Controls.Add(user_data_logined_wrapper);
                    user_data_logined_wrapper.Controls.Add(user_data_name);
                    //                    user_data_logined_wrapper.Controls.Add(user_data_surname);

                    RouteValueDictionary parameters_User = new RouteValueDictionary{
                        { "id", Convert.ToInt32(DT.Rows[0]["ID"].ToString()) }
                    };
                    VirtualPathData vpd_user = RouteTable.Routes.GetVirtualPath(null, "User", parameters_User);
                    user_page_link.NavigateUrl = vpd_user.VirtualPath;

                    RouteValueDictionary parameters_posts = new RouteValueDictionary{
                        { "id", Convert.ToInt32(DT.Rows[0]["ID"].ToString().TrimEnd()) }
                    };
                    VirtualPathData vpd_posts = RouteTable.Routes.GetVirtualPath(null, "My_Posts", null);
                    user_publication_link.NavigateUrl = vpd_posts.VirtualPath;

                    VirtualPathData vpd_new_post = RouteTable.Routes.GetVirtualPath(null, "Post_new", null);
                    user_new_publication.NavigateUrl = vpd_new_post.VirtualPath;

                    RouteValueDictionary parameters_chronologys = new RouteValueDictionary{
                        { "id", Convert.ToInt32(DT.Rows[0]["ID"].ToString().TrimEnd()) }
                    };
                    VirtualPathData vpd_chronologys = RouteTable.Routes.GetVirtualPath(null, "User_Chronologys", null);
                    user_chronologys_link.NavigateUrl = vpd_chronologys.VirtualPath;

                    RouteValueDictionary parameters_settings = new RouteValueDictionary{
                        { "id", Convert.ToInt32(DT.Rows[0]["ID"].ToString().TrimEnd()) }
                    };
                    VirtualPathData vpd_settings = RouteTable.Routes.GetVirtualPath(null, "User_Settings", null);
                    user_settings_link.NavigateUrl = vpd_settings.VirtualPath;

                    login_panel.Controls.Add(user_page_link);
                    user_page_link.Controls.Add(new Label() { Text = "Мой аккаунт" });
                    user_page_link.Controls.Add(new Label() { Text = "<i class='fa fa-user-circle-o' aria-hidden='true'></i>" });

                    login_panel.Controls.Add(user_publication_link);
                    user_publication_link.Controls.Add(new Label() { Text = "Мои публикации" });
                    user_publication_link.Controls.Add(new Label() { Text = "<i class='fa fa-comment' aria-hidden='true'></i>" });

                    login_panel.Controls.Add(user_new_publication);
                    user_new_publication.Controls.Add(new Label() { Text = "Новая публикация" });
                    user_new_publication.Controls.Add(new Label() { Text = "<i class='fa fa-pencil' aria-hidden='true'></i>" });

                    login_panel.Controls.Add(user_chronologys_link);
                    user_chronologys_link.Controls.Add(new Label() { Text = "Мои результаты сегодня" });
                    user_chronologys_link.Controls.Add(new Label() { Text = "<i class='fa fa-line-chart' aria-hidden='true'></i>" });

                    login_panel.Controls.Add(user_settings_link);
                    user_settings_link.Controls.Add(new Label() { Text = "Настройки" });
                    user_settings_link.Controls.Add(new Label() { Text = "<i class='fa fa-cog' aria-hidden='true'></i>" });

                    login_panel.Controls.Add(Logoff);
                    Logoff.Controls.Add(new Label() { Text = "Выход" });
                    Logoff.Controls.Add(new Label() { Text = "<i class='fa fa-sign-out' aria-hidden='true'></i>" });


                    //                    login_panel.Controls.Add(Login_status);
                    //                    Login_status.Controls.Add(new Label() { Text = "", CssClass = "user_page_link_pic" });

                }
                catch (Exception ex)
                {
                    login_panel.Controls.Add(new Literal() { Text = "Не сегодня :(<br />" });

                }
                finally
                {
                    Connection.Close();
                }
            }
            else if (!is_authorized)
            {
                Panel login_text = new Panel() { CssClass = "text", ID = "login_text" };
                Panel login_data = new Panel() { CssClass = "data", ID = "login_data" };
                Panel login_buttons = new Panel() { CssClass = "login_buttons", ID = "login_buttons" };

                TextBox Login = new TextBox() { ID = "Login", MaxLength = 40 };
                TextBox Password = new TextBox() { ID = "Password", MaxLength = 40, TextMode = TextBoxMode.Password };

                LinkButton Registration = new LinkButton() { ID = "Registr", PostBackUrl = "~/Account/Registration", Text = "Регистрация", OnClientClick = "Registration" };
                Button Submit = new Button() { ID = "Submit", Text = "Вход" };
                Submit.Click += Submit_Click;

                login_panel.Controls.Clear();

                loginBut.Controls.Add(new Literal() { Text = "Вход" });
                loginBut.HorizontalAlign = HorizontalAlign.Center;

                login_panel.Controls.Add(login_text);
                login_text.Controls.Add(new Literal() { Text = "<p>Логин:</p>" });
                login_text.Controls.Add(new Literal() { Text = "<p>Пароль:</p>" });

                login_panel.Controls.Add(login_data);
                login_data.Controls.Add(Login);
                login_data.Controls.Add(Password);

                login_panel.Controls.Add(login_buttons);
                login_buttons.Controls.Add(Registration);
                login_buttons.Controls.Add(Submit);

                this.form.DefaultButton = Submit.UniqueID;
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) // проверяем правильность введенных данных 
                return;

            new WEB.Account.Login().login(((TextBox)this.FindControl("Login")).Text, ((TextBox)this.FindControl("Password")).Text);
        }

        protected void Logoff_Click(object sender, EventArgs e)
        {
            update_online_status("logout");
            this.loginBut.Controls.Add(new Literal() { Text = "Вход" });
            new WEB.Account.Login().logOff();
        }
        
        protected void search_Click(object sender, EventArgs e)
        {
            RouteValueDictionary parameters_search = new RouteValueDictionary{
                    { "words", search_box.Text.Replace('?', new char()) }
                };
            VirtualPathData vpd_search = RouteTable.Routes.GetVirtualPath(null, "Search", parameters_search);

            this.Page.Response.RedirectToRoute("Search", parameters_search);

            this.Central_block_central_col.Controls.Add(new Literal() { Text = this.search.Text });
//            this.Response.Redirect(Request.RawUrl);

        }

        protected void load_sliders()
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String command = "SELECT Id, Name, Text, Image, Date_start, Date_end, Post_id, Text_background FROM Slides ORDER BY Id DESC";
            OleDbCommand Command = new OleDbCommand(command, Connection);
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();
            RouteValueDictionary parameters_Full_post;
            VirtualPathData vpd_full_post;
            Slide slide;
            DateTime Date_start;
            DateTime Date_end;
            int counter = 0;

            Connection.Open();
            Adapter.Fill(DT);
            Connection.Close();

            foreach (DataRow row in DT.Rows) {
                DateTime.TryParse(row["Date_start"].ToString(), out Date_start);
                DateTime.TryParse(row["Date_end"].ToString(), out Date_end);
                if (Date_start <= DateTime.Now || !DateTime.TryParse(row["Date_start"].ToString(), out Date_start))
                    if (Date_end >= DateTime.Now || !DateTime.TryParse(row["Date_end"].ToString(), out Date_end))
                    {
                        parameters_Full_post = new RouteValueDictionary{
                        { "post_id", Convert.ToInt32(row["Post_id"].ToString()) }
                    };
                        vpd_full_post = RouteTable.Routes.GetVirtualPath(null, "Post", parameters_Full_post);

                        slide = new Slide(row["Name"].ToString(), row["Text"].ToString(), row["Image"].ToString(), counter, vpd_full_post, row["Text_background"].ToString());
                        slide.Initialize(Slider);
                        counter++;
                    }
            }
        }
    }
}