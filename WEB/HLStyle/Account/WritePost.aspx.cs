using System;
using AjaxControlToolkit;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class WritePost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Новая публикая | " + this.Master.CompanyName;
            if (this.Context.User.Identity.IsAuthenticated)
            {
                this.Page.Form.Attributes.Add("enctype", "multipart/form-data");
                load_new_post_field();
            }
            else
            {
                not_autorized();
            }
        }

        private void not_autorized()
        {
            Panel non_autorized = new Panel() { CssClass = "non_autrized_panel" };

            this.Content_panel.ContentTemplateContainer.Controls.Add(non_autorized);
            non_autorized.Controls.Add(new Label() { Text = "Пожалуйста,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Login", Text = "Войдите", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "или", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Registration", Text = "Зарегистрируйтесь,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "чтобы просмотреть эту страницу", CssClass = "non_autorized_msg_parts" });

        }

        void load_new_post_field() {
            NewPost new_post = new NewPost();
            new_post.Initialize(Content_new_post, this.Page, this.User.Identity.Name);
        }
    }

    public partial class NewPost : System.Web.UI.Page
    {
        private Label Status_label = new Label() { Visible = false, CssClass = "status_new_post" };

        private Panel Name_post_wrapper = new Panel() { ID= "Name_post_wrapper", CssClass= "post_part_wrapper" };
        private TextBox Name_post = new TextBox() { ID= "Name_post", CssClass = "Name_post", MaxLength = 100 };

        private Panel Post_text_wrapper = new Panel() { ID= "Post_text_wrapper", CssClass = "post_part_wrapper" };
        private TextBox Text_post = new TextBox() { ID= "Text_post", CssClass = "Text_post textBox", TextMode = TextBoxMode.MultiLine };

        private Panel Post_pic_wrapper = new Panel() { ID = "Post_pic_wrapper", CssClass = "post_part_wrapper" };
        private FileUpload Post_pic = new FileUpload() { ID="post_pic", CssClass="post_pic" };

        private Panel Buttons_wrapper = new Panel() { CssClass = "settings_buttons_wrapper", ID = "settings_buttons_wrapper" };
        private Button Create_post = new Button() { ID="Create_post", CssClass = "Create_post", Text="Опубликовать" };

        private Page page = new Page();
        private String user_id = String.Empty;

        public NewPost() {

        }

        public void Initialize(UpdatePanel Content, Page P, String ID) {
            Name_post.Attributes.Add("placeholder", "Название");
            Text_post.Attributes.Add("placeholder", "Ваш текст...");
            Content.Triggers.Add(new PostBackTrigger() { ControlID= "Create_post" });

            Content.ContentTemplateContainer.Controls.Add(Status_label);
            Status_label.Attributes.Add("style", "width:100%; text-align:left");

            Content.ContentTemplateContainer.Controls.Add(Name_post_wrapper);
            Name_post_wrapper.Controls.Add(new Label() { Text = "Название публикации:" });
            Name_post_wrapper.Controls.Add(Name_post);

            Content.ContentTemplateContainer.Controls.Add(Post_pic_wrapper);
            Post_pic_wrapper.Controls.Add(new Label() { Text = "Картинка к публикации:" });
            Post_pic_wrapper.Controls.Add(Post_pic);

            Text_post.Attributes.Add("OnClick", "text_field_clicked()");
            Text_post.Attributes.Add("onfocusin", "text_field_clicked()");
            Text_post.Attributes.Add("onfocusout", "text_field_clicked()");
            Content.ContentTemplateContainer.Controls.Add(Post_text_wrapper);
            Post_text_wrapper.Controls.Add(Text_post);

            Create_post.Click += Create_post_Click;
            Content.ContentTemplateContainer.Controls.Add(Buttons_wrapper);
//            Buttons_wrapper.Controls.Add(Status_label);
            Buttons_wrapper.Controls.Add(Create_post);

            page = P;
            user_id = ID;
        }

        private void Create_post_Click(object sender, EventArgs e)
        {
            Status_label.Visible = true;
            Status_label.ForeColor = System.Drawing.Color.Green;
            Status_label.BorderStyle = BorderStyle.None;

            if (!Name_post.Text.Equals(String.Empty))
            {
                if (!Text_post.Text.Equals("Ваш текст..."))
                {
                    try
                    {
                        create_page(this.Name_post.Text);

                        Status_label.Text = "Результат: Опубликовано!<br />";
                    }
                    catch (Exception ex)
                    {
                        Status_label.ForeColor = System.Drawing.Color.Red;
                        Status_label.Text = String.Format("Ошибка: {0}", ex.Message);
                    }
                }
                else
                {
                    Status_label.ForeColor = System.Drawing.Color.Red;
                    Status_label.Text = "Пожалуйста, введите текст публикации.";
                }
            }
            else
            {
                Status_label.ForeColor = System.Drawing.Color.Red;
                Status_label.Text = "Пожалуйста, введите название.";
            }
        }

        private void create_page(String name) {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand();

            Command = new OleDbCommand("INSERT INTO \"dbo\".\"WEB_Pages\" (\"Name\", \"Auther_id\", \"Date\") VALUES (''+?+'', ?, ?)", Connection);

            Command.Parameters.Add("Name", OleDbType.VarWChar).Value = name;
            Command.Parameters.Add("Auther_id", OleDbType.Integer).Value = user_id;
            Command.Parameters.Add("Date", OleDbType.DBTimeStamp).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            if (check_name(name))
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                create_post(get_web_page_id(name), Post_pic, Text_post.Text);
            }
            else
            {
                if (check_page_owner(name, user_id))
                {
                    create_post(get_web_page_id(name), Post_pic, Text_post.Text);
                    Status_label.ForeColor = System.Drawing.Color.Green;
                    Status_label.Text = "Страница с таким названием уже существует,<br />Публикация будет добавлена в существующую страницу";
                }
                else {
                    throw new Exception("Страница с таким названием уже существует.");
                }
            }
        }

        private void create_post(Int64 page_id, FileUpload FU, String text)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand();
            String Photo = "~/img/default_topic.jpg";
            StringBuilder sb = new StringBuilder(
                             HttpUtility.HtmlEncode(text));
            sb.Replace("&lt;b&gt;", "<b>");
            sb.Replace("&lt;/b&gt;", "</b>");
            sb.Replace("&lt;i&gt;", "<i>");
            sb.Replace("&lt;/i&gt;", "</i>");
            sb.Replace("\n", "<br />");
            sb.ToString().TrimEnd();

            if (FU.HasFile)
            {
                Photo = upload_pic(Page.User.Identity.Name, FU);
            }

