using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class User : System.Web.UI.Page
    {
        private VirtualPathData vpd { get; set; }
        private VirtualPathData vpd_user { get; set; }
        private VirtualPathData vpd_full_post { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                load_user();
            }
            catch {

                VirtualPathData search_vpd;
                RouteValueDictionary parameters_Search = new RouteValueDictionary{
                        { "words", "" }
                    };
                search_vpd = RouteTable.Routes.GetVirtualPath(null, "Search", parameters_Search);
                HttpContext.Current.Response.Redirect(search_vpd.VirtualPath);
            }
        }

        private void load_user()
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "SELECT Users.ID, Users.Name, Users.Surname, Users.Sex, Users.Date_of_birth, Users.Height, " + 
                "Users.Avatar, Users.About, Users.Latest_online FROM Users WHERE(Users.ID = ?)";
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            Command.Parameters.Add("User.ID", OleDbType.BigInt).Value = get_id();
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();
            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();
                Panel posts_plase = new Panel();

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow row in DT.Rows)
                    {
                        this.Page.Title = row["Name"].ToString().TrimEnd() + ' ' + row["Surname"].ToString().TrimEnd();
                        RouteValueDictionary parameters = new RouteValueDictionary{
                            { "id", Convert.ToInt32(row["ID"].ToString().TrimEnd()) }
                        };
                        vpd = RouteTable.Routes.GetVirtualPath(null, "User_Posts", parameters);

                        String width_and_date = find_weigth_date(row["ID"].ToString().TrimEnd());
                        String Weigth = String.Empty;
                        DateTime Date = new DateTime(1899, 1, 1);

                        if (!width_and_date.Equals(String.Empty))
                        {
                            Weigth = width_and_date.Split(' ')[0];
                            Date = Convert.ToDateTime(width_and_date.Split(' ')[1]).Date;
                        }

                        User_Profile new_profile = new User_Profile(
                            row["Name"].ToString().TrimEnd() + ' ' + row["Surname"].ToString().TrimEnd(),
                            Convert.ToDateTime(row["Date_of_birth"]),
                            Convert.ToBoolean(row["Sex"]),
                            Weigth,
                            Date,
                            row["Height"].ToString().TrimEnd(),
                            row["About"].ToString().TrimEnd(),
                            row["Avatar"].ToString().TrimEnd(),
                            vpd,
                            row["Latest_online"].ToString()
                        );

                        new_profile.Initialize(Content);
                        posts_plase = new_profile.user_posts_wrapper;
                        load_news(posts_plase);
                    }
                }
                else {
                    User_Profile new_profile = new User_Profile(
                        "DELETE",
                        new DateTime(),
                        false,
                        "",
                        new DateTime(),
                        "",
                        "",
                        "",
                        new VirtualPathData(null, null),
                        ""
                    );
                    this.Page.Title = "DELETE";
                    posts_plase = new_profile.user_posts_wrapper;
                    posts_plase.Controls.Add(new Label() { Text = "Пользователь не существует либо страница была удалена" });
                    new_profile.Initialize(Content);

                    //                    HttpContext.Current.Response.Redirect("/404");
                }
            }
            catch (Exception exception)
            {
                if (exception.Source != "0")
                {
                    Content.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, пожалуйста перезагрузите<br />"/* + exeption.Message + "<br />" */});
                }
            }
        }

        private String find_weigth_date(String id)
        {
            String result = String.Empty;

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "SELECT Weight, Date FROM User_weight_chronology WHERE(User_ID = ?)";
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            Command.Parameters.Add("User_ID", OleDbType.BigInt).Value = id;
            DataTable DT = new DataTable();
            try
            {
                Connection.Open();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                Adapter.Fill(DT);
                Connection.Close();

                foreach (DataRow row in DT.Rows)
                {
                    result = String.Format("{0} {1}", row["Weight"].ToString(), row["Date"].ToString()) ;
                }
            }
            catch
            {

            }

                return result;
        }

        private String get_id()
        {
            String result = String.Empty;
            try
            {
                result = Page.RouteData.Values["id"].ToString();
            }
            catch { }

            return result;
        }

        public void load_news(Panel posts_plase)
        {
            String commandstr = "SELECT WEB_Pages_topics.ID, WEB_Pages_topics.Topic_pic, WEB_Pages.Name, WEB_Pages_topics.Topic_text, WEB_Pages.[Date], Users.Name AS Author, " +
                "Users.Surname, Users.Avatar, WEB_Pages.ID AS Web_Page_id, Users.ID AS Author_id FROM WEB_Pages_topics INNER JOIN WEB_Pages ON WEB_Pages_topics.WEB_Page_id = " +
                "WEB_Pages.ID INNER JOIN Users ON WEB_Pages.Auther_id = Users.ID WHERE (Users.ID = ?) ORDER BY WEB_Pages.[Date] DESC";
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            Command.Parameters.Add("User.ID", OleDbType.BigInt).Value = get_id();
            DataTable DT = new DataTable();
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);

            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();
                List<Post_preview> posts_prev_list = new List<Post_preview>();

                foreach (DataRow row in DT.Rows)
                {
                    RouteValueDictionary parameters_Full_post = new RouteValueDictionary{
                    { "post_id", Convert.ToInt32(row["Web_Page_id"].ToString()) }
                };
                    vpd_full_post = RouteTable.Routes.GetVirtualPath(null, "Post", parameters_Full_post);

                    RouteValueDictionary parameters_User = new RouteValueDictionary{
                    { "id", Convert.ToInt32(row["Author_id"].ToString()) }
                };
                    vpd_user = RouteTable.Routes.GetVirtualPath(null, "User", parameters_User);

                    Post_preview new_post_prev = new Post_preview(
                        row["Author"].ToString().TrimEnd() + ' ' + row["Surname"].ToString().TrimEnd(),
                        row["Name"].ToString().TrimEnd(),
                        '(' + DateTime.Parse(row["Date"].ToString()).ToShortDateString() + ' ' + DateTime.Parse(row["Date"].ToString()).ToShortTimeString() + ')',
                        row["Topic_text"].ToString(),
                        row["Avatar"].ToString(),
                        row["Topic_pic"].ToString(),
                        vpd_full_post,
                        vpd_user,
                        row["Author_id"].ToString(),
                        uint.Parse(row["Web_Page_id"].ToString())
                    );

                    new_post_prev.Initialize_in_Panel_without_author(posts_plase);
                    posts_prev_list.Add(new_post_prev);
                }
                if (DT.Rows.Count.Equals(0))
                {
                    posts_plase.Controls.Add(new Label() { Text = "У пользователя пока нет публикаций" });
                }
            }
            catch (Exception exeption)
            {
                Content.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, пожалуйста перезагрузите<br />" + exeption.Message });
            }
        }
    }

    public class User_Profile
    {
        private Panel user_wrapper { get; set; } = new Panel() { CssClass = "user_wrapper" };
        private Panel user_info_head { get; set; } = new Panel() { CssClass = "user_info_head" };
        private Panel user_info_name_surname_wrapper { get; set; } = new Panel() { CssClass = "user_info_name_surname_wrapper" };
        private Panel user_info_name_surname { get; set; } = new Panel() { CssClass = "user_info_name_surname" };
        private Panel user_info_avatar_about_wrapper { get; set; } = new Panel() { CssClass = "user_info_avatar_about_wrapper" };
        private Panel user_info_avatar_wrapper { get; set; } = new Panel() { CssClass = "user_info_avatar_wrapper"};
        private Panel user_info_data_wrapper { get; set; } = new Panel() { CssClass = "user_info_data_wrapper" };
        private Panel user_info_birth_date { get; set; } = new Panel() { CssClass = "user_info_birth_date", ID = "user_info_item" };
        private Panel user_info_age { get; set; } = new Panel() { CssClass = "user_info_age" };
        private Panel user_info_sex { get; set; } = new Panel() { CssClass = "user_info_sex", ID = "user_info_item" };
        private Panel user_info_weight_date { get; set; } = new Panel() { CssClass = "user_info_weight_date" };
        private Panel user_info_weight { get; set; } = new Panel() { CssClass = "user_info_weight", ID = "user_info_item" };
        private Panel user_info_height { get; set; } = new Panel() { CssClass = "user_info_height", ID = "user_info_item" };
        private Panel user_about { get; set; } = new Panel() { CssClass = "user_about" };
        private System.Web.UI.WebControls.Image user_avatar { get; set; } = new System.Web.UI.WebControls.Image() { CssClass = "user_avatar" };
        public Panel user_posts_wrapper { get; private set; } = new Panel() { CssClass = "user_posts_wrapper" };
        private HyperLink user_posts_link { get; set; } = new HyperLink() { CssClass = "user_posts_link" };
        private String Latest_online { get; set; } = String.Empty;
        private VirtualPathData vpd_user { get; set; }

        public User_Profile()
        {

        }

        public User_Profile(String User_info_name_surname, DateTime User_info_birth_date, bool User_info_sex, String User_info_weight, DateTime User_info_weight_date, String User_info_height, String User_about, String User_avatar, VirtualPathData VPD, String latest_online)
        {
            vpd_user = VPD;

            Latest_online = online_message(latest_online, User_info_sex);
            user_info_name_surname.Controls.Add(new Literal() { Text = "<h1>" + User_info_name_surname + Latest_online + "</h1>" });
            user_info_birth_date.Controls.Add(new Label() { Text = "Дата рождения" });
            user_info_birth_date.Controls.Add(new Literal() { Text = User_info_birth_date.ToShortDateString() });
            user_info_age.Controls.Add(new Literal() { Text = year_word(User_info_birth_date) });
            user_info_sex.Controls.Add(new Label() { Text = "Пол" });
            if (User_info_sex)
            {
                user_info_sex.Controls.Add(new Literal() { Text = "женский<small>" });
                user_info_sex.Controls.Add(new Label() { ID = "gender-girl" });
                user_info_sex.Controls.Add(new Literal() { Text = "</small>" });
            }
            else
            {
                user_info_sex.Controls.Add(new Literal() { Text = "мужской<small>" });
                user_info_sex.Controls.Add(new Label() { ID = "gender-man" });
                user_info_sex.Controls.Add(new Literal() { Text = "</small>" });
            }
            user_info_weight.Controls.Add(new Label() { Text = "Вес" });
            if (!User_info_weight.Equals(String.Empty) && !User_info_weight.Equals(null))
            {
                user_info_weight.Controls.Add(new Literal() { Text = User_info_weight + " кг" });
                user_info_weight_date.Controls.Add(new Literal() { Text = User_info_weight_date.ToShortDateString() });
            }
            else
                user_info_weight.Controls.Add(new Literal() { Text = "0 кг" });
            user_info_height.Controls.Add(new Label() { Text = "Рост" });
            if (!User_info_height.Equals(String.Empty))
                user_info_height.Controls.Add(new Literal() { Text = User_info_height + " см" });
            else
                user_info_height.Controls.Add(new Literal() { Text = "0 см" });
            user_about.Controls.Add(new Literal() { Text = "<h2>О себе</h2>" });
            if (!User_about.Equals(String.Empty))
                user_about.Controls.Add(new Literal() { Text = User_about });
            else
                user_about.Visible = false;
            if (!User_avatar.Equals(String.Empty))
                user_avatar.ImageUrl = User_avatar;
            else
                user_avatar.ImageUrl = "~/img/default_user.png";
            user_posts_link.NavigateUrl = vpd_user.VirtualPath;
        }

        public void Initialize(UpdatePanel Content)
        {
            Content.ContentTemplateContainer.Controls.Add(user_wrapper);
            user_wrapper.Controls.Add(user_info_head);
            user_info_head.Controls.Add(user_info_name_surname_wrapper);
            user_info_name_surname_wrapper.Controls.Add(user_info_name_surname);
            user_info_head.Controls.Add(user_info_avatar_about_wrapper);
            user_info_avatar_about_wrapper.Controls.Add(user_info_avatar_wrapper);
            user_info_avatar_wrapper.Controls.Add(user_avatar);
            user_info_avatar_about_wrapper.Controls.Add(user_about);
            user_wrapper.Controls.Add(user_info_data_wrapper);
            user_info_data_wrapper.Controls.Add(user_info_birth_date);
            user_info_birth_date.Controls.Add(user_info_age);
            user_info_data_wrapper.Controls.Add(user_info_sex);
            user_info_data_wrapper.Controls.Add(user_info_weight);
            user_info_weight.Controls.Add(user_info_weight_date);
            user_info_data_wrapper.Controls.Add(user_info_height);
            user_wrapper.Controls.Add(user_posts_wrapper);
            try
            {
                user_posts_wrapper.Controls.Add(new Literal() { Text = String.Format("<h2>Публикации{0}</h2>", publ_count(vpd_user.VirtualPath.Split('/')[3])) });
            }
            catch {
                throw new Exception() { Source = "0" };
            }
        }

        private String publ_count(String user_id)
        {
            String result = "<small>не удалось получить данные</small>";
            String commandstr = "SELECT WEB_Pages_topics.ID, WEB_Pages_topics.WEB_Page_id, WEB_Pages.ID AS ID_Page, WEB_Pages.Auther_id FROM " +
                "WEB_Pages_topics INNER JOIN WEB_Pages ON WEB_Pages_topics.WEB_Page_id = WEB_Pages.ID WHERE(WEB_Pages.Auther_id = ?)";
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            DataTable DT = new DataTable();
            OleDbCommand Command = new OleDbCommand(commandstr, Connection);
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            Command.Parameters.Add("WEB_Pages.Auther_id", OleDbType.BigInt).Value = user_id;

            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                result = DT.Rows.Count.ToString();
                return String.Format("<small>{0}</small>", result);         
            }
            catch
            {
                return result;
            }
        }

        private int calc_age(DateTime dateBirthDay) {
            int year = DateTime.Now.Year - dateBirthDay.Year;
            if (DateTime.Now.Month < dateBirthDay.Month ||
                (DateTime.Now.Month == dateBirthDay.Month && DateTime.Now.Day < dateBirthDay.Day)) year--;

            return year;
        }

        private String online_message(String latest_online, Boolean User_info_sex) {
            String result = String.Empty;

            DateTime temp_lat_onl;

            DateTime.TryParse(latest_online, out temp_lat_onl);
            if (!latest_online.Equals(String.Empty) && (DateTime.Now < temp_lat_onl))
            {
                result = "<small>онлайн</small>";
            }
            else
            {
                if (!User_info_sex)
                {
                    result = "<small>был в сети ";
                    if (!latest_online.Equals(String.Empty))
                    {
                        if (DateTime.Now > temp_lat_onl.AddDays(1))
                        {
                            result += temp_lat_onl.ToShortDateString() + " в ";
                        }
                        else if (DateTime.Now.Day > temp_lat_onl.Day)
                        {
                            result += "вчера в ";
                        }
                        else
                        {
                            result += "в ";
                        }
                    }
                    else {
                        result += String.Format("{0} в {1}", new DateTime(1900, 1, 1, 0, 0, 0).ToShortDateString(), new DateTime(1900, 1, 1, 0, 0, 0).ToShortTimeString());
                    }
                    result += temp_lat_onl.ToShortTimeString() + "</small >";
                }
                else
                {
                    result = "<small>была в сети ";
                    if (!latest_online.Equals(String.Empty))
                    {
                        if (DateTime.Now > temp_lat_onl.AddDays(1))
                        {
                            result += temp_lat_onl.ToShortDateString() + " в ";
                        }
                        else if (DateTime.Now.Day > temp_lat_onl.Day)
                        {
                            result += "вчера в ";
                        }
                        else
                        {
                            result += "в ";
                        }
                    }
                    else
                    {
                        result += String.Format("{0} в {1}", new DateTime(1900, 1, 1, 0, 0, 0).ToShortDateString(), new DateTime(1900, 1, 1, 0, 0, 0).ToShortTimeString());
                    }
                    result += temp_lat_onl.ToShortTimeString() + "</small >";
                }
            }

            return result;
        }



        public String year_word(DateTime dateBirthDay)
        {
            String[] words_arr = new String[] { "год", "года", "лет" };

            int age = calc_age(dateBirthDay);
            int years = age;

            years = years % 100;
            if (years > 19)
            {
                years = years % 10;
            }
            switch (years)
            {
                case 1:
                    {
                        return (age.ToString() + ' ' + words_arr[0]);
                    }
                case 2:
                case 3:
                case 4:
                    {
                        return (age.ToString() + ' ' + words_arr[1]);
                    }
                default:
                    {
                        return (age.ToString() + ' ' + words_arr[2]);
                    }
            }
        }

    }
}