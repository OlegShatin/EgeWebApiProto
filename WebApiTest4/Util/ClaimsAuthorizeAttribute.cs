﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApiTest4.Util
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string claimType;
        private string claimValue;

        public ClaimsAuthorizeAttribute() : base()
        {
            
        }
        public ClaimsAuthorizeAttribute(string type, string value)
        {
            this.claimType = type;
            this.claimValue = value;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (user != null && user.HasClaim(claimType, claimValue))
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }

//        public override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            var user = filterContext.HttpContext.User as ClaimsPrincipal;
//            if (user != null && user.HasClaim(claimType, claimValue))
//            {
//                base.OnAuthorization(filterContext);
//            }
//            else
//            {
//                base.HandleUnauthorizedRequest(filterContext);
//            }
//        }
    }
}