using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPool.Library
{

    public static class UserSession
    {
        public static int UserId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(System.Web.HttpContext.Current.Session["UserId"].ToString());
                }
            }

            set
            {
                System.Web.HttpContext.Current.Session["UserId"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserName"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["UserName"].ToString();
                }
            }

            set
            {
                System.Web.HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static string DisplayName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["DisplayName"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["DisplayName"].ToString();
                }
            }

            set
            {
                System.Web.HttpContext.Current.Session["DisplayName"] = value;
            }
        }

        public static string Email
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Email"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["Email"].ToString();
                }
            }

            set
            {
                System.Web.HttpContext.Current.Session["Email"] = value;
            }
        }

        public static string Country
        {
            get
            {
                  return System.Web.HttpContext.Current.Session["Country"].ToString();
            }

            set
            {
                System.Web.HttpContext.Current.Session["Country"] = value;
            }
        }

        public static string ProfileImage
        {
            get
            {
                 return System.Web.HttpContext.Current.Session["ProfileImage"].ToString();
            }

            set
            {
                System.Web.HttpContext.Current.Session["ProfileImage"] = value;
            }
        }

        
    }
}