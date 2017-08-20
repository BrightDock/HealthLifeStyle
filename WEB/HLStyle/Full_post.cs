using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public class Full_post
    {
        private Panel post { get; set; } = new Panel() { CssClass = "post" };
        private Panel post_head { get; set; } = new Panel() { CssClass = "post_head" };
        private Panel post_author_image_wrapper { get; set; } = new Panel() { CssClass = "post_author_image_wrapper" };
        private Panel name_date_wrapper { get; set; } = new Panel() { CssClass = "name_date_wrapper" };
        private HyperLink link { get; set; } = new HyperLink() { CssClass = "post_author_link" };
        private Panel post_auther { get; set; } = new Panel() { CssClass = "post_author" };
        private Panel post_date { get; set; } = new Panel() { CssClass = "post_date" };
        private Panel post_views { get; set; } = new Panel() { CssClass = "post_views" };
        private Panel post_name { get; set; } = new Panel() { CssClass = "post_name" };
        private Panel post_text { get; set; } = new Panel() { CssClass = "post_text" };
        private Panel post_img_wrapper { get; set; } = new Panel() { CssClass = "post_img_wrapper" }; // sticky
        private System.Web.UI.WebControls.Image post_auther_image { get; set; } = new System.Web.UI.WebControls.Image() { CssClass = "post_auther_image" };
        private System.Web.UI.WebControls.Image post_image { get; set; } = new System.Web.UI.WebControls.Image() { CssClass = "post_image" };
        private int str_len { get; set; } = 300;
        VirtualPathData vpd_user;
        bool is_first = true;

        public Full_post()
        {

        }

        public Full_post(String Post_author, String Post_name, String Post_date, String Post_text, String Post_author_image, String Post_image, VirtualPathData VPD, String Post_author_id, bool Is_first, uint Post_id)
        {
            vpd_user = VPD;
            this.is_first = Is_first;
            link.Controls.Add(new Literal() { Text = Post_author });
            link.ToolTip = Post_author + " (id" + Post_author_id + ')';
            post_name.Controls.Add(new Literal() { Text = "<h1>" + Post_name + "</h1>" });
            post_name.ToolTip = Post_name;
            post_date.Controls.Add(new Literal() { Text = Post_date });
            post_views.Controls.Add(new Literal() { Text = watch_iteration(Post_id) });
            post_text.Controls.Add(new Literal() { Text = Post_text });
            if (!Post_author_image.Equals(String.Empty))
                post_auther_image.ImageUrl = Post_author_image;
            else
                post_auther_image.ImageUrl = "~/img/default_user.png";
            if (!Post_image.Equals(String.Empty))
                post_image.ImageUrl = Post_image;
            else
                post_image.ImageUrl = "~/img/default_topic.jpg";
            link.NavigateUrl = vpd_user.VirtualPath;
        }

        public String change_text_length(String str, int words)
        {
            str = str.Substring(0, str.Substring(0, str_len).LastIndexOf(' ')) + "...";
            return str;
        }

        public void Initialize(UpdatePanel Content)
        {
            Content.ContentTemplateContainer.Controls.Add(post);
            if (this.is_first)
            {
                post.Controls.Add(post_head);
                post_head.Controls.Add(post_name);
                post_head.Controls.Add(post_author_image_wrapper);
                post_author_image_wrapper.Controls.Add(post_auther_image);
                post_head.Controls.Add(name_date_wrapper);
                name_date_wrapper.Controls.Add(post_auther);
                name_date_wrapper.Controls.Add(post_views);
                post_auther.Controls.Add(link);
                name_date_wrapper.Controls.Add(post_date);
            }
            post.Controls.Add(post_img_wrapper);
            post_img_wrapper.Controls.Add(post_image);
            post.Controls.Add(post_text);
        }

        private String watch_iteration(uint post_id)
        {
            String result = "Просмотры: ";
            uint watch_count = 0;

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String command = "SELECT ID, Count_watch FROM WEB_Pages WHERE (ID = ?)";

            try
            {
                OleDbCommand Command = new OleDbCommand(command, Connection);
                Command.Parameters.Add("ID", OleDbType.BigInt).Value = post_id.ToString();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                if (DT.Rows.Count > 0)
                {
                    uint.TryParse(DT.Rows[0]["Count_watch"].ToString(), out watch_count);
                    result += (watch_count += 1);
                    if (this.is_first)
                        iterate_watch(watch_count, post_id);
                }

            }
            catch (Exception ex)
            {
                result = string.Format("Не удалось получить {0} {1} {2}"/*, ex.Message, watch_count, post_id*/);
            }

            return result;
        }

        private void iterate_watch(uint watch_count, uint post_id)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OleDbCommand Command = new OleDbCommand("UPDATE dbo.WEB_Pages SET Count_watch = ? WHERE ID = ?", Connection);
            Command.Parameters.Add("Count_watch", OleDbType.BigInt).Value = watch_count;
            Command.Parameters.Add("ID", OleDbType.BigInt).Value = post_id;

            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}