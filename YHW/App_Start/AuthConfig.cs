using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using YHW.Models.Content;

namespace YHW
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "gyJVnQshv35yuy30g0WA",
                consumerSecret: "eK7LSztiuu7Mt843981ZhUSJK067Ox2X8qjbpoLww4");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "273612212799967",
                appSecret: "c5a64b38dc86770b556a02f497fc1727");

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
