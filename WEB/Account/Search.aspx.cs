using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Account
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["words"] != null)
            {
                if (!Page.RouteData.Values["words"].Equals(search_box.Text) && search_box.Text.Equals("Что найти?"))
                {
                    search_box.Text = Page.RouteData.Values["words"].ToString();
                    load_search(Page.RouteData.Values["words"].ToString());
                }
                else
                {
                    load_search(search_box.Text);
                }
            }
            else {
                load_search(String.Empty);
            }
        }

        private void load_search(String searchString)
        {
            try
            {
                if (!searchString.Equals(String.Empty))
                {
                    this.Title = "Результаты поиска по " + searchString;
                    if (!search_box.Text.Equals(searchString)) {
                        search_box.Text = searchString;
                        Page.RouteData.Values["words"] = search_box.Text;
                    }

                    postsLoad(searchString);
                    usersLoad(searchString);
                }
            }
            catch (Exception ex) {
                resultsPanel.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, попробуйте презагрузить страницу<br />" });
            }

//            load_news(resultsPanel);

        }

        private void postsLoad(String searchString)
        {
            VirtualPathData vpd_full_post;
            VirtualPathData vpd_user;
            String commandstr;
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command;
            DataTable DT = new DataTable();
            OleDbDataAdapter Adapter = new OleDbDataAdapter();

            commandstr = "SELECT TOP 3 WEB_Pages_topics.ID, WEB_Pages_topics.Topic_pic, WEB_Pages.Name, WEB_Pages_topics.Topic_text, WEB_Pages.[Date], Users.Name AS Author, Users.Surname, " +
                "Users.Avatar, WEB_Pages.ID AS Web_Page_id, Users.ID AS Author_id FROM WEB_Pages_topics INNER JOIN WEB_Pages ON WEB_Pages_topics.WEB_Page_id = WEB_Pages.ID INNER JOIN " +
                "Users ON WEB_Pages.Auther_id = Users.ID WHERE(WEB_Pages_topics.Topic_text LIKE '%' + ? +'%') OR (WEB_Pages.Name LIKE '%' + ? +'%')";

            Command = new OleDbCommand(commandstr, Connection);
            if (!searchString.Equals(String.Empty))
            {
                Command.Parameters.Add("Topic_text", OleDbType.VarWChar).Value = searchString;
                Command.Parameters.Add("Name", OleDbType.VarWChar).Value = searchString;
            }
            Adapter.SelectCommand = Command;

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
                    new_post_prev.Initialize_in_Panel(items);
                    posts_prev_list.Add(new_post_prev);
                }
            }
            catch (Exception exeption)
            {
                resultsPanel.ContentTemplateContainer.Controls.Add(new Literal() { Text = "Не удалось загрузить страницу, попробуйте презагрузить страницу<br />" + exeption.Message });
            }
            if (DT.Rows.Count.Equals(0))
            {
                resultsPanel.ContentTemplateContainer.Controls.Add(new Label() { Text = "Поиск по публикациям не дал результатов" });
            }
        }

        private void usersLoad(String searchString)
        {

        }

        protected void search_box_TextChanged(object sender, EventArgs e)
        {
            items.Controls.Clear();

            load_search(search_box.Text);
        }

        protected void search_button_Click(object sender, EventArgs e)
        {
            items.Controls.Clear();

            load_search(search_box.Text);
        }
    }
}