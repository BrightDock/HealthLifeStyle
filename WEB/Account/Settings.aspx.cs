using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Настройки | " + this.Master.CompanyName;
            if (this.Context.User.Identity.IsAuthenticated)
            {
                load_setting();
            }
            else
            {
                not_autorized();
            }
        }

        private String get_id()
        {
            String result = String.Empty;

            result = Page.User.Identity.Name;

            return result;
        }

        private String find_weigth(String id)
        {
            String result = String.Empty;

            String commandstr = "SELECT Weight FROM User_weight_chronology WHERE (User_ID = ?)";

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            Command.Parameters.Add("User_ID", OleDbType.BigInt).Value = id;
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();

            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                foreach (DataRow row in DT.Rows)
                {
                    result = String.Format("{0}", row["Weight"].ToString().TrimEnd());
                }
            }
            catch
            {

            }

            return result;
        }

        private void not_autorized()
        {
            Panel non_autorized = new Panel() { CssClass = "non_autrized_panel" };
            
            this.Content.ContentTemplateContainer.Controls.Add(non_autorized);
            non_autorized.Controls.Add(new Label() { Text = "Пожалуйста,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Login", Text = "Войдите", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "или", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Registration", Text = "Зарегистрируйтесь,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "чтобы просмотреть эту страницу", CssClass = "non_autorized_msg_parts" });

        }

        private void load_setting()
        {
            String commandstr = "SELECT ID, Name, Surname, Login, Password, Sex, Date_of_birth, Height, Account_type_id, Avatar, About FROM Users WHERE (ID = ?)";

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            Command.Parameters.Add("ID", OleDbType.BigInt).Value = get_id();
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();

            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();
                Panel posts_plase = new Panel();

                foreach (DataRow row in DT.Rows)
                {
                    String Weight = find_weigth(row["ID"].ToString());

                    Settings_part settings_part = new Settings_part(
                        row["Name"].ToString().TrimEnd(),
                        row["Surname"].ToString().TrimEnd(),
                        Convert.ToDateTime(row["Date_of_birth"]),
                        Convert.ToBoolean(row["Sex"]),
                        row["Password"].ToString(),
                        row["Avatar"].ToString().TrimEnd(),
                        row["Height"].ToString().TrimEnd(),
                        Weight,
                        row["About"].ToString().TrimEnd()
                    );

                    settings_part.Initialize(Content);
                }
            }
            catch (Exception exeption)
            {
                Content.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, пожалуйста перезагрузите<br />" + exeption.Message + "<br />" });
            }
        }
        public void redirect_to(String URL) {
            Response.Redirect(URL, true);
        }
    }

    public partial class Settings_part : System.Web.UI.Page
    {
        private UpdatePanel settings_wrapper { get; set; } = new UpdatePanel() { ID = "settings_wrapper" };
        private Panel Messages_panel { get; set; } = new Panel() { Visible = false };
        private Label Error_label { get; set; } = new Label() { ID = "Error_label", Text = "", ForeColor = System.Drawing.Color.Red };

        private Panel settings_user_name_wrapper { get; set; } = new Panel() { CssClass = "settings_user_name_wrapper" };
        private TextBox User_name { get; set; } = new TextBox() { CssClass = "settings_user_name" };

        private Panel settings_user_surname_wrapper { get; set; } = new Panel() { CssClass = "settings_user_surname_wrapper" };
        private TextBox User_surname { get; set; } = new TextBox() { CssClass = "settings_user_surname" };

        private Panel settings_user_birthday_wrapper { get; set; } = new Panel() { CssClass = "settings_user_birthday_wrapper" };
        private TextBox User_birthday { get; set; } = new TextBox() { CssClass = "settings_user_birthday", TextMode = TextBoxMode.Date };

        private Panel settings_user_gender_wrapper { get; set; } = new Panel() { CssClass = "settings_user_gender_wrapper" };
        private TextBox User_gender { get; set; } = new TextBox() { CssClass = "settings_user_gender", ReadOnly = true };

        private Panel settings_user_password_wrapper { get; set; } = new Panel() { CssClass = "password" };
        private TextBox User_password { get; set; } = new TextBox() { CssClass = "settings_user_password", TextMode = TextBoxMode.Password };

        private Panel settings_user_avatar_wrapper { get; set; } = new Panel() { CssClass = "settings_user_avatar_wrapper" };
        private AjaxControlToolkit.AsyncFileUpload User_avatar { get; set; } = new AjaxControlToolkit.AsyncFileUpload() { CssClass = "settings_user_avatar" };

        private Panel settings_user_height_wrapper { get; set; } = new Panel() { CssClass = "settings_user_height_wrapper" };
        private TextBox User_height { get; set; } = new TextBox() { CssClass = "settings_user_height" };

        private Panel settings_user_weight_wrapper { get; set; } = new Panel() { CssClass = "settings_user_weight_wrapper" };
        private TextBox User_weight { get; set; } = new TextBox() { CssClass = "settings_user_weight" };

        private Panel settings_user_delete { get; set; } = new Panel() { CssClass = "settings_user_delete" };
        Button User_delete = new Button() { ID = "User_delete", Text = "Удалить" };

        private Panel settings_user_about_wrapper { get; set; } = new Panel() { CssClass = "settings_user_about_wrapper" };
        private TextBox User_about { get; set; } = new TextBox() { ID = "settings_user_about", CssClass = "settings_user_about textBox", TextMode = TextBoxMode.MultiLine, Rows = 2, Columns = 10 };
        
        private UpdatePanel ContentPanel { get; set; } = new UpdatePanel();
        public Settings_part() {

        }

        public Settings_part(String name, String surname, DateTime birthday, Boolean gender, String password, String avatar, String height, String weight, String about)
        {
            User_name.Text = name;
            User_surname.Text = surname;
            User_birthday.Text = birthday.ToString("yyyy-MM-dd");
            if (gender)
            {
                User_gender.Text = "Женский";
            }
            else
            {
                User_gender.Text = "Мужской";
            }
            User_password.Text = password;
            User_height.Text = height;
            User_weight.Text = weight;
            User_about.Text = about;
            User_about.Attributes.Add("placeholder", "Ваш текст...");

        }

        public void Initialize(UpdatePanel Content)
        {
            Content.ContentTemplateContainer.Controls.Add(settings_wrapper);
            settings_wrapper.ContentTemplateContainer.Controls.Add(Messages_panel);
            Messages_panel.Controls.Add(Error_label);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_name_wrapper);
            settings_user_name_wrapper.Controls.Add(new Label() { Text = "Имя:" });
            settings_user_name_wrapper.Controls.Add(User_name);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_surname_wrapper);
            settings_user_surname_wrapper.Controls.Add(new Label() { Text = "Фамилия:" });
            settings_user_surname_wrapper.Controls.Add(User_surname);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_birthday_wrapper);
            settings_user_birthday_wrapper.Controls.Add(new Label() { Text = "Дата рождения:" });
            settings_user_birthday_wrapper.Controls.Add(User_birthday);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_gender_wrapper);
            settings_user_gender_wrapper.Controls.Add(new Label() { Text = "Пол:" });
            settings_user_gender_wrapper.Controls.Add(User_gender);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_password_wrapper);
            settings_user_password_wrapper.Controls.Add(new Label() { Text = "Пароль:" });
            settings_user_password_wrapper.Controls.Add(User_password);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_avatar_wrapper);
            settings_user_avatar_wrapper.Controls.Add(new Label() { Text = "Фото:" });
            settings_user_avatar_wrapper.Controls.Add(User_avatar);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_height_wrapper);
            settings_user_height_wrapper.Controls.Add(new Label() { Text = "Рост:" });
            settings_user_height_wrapper.Controls.Add(User_height);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_weight_wrapper);
            settings_user_weight_wrapper.Controls.Add(new Label() { Text = "Вес:" });
            settings_user_weight_wrapper.Controls.Add(User_weight);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_about_wrapper);
            settings_user_about_wrapper.Controls.Add(new Label() { Text = "О себе:" });
            settings_user_about_wrapper.Controls.Add(User_about);

            settings_wrapper.ContentTemplateContainer.Controls.Add(settings_user_delete);
            settings_user_delete.Controls.Add(new Label() { Text = "Удалить аккаунт:" });
            User_delete.Click += delete_user;
            settings_user_delete.Controls.Add(User_delete);

            Panel Buttons_wrapper = new Panel() { CssClass = "settings_buttons_wrapper" , ID = "settings_buttons_wrapper" };
            settings_wrapper.ContentTemplateContainer.Controls.Add(Buttons_wrapper);

            Button Apply_Changes = new Button() { ID = "Apply_Changes", Text = "Подтвердить" };
            Apply_Changes.Click += apply_changes;
            Buttons_wrapper.Controls.Add(Apply_Changes);

            Button Cancel_Changes = new Button() { ID = "Cancel_Changes", Text = "Отмена" };
            Cancel_Changes.Click += cancel_changes;
            Buttons_wrapper.Controls.Add(Cancel_Changes);

            settings_wrapper.ContentTemplateContainer.Controls.Add(new UpdateProgress() { ID = "UPDProgress", AssociatedUpdatePanelID = "settings_wrapper" });
            ContentPanel = Content;
        }

        private void apply_changes(object sender, EventArgs e) {
            if (check_valide())
            {
                try
                {
                    Messages_panel.Visible = true;
                    Error_label.ForeColor = System.Drawing.Color.Green;
                    Error_label.Text = "Обновление начато";
                    change_user(User_avatar);
                    Messages_panel.Visible = true;
                    Error_label.ForeColor = System.Drawing.Color.Green;
                    Error_label.Text = "Информация обновлена";
                }
                catch (Exception ex){
                    Messages_panel.Visible = true;
                    Error_label.ForeColor = System.Drawing.Color.Red;
                    Error_label.Text = "Информация не была обновлена!<br />" + ex.Message;
                }
                
            }
        }

        private void change_user(AjaxControlToolkit.AsyncFileUpload fu) {
            DateTime birthday = new DateTime(1899, 1, 1);
            DateTime.TryParse(User_birthday.Text, out birthday);

            int height = 0;
            int.TryParse(User_height.Text, out height);

            Double weight = 0;
            Double.TryParse(User_weight.Text, out weight);
            int Weight = 1;
            if (!weight.Equals(0.0))
            {
                Weight = add_weight(Page.User.Identity.Name, weight);
            }

//            Error_label.Text = fu.HasFile;
            String Photo = "~/img/default_user.png";
            if (fu.HasFile)
            {
                try
                {
                    upload_avatar(get_user_login(Page.User.Identity.Name), fu);
                    Photo = String.Format("~/UserAccounts/{0}/Images/{0}_avatar{1}", get_user_login(Page.User.Identity.Name), new FileInfo(fu.FileName).Extension);
                }
                catch (Exception ex)
                {
                    Error_label.Text = ex.Message;
                }
            }

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            OleDbCommand Command = new OleDbCommand();
            if (fu.HasFile)
            {
                Command = new OleDbCommand("UPDATE Users SET Name = ''+?+'', Surname = ''+?+'', Password = ''+?+'', Date_of_birth = ?, Weight = ?, Height = ?, " +
                    "Avatar = ''+?+'', About = ''+?+'' WHERE ID = ''+?+''", Connection);
            }
            else
            {
                Command = new OleDbCommand("UPDATE Users SET Name = ''+?+'', Surname = ''+?+'', Password = ''+?+'', Date_of_birth = ?, Weight = ?, Height = ?, " +
                    "About = ''+?+'' WHERE ID = ''+?+''", Connection);
            }
            Command.Parameters.Add("Name", OleDbType.VarWChar).Value = User_name.Text;
            Command.Parameters.Add("Surname", OleDbType.VarWChar).Value = User_surname.Text;
            Command.Parameters.Add("Password", OleDbType.VarWChar).Value = User_password.Text;
            Command.Parameters.Add("Date_of_birth", OleDbType.DBDate).Value = birthday.Date;
            if (!weight.Equals(0.0))
            {
                Command.Parameters.Add("Weight", OleDbType.Double).Value = Weight;
            }
            else
            {
                Command.Parameters.Add("Weight", OleDbType.Integer).Value = null;
            }
            Command.Parameters.Add("Height", OleDbType.Integer).Value = height;
            if (fu.HasFile)
            {
                Command.Parameters.Add("Avatar", OleDbType.VarWChar).Value = Photo;
            }
            else
            {

            }
            Command.Parameters.Add("About", OleDbType.VarWChar).Value = User_about.Text;
            Command.Parameters.Add("ID", OleDbType.BigInt).Value = Page.User.Identity.Name;
            
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        private int add_weight(String id, Double weight)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("INSERT INTO User_weight_chronology (Date, Weight, User_ID) VALUES (?, ?, ?)", Connection);
            Command.Parameters.Add("Date", OleDbType.DBDate).Value = DateTime.Now;
            Command.Parameters.Add("Weight", OleDbType.Double).Value = weight;
            Command.Parameters.Add("User_ID", OleDbType.BigInt).Value = Page.User.Identity.Name;

            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();

            Connection.Open();
            Command = new OleDbCommand("SELECT ID FROM User_weight_chronology WHERE (User_ID = ?)", Connection);
            Command.Parameters.Add("User_ID", OleDbType.BigInt).Value = Page.User.Identity.Name;
            DataTable DT = new DataTable();
            OleDbDataAdapter DA = new OleDbDataAdapter(Command);
            DA.Fill(DT);

            return Convert.ToInt32(DT.Rows[DT.Rows.Count - 1]["ID"]);
        }
        
        private String get_user_login(string ID)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("SELECT Login FROM Users WHERE ID = ''+?+''", Connection);
            Command.Parameters.Add("ID", OleDbType.VarWChar).Value = Page.User.Identity.Name;

            Connection.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();
            DA.Fill(DT);

            Connection.Close();
            return DT.Rows[0]["Login"].ToString();
        }

        private void upload_avatar(String user_login, AjaxControlToolkit.AsyncFileUpload fu)
        {
            String file_name_path = String.Empty;
            Directory.CreateDirectory(String.Format("{0}\\UserAccounts\\{1}\\Images", Request.PhysicalApplicationPath, user_login));

            file_name_path = String.Format("{0}\\UserAccounts\\{1}\\Images\\{1}_avatar{2}", Request.PhysicalApplicationPath, user_login, new FileInfo(fu.FileName).Extension);

            fu.SaveAs(file_name_path);
        }

        private void cancel_changes(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        private Boolean check_valide() {
            if (!User_name.Text.Equals(String.Empty))
            {
                if (!User_surname.Text.Equals(String.Empty))
                {
                        if (!User_password.Text.Equals(String.Empty))
                        {
                            return true;
                        }
                        else
                        {
                            Messages_panel.Visible = true;
                            Error_label.ForeColor = System.Drawing.Color.Red;
                            Error_label.Text = "Пароль не введён!<br />";
                            User_password.Text = String.Empty;
                            User_password.Focus();

                        return false;
                        }
                }
                else
                {
                    Messages_panel.Visible = true;
                    Error_label.ForeColor = System.Drawing.Color.Red;
                    Error_label.Text = "Пожалуйста, введите Фамилию!<br />";
                    User_surname.Focus();

                    return false;
                }
            }
            else
            {
                Messages_panel.Visible = true;
                Error_label.ForeColor = System.Drawing.Color.Red;
                Error_label.Text = "Пожалуйста, введите Имя!<br />";
                User_name.Focus();

                return false;
            }
        }

        private void delete_user(object sender, EventArgs e)
        {
            Messages_panel.Visible = true;

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("DELETE Users WHERE ID = ''+?+''", Connection);
            Command.Parameters.Add("ID", OleDbType.VarWChar).Value = Page.User.Identity.Name;

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                Error_label.Text = "Аккаунт успешно удалён!";
                FormsAuthentication.SignOut();
//                new Settings().redirect_to("~/News");
            }
            catch (Exception ex)
            {
                Error_label.Text = "Ошибка при удалении!<br />"/* + ex.Message*/;
            }
        }
    }
}