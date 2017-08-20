using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class Post : System.Web.UI.Page
    {
        VirtualPathData vpd;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!get_id().Equals(String.Empty) && !get_id()[0].Equals('-'))
                {
                    load_posts();
                }
                else
                {
                    Panel User_posts = new Panel() { ID = "User_posts" };
                    Content.ContentTemplateContainer.Controls.Add(User_posts);
                    load_news(User_posts);
                }
            }
            catch
            {
                Response.Redirect(ResolveUrl("~/News"));
            }
        }

        private void load_posts()
        {
            String commandstr;
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command;
            DataTable DT = new DataTable();
            OleDbDataAdapter Adapter = new OleDbDataAdapter();

            commandstr = "SELECT WEB_Pages_topics.ID, Users.ID AS Author_id, Users.Name AS Auther_name, Users.Surname, Users.Avatar, WEB_Pages.Name, " +
                "WEB_Pages_topics.Topic_text, WEB_Pages_topics.Topic_pic, WEB_Pages.[Date], WEB_Pages.ID AS Web_Page_id FROM WEB_Pages_topics INNER JOIN " +
                "WEB_Pages ON WEB_Pages_topics.WEB_Page_id = WEB_Pages.ID INNER JOIN Users ON WEB_Pages.Auther_id = Users.ID WHERE (Web_Page_id = ?)";

            Command = new OleDbCommand(commandstr, Connection);
            if (!get_id()[0].Equals('-'))
            {
                Command.Parameters.Add("Web_Page_id", OleDbType.BigInt).Value = get_id();
            }
            else
            {
                Command.Parameters.Add("Web_Page_id", OleDbType.BigInt).Value = get_id().Trim('-');
            }
            Adapter.SelectCommand = Command;

            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                List<Full_post> posts_prev_list = new List<Full_post>();
                bool is_first = true;
                uint userID = new uint();
                uint authorID = new uint();

                foreach (DataRow row in DT.Rows)
                {
                    RouteValueDictionary parameters = new RouteValueDictionary{
                        { "id", Convert.ToInt32(row["Author_id"].ToString()) }
                    };
                    vpd = RouteTable.Routes.GetVirtualPath(null, "User", parameters);

                    Full_post new_post = new Full_post(
                        row["Auther_name"].ToString().TrimEnd() + ' ' + row["Surname"].ToString().TrimEnd(),
                        row["Name"].ToString().TrimEnd(),
                        '(' + DateTime.Parse(row["Date"].ToString()).ToShortDateString() + ' ' + DateTime.Parse(row["Date"].ToString()).ToShortTimeString() + ')',
                        row["Topic_text"].ToString(),
                        row["Avatar"].ToString(),
                        row["Topic_pic"].ToString(),
                        vpd,
                        row["Author_id"].ToString(),
                        is_first,
                        uint.Parse(row["Web_Page_id"].ToString())
                    );

                    new_post.Initialize(Content);
                    posts_prev_list.Add(new_post);

                    this.Page.Title = row["Name"].ToString().TrimEnd();
                    is_first = false;
                    uint.TryParse(row["Author_id"].ToString(), out authorID);
                }

                if (DT.Rows.Count.Equals(0))
                {
                    this.Page.Title = "У пользователя " + " пока нет публикаций" + " | " + this.Master.CompanyName;
                }
                /*                UpdatePanel comments = new UpdatePanel() { ID = "commentsWrapper", UpdateMode = UpdatePanelUpdateMode.Conditional };
                                Content.ContentTemplateContainer.Controls.Add(comments);*/
                uint.TryParse(User.Identity.Name, out userID);
                Comments commentsWrapper = new Comments(Content, authorID, uint.Parse(get_id()), userID);
//                loadComments(comments);
            }
            catch (Exception exeption)
            {
                Content.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, пожалуйста перезагрузите<br />" + exeption.Message });
            }
        }

        private String get_id()
        {
            String result = String.Empty;

            try
            {
                result = Page.RouteData.Values["id"].ToString();
            }
            catch
            {
                if (result.Equals(String.Empty))
                {
                    try
                    {
                        result = Page.RouteData.Values["post_id"].ToString();
                    }
                    catch
                    {
                        if (this.User.Identity.IsAuthenticated)
                        {
                            result = '-' + Page.User.Identity.Name;
                        }
                    }
                }
            }
            return result;
        }

        public void load_news(Panel posts_plase)
        {
            String commandstr = "SELECT WEB_Pages_topics.ID, Users.ID AS Author_id, Users.Name AS Auther_name, Users.Surname, Users.Avatar, WEB_Pages.Name, " +
                    "WEB_Pages_topics.Topic_text, WEB_Pages_topics.Topic_pic, WEB_Pages.[Date], WEB_Pages.ID AS Web_Page_id FROM WEB_Pages_topics INNER JOIN " +
                    "WEB_Pages ON WEB_Pages_topics.WEB_Page_id = WEB_Pages.ID INNER JOIN Users ON WEB_Pages.Auther_id = Users.ID WHERE (Users.ID = ?) ORDER BY WEB_Pages.[Date] DESC";
            try
            {
                OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                DataTable DT = new DataTable();
                Connection.Open();
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                if (!get_id()[0].Equals('-'))
                {
                    Command.Parameters.Add("Users.ID", OleDbType.BigInt).Value = get_id();
                }
                else
                {
                    Command.Parameters.Add("Users.ID", OleDbType.BigInt).Value = get_id().Trim('-');
                }
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                Adapter.Fill(DT);

                foreach (DataRow row in DT.Rows)
                {
                    RouteValueDictionary parameters_Full_post = new RouteValueDictionary{
                    { "post_id", Convert.ToInt32(row["Web_Page_id"].ToString()) }
                };
                    VirtualPathData vpd_full_post = RouteTable.Routes.GetVirtualPath(null, "Post", parameters_Full_post);

                    RouteValueDictionary parameters_User = new RouteValueDictionary{
                    { "id", Convert.ToInt32(row["Author_id"].ToString()) }
                };
                    VirtualPathData vpd_user = RouteTable.Routes.GetVirtualPath(null, "User", parameters_User);

                    Post_preview new_post_prev = new Post_preview(
                        row["Auther_name"].ToString().TrimEnd() + ' ' + row["Surname"].ToString().TrimEnd(),
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

                    this.Page.Title = "Публикации " + row["Auther_name"].ToString().TrimEnd() + ' ' + row["Surname"].ToString().TrimEnd();
                    new_post_prev.Initialize_in_Panel_without_author(posts_plase);
                }
            }
            catch (Exception exeption)
            {
                Content.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, пожалуйста перезагрузите<br />" });
            }
        }
    }
}