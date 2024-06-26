﻿using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Infrastructure.Helpers.MIddlewares
{
    public class UserSessionValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        private static bool IsAjaxRequest(HttpRequest request) => request.Headers.XRequestedWith == "XMLHttpRequest";

        public async Task InvokeAsync(HttpContext context, UserManager<AppUserEntity> userManager, SignInManager<AppUserEntity> signInManager)
        {
            try
            {
                if (context.User.Identity!.IsAuthenticated)
                {

                    var user = await userManager.GetUserAsync(context.User);

                    if (user == null)
                    {
                        await signInManager.SignOutAsync();

                        if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase))
                        {
                            var signInPath = "/signin";
                            context.Response.Redirect(signInPath);
                            return;
                        }
                    }
                }

                await _next(context);
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
        }
    }
}