//            Status_label.Text += Photo + "<br />";

            Command = new OleDbCommand("INSERT INTO \"dbo\".\"WEB_Pages_topics\" (\"Topic_pic\", \"Topic_text\", \"WEB_Page_id\") VALUES (''+?+'', ''+?+'', ?)", Connection);

            Command.Parameters.Add("Topic_pic", OleDbType.VarWChar).Value = Photo;
            Command.Parameters.Add("Topic_text", OleDbType.VarWChar).Value = sb.ToString();
            Command.Parameters.Add("WEB_Page_id", OleDbType.Integer).Value = page_id;

            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        private Int64 get_web_page_id(String name)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("SELECT ID FROM \"dbo\".\"WEB_Pages\" WHERE (\"Name\" = ''+?+'') AND (\"Auther_id\" = ?) AND (\"Date\" + 1 > ?)", Connection);
            DataTable DT = new DataTable();
            OleDbDataAdapter DA = new OleDbDataAdapter(Command);

            Command.Parameters.Add("Name", OleDbType.VarWChar).Value = name;
            Command.Parameters.Add("Auther_id", OleDbType.Integer).Value = user_id;
            Command.Parameters.Add("Date", OleDbType.DBTimeStamp).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            Connection.Open();
            DA.Fill(DT);
            Connection.Close();

            
            return Int64.Parse(DT.Rows[0]["ID"].ToString());
        }

        private bool check_name(String name)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("SELECT Name FROM WEB_Pages WHERE Name = ''+?+''", Connection);
            DataTable DT = new DataTable();
            OleDbDataAdapter DA = new OleDbDataAdapter(Command);

            Command.Parameters.Add("Name", OleDbType.VarWChar).Value = name;

            Connection.Open();
            DA.Fill(DT);
            Connection.Close();
            if (DT.Rows.Count > 0)
                return false;
            else
                return true;
        }

        private bool check_page_owner(String name, String user_id) {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("SELECT Name, Auther_id FROM WEB_Pages WHERE (Name = ''+?+'') AND (Auther_id = ?)", Connection);
            DataTable DT = new DataTable();
            OleDbDataAdapter DA = new OleDbDataAdapter(Command);

            Command.Parameters.Add("Name", OleDbType.VarWChar).Value = name;
            Command.Parameters.Add("Auther_id", OleDbType.Integer).Value = user_id;

            Connection.Open();
            DA.Fill(DT);
            Connection.Close();
            if (DT.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private String upload_pic(String user_id, FileUpload fu)
        {
            String file_name_local_path = String.Empty;
            String file_name_full_path = String.Empty;
            /*
                        Status_label.Text += String.Format("{0}\\UserAccounts\\{1}\\Images\\Posts", page.Request.PhysicalApplicationPath, get_user_login()) + "<br />";
                        Status_label.Text += String.Format("{0}\\UserAccounts\\{1}\\Images\\Posts\\{2}{3}", page.Request.PhysicalApplicationPath, get_user_login(),
                            file_name(10), new FileInfo(fu.FileName).Extension) + "<br />";
            */

            if (new FileInfo(fu.FileName).Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || 
                new FileInfo(fu.FileName).Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                new FileInfo(fu.FileName).Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {

                Directory.CreateDirectory(String.Format("{0}\\UserAccounts\\{1}\\Images\\Posts", page.Request.PhysicalApplicationPath, get_user_login()));

                file_name_local_path = String.Format("~\\UserAccounts\\{0}\\Images\\Posts\\{1}{2}", get_user_login().TrimEnd(), file_name(30), new FileInfo(fu.FileName).Extension);

                file_name_full_path = String.Format("{0}{1}", page.Request.PhysicalApplicationPath, file_name_local_path.Remove(0, 1));

                //            Status_label.Text += file_name_full_path + "<br />";

                fu.SaveAs(file_name_full_path);
            }
            else
            {
                throw new Exception("Формат картинки не поддерживается.");
            }

            return file_name_local_path;
        }

        private String get_user_login()
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("SELECT Login FROM Users WHERE ID = ?", Connection);
            Command.Parameters.Add("ID", OleDbType.VarWChar).Value = user_id;

            Connection.Open();
            OleDbDataAdapter DA = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();
            DA.Fill(DT);

            Connection.Close();
            return DT.Rows[0]["Login"].ToString();
        }

        private String file_name(int n)
        {
            Random rand = new Random();
            String alphabet = "";
            char ch = (char)65;
            while (ch >= (char)(65) && ch <= (char)90)
            {
                alphabet += ch;
                ch = (char)((int)ch + 1);
            }
            ch = (char)97;
            while (ch >= (char)(97) && ch <= (char)122)
            {
                alphabet += ch;
                ch = (char)((int)ch + 1);
            }
            ch = (char)48;
            while (ch >= (char)(48) && ch <= (char)57)
            {
                alphabet += ch;
                ch = (char)((int)ch + 1);
            }
            String result_str = String.Empty;
            for (int i = 0; i < n; i++)
            {
                result_str += alphabet.ElementAt(rand.Next(0, alphabet.Length - 1));
            }
            return result_str;
        }
    }
}