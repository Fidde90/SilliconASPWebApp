﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Helpers.MIddlewares
{
    public class UserSessionValidationMiddleware
    {
        private readonly RequestDelegate _next;

        // Ajax står för Asyncroness Javascript And Xml.
        private static bool IsAjaxRequest(HttpRequest request) => request.Headers.XRequestedWith == "XMLHttpRequest";

        public UserSessionValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<AppUserEntity> userManager, SignInManager<AppUserEntity> signInManager)
        {
            if(context.User.Identity!.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);

                if(user == null)
                {
                    await signInManager.SignOutAsync();

                    if(!IsAjaxRequest(context.Request) && context.Request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase))
                    {
                        var signInPath = "/signin";
                        context.Response.Redirect(signInPath);
                        return;
                    }
                }       
            }
            await _next(context);
        }
    }
}
