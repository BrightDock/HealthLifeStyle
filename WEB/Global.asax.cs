using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WEB
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.MapPageRoute("Post",
                "Posts/{post_id}",
                "~/Post.aspx", true, new RouteValueDictionary { { "post_id", @"^\d+$" } });

            routes.MapPageRoute("Post_new",
                "PostWrite",
                "~/Account/WritePost.aspx", true, null);

            routes.MapPageRoute("User",
                "Users/{id}",
                "~/Account/User.aspx", true, new RouteValueDictionary { { "id", @"^\d+$" } });

            routes.MapPageRoute("User_Posts",
                "Users/Posts/{id}",
                "~/Post.aspx", true, new RouteValueDictionary { { "id", @"^\d+$" } });

            routes.MapPageRoute("My_Posts",
                "MyPosts",
                "~/Post.aspx", true, null);

            routes.MapPageRoute("User_Chronologys",
                "Results",
                "~/MyResults.aspx", true, null);

            routes.MapPageRoute("User_settings",
                "Settings",
                "~/Account/Settings.aspx", true, null);

            routes.MapPageRoute("Search_empty",
                "Search/",
                "~/Account/Search.aspx", true, null);

            routes.MapPageRoute("Search",
                "Search/{words}",
                "~/Account/Search.aspx", true, new RouteValueDictionary { { "words", @"[a-zA-Z0-9а-яА-Я \d]" } });
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}