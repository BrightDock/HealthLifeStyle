using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public class Post_preview
    {
        public Panel post_prev { get; private set; } = new Panel() { CssClass = "post_prev" };
        private Panel post_head_prev { get; set; } = new Panel() { CssClass = "post_head_prev" };
        private Panel post_author_image_wrapper_prev { get; set; } = new Panel() { CssClass = "post_author_image_wrapper_prev" };
        private Panel name_date_wrapper_prev { get; set; } = new Panel() { CssClass = "name_date_wrapper_prev" };
        private HyperLink author_link { get; set; } = new HyperLink() { CssClass = "post_author_link" };
        private Panel post_author_prev { get; set; } = new Panel() { CssClass = "post_author_prev" };
        private Panel post_date_prev { get; set; } = new Panel() { CssClass = "post_date_prev" };
        private Panel post_views_prev_wrapper { get; set; } = new Panel() { CssClass = "post_views_prev_wrapper" };
        private Literal post_views_prev { get; set; } = new Literal() { Text = "<i class='fa fa-eye' aria-hidden='true'></i>" };
        private HyperLink post_name_prev { get; set; } = new HyperLink() { CssClass = "post_name_prev" };
        private Panel post_text_prev { get; set; } = new Panel() { CssClass = "post_text_prev" };
        private Panel post_img_wrapper_prev { get; set; } = new Panel() { CssClass = "post_img_wrapper_prev" };
        private System.Web.UI.WebControls.Image post_author_image_prev { get; set; } = new System.Web.UI.WebControls.Image() { CssClass = "post_author_image_prev" };
        private System.Web.UI.WebControls.Image post_image_prev { get; set; } = new System.Web.UI.WebControls.Image() { CssClass = "post_image_prev" };
        private int str_len_prev { get; set; } = 300;
        private String text_prev = String.Empty;
        public HyperLink link { get; set; } = new HyperLink() { CssClass = "post_origin_link" };
        VirtualPathData vpd_full_post;
        VirtualPathData vpd_user;

        public Post_preview()
        {

        }

        public Post_preview(String Post_author_prev, String Post_name_prev, String Post_date_prev, String Post_text_prev, String Post_author_image_prev, String Post_image_prev, VirtualPathData VPD_full_post, VirtualPathData VPD_User, String Author_id, uint Post_id)
        {
            vpd_full_post = VPD_full_post;
            vpd_user = VPD_User;
            text_prev = Post_text_prev;
            author_link.Controls.Add(new Literal() { Text = Post_author_prev });
            author_link.ToolTip = Post_author_prev + " (id" + Author_id + ')';
            post_name_prev.Controls.Add(new Literal() { Text = "<h2>" + Post_name_prev + "</h2>" });
            post_name_prev.ToolTip = Post_name_prev;
            post_date_prev.Controls.Add(new Literal() { Text = Post_date_prev });
            post_views_prev_wrapper.Controls.Add(new Literal() { Text = watch_iteration(Post_id) });
            post_text_prev.Controls.Add(new Literal() { Text = change_text_length(text_prev, str_len_prev) });
            link.Text = "Читать далее";
            link.NavigateUrl = vpd_full_post.VirtualPath;
            post_name_prev.NavigateUrl = vpd_full_post.VirtualPath;
            if (!Post_author_image_prev.Equals(String.Empty))
                post_author_image_prev.ImageUrl = Post_author_image_prev;
            else
                post_author_image_prev.ImageUrl = "~/img/default_user.png";
            if (!Post_image_prev.Equals(String.Empty))
                post_image_prev.ImageUrl = Post_image_prev;
            else
                post_image_prev.ImageUrl = "~/img/default_topic.jpg";
            author_link.NavigateUrl = vpd_user.VirtualPath;
        }

        public String change_text_length(String str, int words)
        {
            //            str = str.Substring(0, str.Substring(0, str_len_prev).LastIndexOf(' ')) + "...";
            return str;
        }

        public void Initialize(UpdatePanel Content)
        {
            Content.ContentTemplateContainer.Controls.Add(post_prev);
            post_prev.Controls.Add(post_head_prev);
            post_head_prev.Controls.Add(post_author_image_wrapper_prev);
            post_author_image_wrapper_prev.Controls.Add(post_author_image_prev);
            post_head_prev.Controls.Add(name_date_wrapper_prev);
            name_date_wrapper_prev.Controls.Add(post_author_prev);
            post_author_prev.Controls.Add(author_link);
            name_date_wrapper_prev.Controls.Add(post_date_prev);
            name_date_wrapper_prev.Controls.Add(post_views_prev_wrapper);
            post_views_prev_wrapper.Controls.Add(post_views_prev);
            post_head_prev.Controls.Add(post_name_prev);
            post_prev.Controls.Add(post_img_wrapper_prev);
            post_img_wrapper_prev.Controls.Add(post_image_prev);
            post_prev.Controls.Add(post_text_prev);
            post_prev.Controls.Add(link);
        }

        public void Initialize_in_Panel(Panel Content)
        {
            Content.Controls.Add(post_prev);
            post_prev.Controls.Add(post_head_prev);
            post_head_prev.Controls.Add(post_author_image_wrapper_prev);
            post_author_image_wrapper_prev.Controls.Add(post_author_image_prev);
            post_head_prev.Controls.Add(name_date_wrapper_prev);
            name_date_wrapper_prev.Controls.Add(post_author_prev);
            post_author_prev.Controls.Add(author_link);
            name_date_wrapper_prev.Controls.Add(post_date_prev);
            name_date_wrapper_prev.Controls.Add(post_views_prev_wrapper);
            post_views_prev_wrapper.Controls.Add(post_views_prev);
            post_head_prev.Controls.Add(post_name_prev);
            post_prev.Controls.Add(post_img_wrapper_prev);
            post_img_wrapper_prev.Controls.Add(post_image_prev);
            post_prev.Controls.Add(post_text_prev);
            post_prev.Controls.Add(link);
        }

        public void Initialize_in_Panel_without_author(Panel Content)
        {
            Content.Controls.Add(post_prev);
            post_prev.Controls.Add(post_head_prev);
            post_head_prev.Controls.Add(post_date_prev);
            post_head_prev.Controls.Add(post_views_prev_wrapper);
            post_views_prev_wrapper.Controls.Add(post_views_prev);
            post_head_prev.Controls.Add(post_name_prev);
            post_prev.Controls.Add(post_img_wrapper_prev);
            post_img_wrapper_prev.Controls.Add(post_image_prev);
            post_prev.Controls.Add(post_text_prev);
            post_prev.Controls.Add(link);
        }

        private String watch_iteration(uint post_id)
        {
            String result = String.Empty;
            uint watch_count = 0;

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String command = "SELECT Count_watch FROM WEB_Pages WHERE (ID = ?)";

            try
            {

                OleDbCommand Command = new OleDbCommand(command, Connection);
                Command.Parameters.Add("ID", OleDbType.BigInt).Value = post_id;
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                if (DT.Rows.Count > 0)
                {
                    uint.TryParse(DT.Rows[0]["Count_watch"].ToString(), out watch_count);
                    result += watch_count.ToString();
                }
            }
            catch (Exception ex)
            {
                result += string.Format("нет данных {0} {1} {2}", ex.Message, watch_count, post_id);
            }

            return result;
        }
    }
}