using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Routing;

namespace WEB
{
    public partial class News : System.Web.UI.Page
    {
        VirtualPathData vpd_full_post;
        VirtualPathData vpd_user;
        protected void Page_Load(object sender, EventArgs e)
        {
            load_news();
        }

        private void load_news()
        {
            String commandstr = "SELECT WEB_Pages_topics.ID, WEB_Pages_topics.Topic_pic, WEB_Pages.Name, WEB_Pages_topics.Topic_text, WEB_Pages.[Date], Users.Name AS " +
                "Author, Users.Surname, Users.Avatar, WEB_Pages.ID AS Web_Page_id, Users.ID AS Author_id FROM WEB_Pages_topics INNER JOIN " +
                "WEB_Pages ON WEB_Pages_topics.WEB_Page_id = WEB_Pages.ID INNER JOIN Users ON WEB_Pages.Auther_id = Users.ID " +
                "ORDER BY WEB_Pages.[Date] DESC";
            try
            {
                OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                DataTable DT = new DataTable();
                Connection.Open();
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
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

                    new_post_prev.Initialize(Content);
                    posts_prev_list.Add(new_post_prev);
                }
            }
            catch (Exception exeption)
            {
                Content.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, пожалуйста перезагрузите<br />"/* + exeption.Message */});
            }
        }
    }
}