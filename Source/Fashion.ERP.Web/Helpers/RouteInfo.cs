﻿using System;
using System.Web;
using System.Web.Routing;

namespace Fashion.ERP.Web.Helpers
{
    public class RouteInfo
    {
        #region Propriedades
        public RouteData RouteData { get; private set; }
        #endregion

        #region Construtores
        public RouteInfo(RouteData data)
        {
            RouteData = data;
        }

        public RouteInfo(Uri uri, string applicationPath)
        {
            RouteData = RouteTable.Routes.GetRouteData(new InternalHttpContext(uri, applicationPath));
        }
        #endregion

        #region InternalHttpContext
        private class InternalHttpContext : HttpContextBase
        {
            private readonly HttpRequestBase _request;

            public InternalHttpContext(Uri uri, string applicationPath)
            {
                _request = new InternalRequestContext(uri, applicationPath);
            }

            public override HttpRequestBase Request { get { return _request; } }
        }
        #endregion

        #region InternalRequestContext
        private class InternalRequestContext : HttpRequestBase
        {
            private readonly string _appRelativePath;
            private readonly string _pathInfo;

            public InternalRequestContext(Uri uri, string applicationPath)
            {
                _pathInfo = uri.Query;

                if (String.IsNullOrEmpty(applicationPath) || !uri.AbsolutePath.StartsWith(applicationPath, StringComparison.OrdinalIgnoreCase))
                    _appRelativePath = uri.AbsolutePath.Substring(applicationPath.Length);
                else
                    _appRelativePath = uri.AbsolutePath;
            }

            public override string AppRelativeCurrentExecutionFilePath { get { return String.Concat("~", _appRelativePath); } }
            public override string PathInfo { get { return _pathInfo; } }
        }
        #endregion
    }  
}