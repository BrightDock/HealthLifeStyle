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

namespace WEB
{
    public class Comments
    {
        private UpdatePanel commentsPlace { get; set; } = new UpdatePanel() { ID = "commentsWrapper" };
        private UpdatePanel leavedComments { get; set; } = new UpdatePanel() { ID = "leavedComments" };
        private UpdatePanel newCommentBox { get; set; } = new UpdatePanel() { ID = "newComment" };
        private uint postAuthorID { get; set; } = new uint();
        private uint userID { get; set; } = new uint();
        private uint pageID { get; set; } = new uint();


        public Comments()
        {

        }

        public Comments(UpdatePanel CommentsPlace, uint AuthorID, uint PageID, uint UserID)
        {
            CommentsPlace.ContentTemplateContainer.Controls.Add(commentsPlace);
            postAuthorID = AuthorID;
            userID = UserID;
            pageID = PageID;
            reloadComments();
        }

        public void reloadComments()
        {
            commentsPlace.ContentTemplateContainer.Controls.Clear();

            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "SELECT Comments.ID, Comments.Author_ID, Users.Name AS Author_name, Users.Surname AS Author_surname, Users.Avatar, Comments.Text, Comments.Target_type, " +
                "Comments.Target_ID, Comments.[Date] FROM Comments INNER JOIN Users ON Comments.Author_ID = Users.ID INNER JOIN WEB_Pages ON Comments.Target_ID = WEB_Pages.ID " +
                "WHERE(Comments.Target_ID = ?)";

            try
            {
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                Command.Parameters.Add("Target_ID", OleDbType.BigInt).Value = pageID;
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                commentsPlace.ContentTemplateContainer.Controls.Add(new Literal() { Text = DT.Rows.Count > 0 ? String.Format("<h3>Комментарии<small>{0}</small></h3>", DT.Rows.Count.ToString()) : String.Empty });
                commentsPlace.ContentTemplateContainer.Controls.Add(leavedComments);
                commentsPlace.ContentTemplateContainer.Controls.Add(newCommentBox);
                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow row in DT.Rows)
                    {
                        RouteValueDictionary parameters_User = new RouteValueDictionary{
                            { "id", Convert.ToInt32(row["Author_id"].ToString()) }
                        };

                        Comment newComment = new Comment(
                                row["Author_name"].ToString().TrimEnd() + ' ' + row["Author_surname"].ToString().TrimEnd(),
                                DateTime.Parse(row["Date"].ToString()).ToShortDateString() + ' ' + DateTime.Parse(row["Date"].ToString()).ToShortTimeString(),
                                new TextBox() { Text = row["Text"].ToString() },
                                row["Avatar"].ToString(),
                                RouteTable.Routes.GetVirtualPath(null, "User", parameters_User),
                                uint.Parse(row["Author_id"].ToString()),
                                "0",
                                userID,
                                postAuthorID,
                                leavedComments,
                                pageID,
                                uint.Parse(row["ID"].ToString())
                            );
                        newComment.commentInit();

                        /*
                                                vpd_user = RouteTable.Routes.GetVirtualPath(null, "User", parameters_User);
                                                commentAuthorHeadLink.Controls.Add(new Literal() { Text = row["Author_name"].ToString().TrimEnd() + ' ' + row["Author_surname"].ToString().TrimEnd() });
                                                commentAuthorHeadLink.ToolTip = commentAuthorHeadLink.Text + " (id" + uint.Parse(row["Author_id"].ToString()) + ')';
                                                commentAuthorLink.ToolTip = commentAuthorHeadLink.Text + " (id" + uint.Parse(row["Author_id"].ToString()) + ')';
                                                commentDate.Controls.Add(new Literal() { Text = string.Format("<span><i class='fa fa-calendar' aria-hidden='true'></i>{0}</span>", DateTime.Parse(row["Date"].ToString()).ToShortDateString() + ' ' + DateTime.Parse(row["Date"].ToString()).ToShortTimeString()) });
                                                commentText.Controls.Add(new Literal() { Text = row["Text"].ToString() });
                                                if (!row["Avatar"].ToString().Equals(String.Empty))
                                                {
                                                    commentAuthorImage.ImageUrl = row["Avatar"].ToString();
                                                }
                                                else
                                                {
                                                    commentAuthorImage.ImageUrl = "~/img/default_user.png";
                                                }
                                                commentAuthorLink.NavigateUrl = vpd_user.VirtualPath;
                                                commentAuthorHeadLink.NavigateUrl = vpd_user.VirtualPath;
                                                commentReply.Controls.Add(new Literal() { Text = "<i class='fa fa-reply' aria-hidden='true'></i> Ответить" });
                                                commentLike.Controls.Add(new Literal() { Text = "<i class='fa fa-thumbs-o-up' aria-hidden='true'></i> " + "0" });*/


                    }
                }
                if (userID != new uint())
                {
                    writeCommentLoad();
                }
                else
                {
                    notAutorized();
                }

            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException(string.Format("Не удалось получить {0}", ex.Message));
            }
        }

        public void writeCommentLoad()
        {
            Panel commentButtons = new Panel() { CssClass = "commentButtons" };
            Panel commentText = new Panel() { CssClass = "commentText" };
            LinkButton commentSend = new LinkButton() { CssClass = "commentSend", Text = "<i class='fa fa-paper-plane-o' aria-hidden='true'></i> Отправить" };
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "SELECT Users.ID, Users.Avatar FROM Users WHERE(Users.ID = ?)";
            TextBox text = new TextBox() { CssClass = "newCommentText textBox", ID = "newCommentText", TextMode = TextBoxMode.MultiLine };
            text.Attributes.Add("placeholder", "Ваш текст...");

            try
            {
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                Command.Parameters.Add("ID", OleDbType.BigInt).Value = userID.ToString();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                RouteValueDictionary parameters_User = new RouteValueDictionary{
                    { "id", Convert.ToInt32(DT.Rows[0]["ID"].ToString()) }
                };

                Comment newComment = new Comment(
                    "",
                    "",
                    text,
                    DT.Rows[0]["Avatar"].ToString(),
                    RouteTable.Routes.GetVirtualPath(null, "User", parameters_User),
                    userID,
                    "0",
                    userID,
                    0,
                    newCommentBox,
                    pageID,
                    0,
                    commentsPlace
                );

                commentSend.Click += newComment.sendComment;
                newComment.comment.ContentTemplateContainer.Controls.Add(new Literal() { Text = "<h3>Ваш комментарий</h3>" });
                newComment.commentInit();
                newComment.commentBody.Controls.Clear();
                newComment.commentBody.Controls.Add(commentText);
                commentText.Controls.Add(text);
                commentButtons.Controls.Add(commentSend);
                newComment.commentBody.Controls.Add(commentButtons);

                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = commentSend.UniqueID;
                trigger.EventName = "Click";
                commentsPlace.Triggers.Add(trigger);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public static void notAutorized()
        {

        }

        ~Comments()
        {

        }
    }


    public class Comment
    {
        public UpdatePanel comment { get; set; } = new UpdatePanel() { ChildrenAsTriggers = true };
        private Panel commentAuthorImageWrapper { get; set; } = new Panel() { CssClass = "commentAuthorImageWrapper" };
        private Image commentAuthorImage { get; set; } = new Image() { CssClass = "commentAuthorImage" };
        public Panel commentBody { get; set; } = new Panel() { CssClass = "commentBody" };
        private Panel commentHead { get; set; } = new Panel() { CssClass = "commentHead" };
        private HyperLink commentAuthorLink { get; set; } = new HyperLink() { CssClass = "post_author_image_wrapper_prev" };
        private HyperLink commentAuthorHeadLink { get; set; } = new HyperLink() { CssClass = "commentAuthorLink" };
        private Panel commentDate { get; set; } = new Panel() { CssClass = "comment_date_prev" };
        private Panel commentText { get; set; } = new Panel() { CssClass = "commentText" };
        public Panel commentButtons { get; set; } = new Panel() { CssClass = "commentButtons" };
        public Panel commentOptions { get; set; } = new Panel() { CssClass = "commentOptions" };
        private LinkButton commentEdit { get; set; } = new LinkButton() { CssClass = "commentEdit", Text = "<i class='fa fa-pencil' aria-hidden='true'></i>" };
        private LinkButton commentDelete { get; set; } = new LinkButton() { CssClass = "commentDelete", Text = "<i class='fa fa-times' aria-hidden='true'></i>" };
        private Label commentReply { get; set; } = new Label() { CssClass = "commentReply" };
        public LinkButton commentLike { get; set; } = new LinkButton() { CssClass = "commentLike" };
        private TextBox newCommentText { get; set; } = new TextBox() { CssClass = "commentText" };
        private UpdatePanel leavedComments { get; set; } = new UpdatePanel();
        private uint pageID { get; set; } = new uint();
        private uint authorID { get; set; } = new uint();
        private uint userID { get; set; } = new uint();
        private uint commentID { get; set; } = new uint();
        private uint postAuthorID { get; set; } = new uint();
        private VirtualPathData vpd_user { get; set; }
        private UpdatePanel thisComments { get; set; }

        public Comment()
        {

        }

        public Comment(String commentAuthor, String CommentDate, TextBox CommentText, String CommentAuthorImage, VirtualPathData VPD_User, uint AuthorID, String likesCount, uint UserID, uint PostAuthorID, UpdatePanel LeavedComments, uint PageID, uint CommentID = new uint(), UpdatePanel ThisComments = null)
        {
            comment.Attributes.Add("class", "comment");
            commentID = CommentID;
            leavedComments = LeavedComments;
            vpd_user = VPD_User;
            postAuthorID = PostAuthorID;
            authorID = AuthorID;
            userID = UserID;
            pageID = PageID;
            ThisComments = thisComments;
            newCommentText = CommentText;
            commentAuthorHeadLink.Controls.Add(new Literal() { Text = commentAuthor });
            commentAuthorHeadLink.ToolTip = commentAuthor + " (id" + authorID + ')';
            commentAuthorLink.ToolTip = commentAuthor + " (id" + authorID + ')';
            commentDate.Controls.Add(new Literal() { Text = string.Format("<span><i class='fa fa-calendar' aria-hidden='true'></i>{0}</span>", CommentDate) });
            commentText.Controls.Add(new Literal() { Text = CommentText.Text });
            if (!CommentAuthorImage.Equals(String.Empty))
            {
                commentAuthorImage.ImageUrl = CommentAuthorImage;
            }
            else
            {
                commentAuthorImage.ImageUrl = "~/img/default_user.png";
            }
            commentAuthorLink.NavigateUrl = vpd_user.VirtualPath;
            commentAuthorHeadLink.NavigateUrl = vpd_user.VirtualPath;
            commentReply.Controls.Add(new Literal() { Text = "<i class='fa fa-reply' aria-hidden='true'></i> Ответить" });
            commentLike.Controls.Add(new Literal() { Text = string.Format("<span><i class='fa fa-thumbs-o-up' aria-hidden='true'></i> {0}</span>", likesCount) });
        }

        public void commentInit(Panel updatePanel = null)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "SELECT Users.ID, Users.Avatar FROM Users WHERE(Users.ID = ?) AND (Users.Account_type_id = 1 OR Users.Account_type_id = 3)";
            bool isAdmin = false;

            try
            {
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                Command.Parameters.Add("ID", OleDbType.BigInt).Value = userID.ToString();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                if (DT.Rows.Count > 0)
                {
                    isAdmin = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            /*            if (updatePanel != null)
                        {
                            updatePanel.Controls.Add(comment);
                        }
                        else
                        {*/
            leavedComments.ContentTemplateContainer.Controls.Add(comment);
            //            }
            comment.ContentTemplateContainer.Controls.Add(commentAuthorImageWrapper);
            comment.ContentTemplateContainer.Controls.Add(commentBody);
            commentAuthorImageWrapper.Controls.Add(commentAuthorLink);
            commentAuthorLink.Controls.Add(commentAuthorImage);
            commentBody.Controls.Add(commentHead);
            commentHead.Controls.Add(commentAuthorHeadLink);
            commentHead.Controls.Add(commentDate);
            if (isAdmin || (postAuthorID == userID) || (authorID == userID))
            {
                commentHead.Controls.Add(commentOptions);
                commentOptions.Controls.Add(commentEdit);
                commentOptions.Controls.Add(commentDelete);
                commentDelete.Click += deleteComment;
            }
            commentBody.Controls.Add(commentText);
            commentBody.Controls.Add(commentButtons);
            if (userID != new uint())
            {
                commentButtons.Controls.Add(commentReply);
            }
            commentButtons.Controls.Add(commentLike);
            if (userID != new uint())
            {
                commentLike.Click += likeClick;
            }
        }

        public void sendComment(object sender, EventArgs e)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "INSERT INTO Comments (Author_ID, Text, Target_type, Target_ID, Date) VALUES (?, ?, ?, ?, ?)";

            try
            {
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                Command.Parameters.Add("Author_ID", OleDbType.BigInt).Value = authorID;
                Command.Parameters.Add("Text", OleDbType.LongVarWChar).Value = newCommentText.Text;
                Command.Parameters.Add("Target_type", OleDbType.WChar).Value = "WEB_Pages";
                Command.Parameters.Add("Target_ID", OleDbType.BigInt).Value = pageID;
                Command.Parameters.Add("Date", OleDbType.DBTimeStamp).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();

                if (thisComments != null)
                {
                    /*                    if (DT.Rows.Count > 0)
                                        {
                                            RouteValueDictionary parameters_User = new RouteValueDictionary{
                                                { "id", userID }
                                            };

                                            Comment newComment = new Comment(
                                                    row["Author_name"].ToString().TrimEnd() + ' ' + row["Author_surname"].ToString().TrimEnd(),
                                                    OleDbType.DBTimeStamp).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0),
                                                    this.newCommentText.Text },
                                                    row["Avatar"].ToString(),
                                                    RouteTable.Routes.GetVirtualPath(null, "User", parameters_User),
                                                    uint.Parse(row["Author_id"].ToString()),
                                                    "0",
                                                    userID,
                                                    postAuthorID,
                                                    leavedComments,
                                                    pageID
                                                );
                                            newComment.commentInit();
                                        }
                                        */
                    thisComments.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            /*            TextBox text = (TextBox)commentText.FindControl("newCommentText");
                        String author = comment.Attributes["User"].ToString();
                        int newComPosiion = commentsPlace.ContentTemplateContainer.Controls.IndexOf(commentsPlace.FindControl("newComment"));

                        Comments newComment = new Comments();
                        newComment.commentText.CssClass += " newCommentTextBox";
                        newComment.commentText.Controls.Add(new Literal() { Text = text.Text.Replace("\n", "<br />") + " send by user with id" + author });
                        newComment.commentReply.Controls.Add(new Literal() { Text = "<i class='fa fa-reply' aria-hidden='true'></i> Ответить" });
                        newComment.Initialize(commentsPlace);

                        commentsPlace.ContentTemplateContainer.FindControl("newComment").Controls.Clear();
                        commentsPlace.ContentTemplateContainer.Controls.Remove(commentsPlace.ContentTemplateContainer.FindControl("newComment"));
                        commentsPlace.ContentTemplateContainer.Controls.Add(newComment.comment);

                        writeCommentLoad(commentsPlace, author);*/
        }

        public void likeClick(object sender, EventArgs e)
        {
            commentHead.Controls.Add(new Literal() { Text = "I, m LIKED, BEATCH!" });
        }

        public void deleteComment(object sender, EventArgs e)
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "DELETE FROM Comments WHERE (Comments.ID = ?)";

            try
            {
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                Command.Parameters.Add("ID", OleDbType.BigInt).Value = commentID;
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();

                if (thisComments != null)
                {
                    thisComments.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}