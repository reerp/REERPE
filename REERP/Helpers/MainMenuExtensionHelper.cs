﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Security;
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;
using System.Web.Security;

namespace Cats.Helpers
{
    public static class MainMenuExtensionHelper
    {
        private static MvcHtmlString Signout()
        {
            FormsAuthentication.SignOut();
            return MvcHtmlString.Create(string.Empty);   
        }

        public static MvcHtmlString EarlyWarningOperationMenuItem(this HtmlHelper helper, string url, EarlyWarningConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "")
        {
            //return MvcHtmlString.Create(@"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>");

            var constants = new EarlyWarningConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.EarlyWarning);

            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }

            var html = string.Empty;
            if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + " class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString EarlyWarningOperationJsLink(this HtmlHelper helper, EarlyWarningConstants.Operation operation, string text = "", string clickFunction = "", string ccsClass = "btn-Approve-Gift", string dataButtontype = "", string id = "")
        {
            //return MvcHtmlString.Create(@"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>");

            var constants = new EarlyWarningConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.EarlyWarning);

            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }

            var html = string.Empty;
            if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                //html = @"<a data-buttontype=" + dataButtontype + " class=" + ccsClass + " href=" + url + ">" + text + "</a>";
                html = @"<a href='#'";
                if (ccsClass != "")
                {
                    html += " class='btn-success'";
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                html += ">";
                if (clickFunction != "" && text != "")
                {
                    html += "<button type = 'button' class='btn btn-xs btn-success' ><span class='fa fa-check'></span>" + text + "</button></a>";
                }
                else
                {
                    html += " ><span class='fa fa-check'></span></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString EarlyWarningOperationButton(this HtmlHelper helper, string url, EarlyWarningConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new EarlyWarningConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.EarlyWarning);

            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }

            var html = string.Empty;

            if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString PSNPOperationMenuItem(this HtmlHelper helper, string text, string url, PsnpConstants.Operation operation, string ccsClass = "", string dataButtontype = "")
        {
            var constants = new PsnpConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.PSNP);

            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }

            var html = string.Empty;
            if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString PSNPOperationButton(this HtmlHelper helper, string url, PsnpConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new PsnpConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.PSNP);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static string isValidOperation(LogisticsConstants.Operation operation)
        {
            var constants = new LogisticsConstants();
            string acces="";
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Logistics);
             
            if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                acces = operation.ToString();
            }
            return acces;
        }
        public static MvcHtmlString LogisticOperationMenuItem(this HtmlHelper helper, string text, string url, LogisticsConstants.Operation operation, string ccsClass = "", string dataButtontype = "")
        {
            var constants = new LogisticsConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Logistics);

            var html = string.Empty;

            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString LogisticsOperationButton(this HtmlHelper helper, string url, LogisticsConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new LogisticsConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Logistics);
            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString ProcurementOperationMenuItem(this HtmlHelper helper, string text, string url, ProcurementConstants.Operation operation, string ccsClass = "", string dataButtontype = "")
        {
            var constants = new ProcurementConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Procurement);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString ProcurementOperationButton(this HtmlHelper helper, string url, ProcurementConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new ProcurementConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Procurement);
            
            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString HubOperationMenuItem(this HtmlHelper helper, string text, string url, HubConstants.Operation operation, string ccsClass = "", string dataButtontype = "")
        {
            var constants = new HubConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Hub);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString HubOperationButton(this HtmlHelper helper, string url, HubConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new HubConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Hub);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }

           else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString RegionalOperationMenuItem(this HtmlHelper helper, string text, string url, RegionalConstants.Operation operation, string ccsClass = "", string dataButtontype = "")
        {
            var constants = new RegionalConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Region);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString RegionalOperationButton(this HtmlHelper helper, string url, RegionalConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new RegionalConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Region);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString FinanceOperationMenuItem(this HtmlHelper helper, string text, string url, FinanceConstants.Operation operation, string ccsClass = "", string dataButtontype = "")
        {
            var constants = new FinanceConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Finance);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString FinanceOperationButton(this HtmlHelper helper, string url, FinanceConstants.Operation operation, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var constants = new FinanceConstants();
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Finance);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else if (ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow)
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString AdminOperationMenuItem(this HtmlHelper helper, string text, string url, string ccsClass = "", string dataButtontype = "")
        {
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Finance);

            var html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else 
            {
                html = @"<a data-buttontype=" + dataButtontype + "  class=" + ccsClass + " href=" + url + ">" + text + "</a>";
            }

            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString AdminOperationButton(this HtmlHelper helper, string url, string text = "", string ccsClass = "", string dataButtontype = "", string id = "")
        {
            var ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Finance);

            string html = string.Empty;
            // If cache is null then force the user to sign-in again
            if (null == ewCache)
            {
                Signout();
                return MvcHtmlString.Create(string.Empty);
            }
            else
            {
                html = "<a href=" + url;
                if (ccsClass != "")
                {
                    html += " class=" + ccsClass;
                }
                if (id != "")
                {
                    html += " id=" + id;
                }
                if (dataButtontype != "")
                {
                    html += " data-buttontype=" + dataButtontype;
                }
                if (text != "")
                {
                    html += " >" + text + "</a>";
                }
                else
                {
                    html += " ></a>";
                }
            }
            return MvcHtmlString.Create(html);
        }
    }
}