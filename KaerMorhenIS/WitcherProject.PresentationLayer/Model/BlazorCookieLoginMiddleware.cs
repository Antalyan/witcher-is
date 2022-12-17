using Microsoft.AspNetCore.Identity;
using System.Collections.Concurrent;
using WitcherProject.PresentationLayer.Model.Administration;


/*
 * Created by https://github.com/dotnet/aspnetcore/issues/13601#issuecomment-682768401
 */
namespace WitcherProject.PresentationLayer.Model
{
    public class BlazorCookieLoginMiddleware<TUser> where TUser : class
    {
        #region Static Login Cache

        public static IDictionary<Guid, LoginModel> Logins { get; set; }
            = new ConcurrentDictionary<Guid, LoginModel>();
        
        public static Guid AnnounceLogin(LoginModel loginInfo)
        {
            loginInfo.LoginStarted = DateTime.Now;
            var key = Guid.NewGuid();
            Logins[key] = loginInfo;
            return key;
        }
        public static LoginModel GetLoginInProgress(string  key)
        {
            return GetLoginInProgress(Guid.Parse(key));
        }

        public static LoginModel GetLoginInProgress(Guid key)
        {
            return Logins.ContainsKey(key) ? Logins[key] : null;
        }

        #endregion

        private readonly RequestDelegate _next;

        public BlazorCookieLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<TUser> signInMgr)
        {
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
            {
                var key = Guid.Parse(context.Request.Query["key"]);
                var loginInfo = Logins[key];

                var result = await signInMgr.PasswordSignInAsync(loginInfo.UserName, loginInfo.Password, false, lockoutOnFailure: false);
                
                //Uncache password for security:
                loginInfo.Password = null;

                if (result.Succeeded)
                {
                    Logins.Remove(key);
                    context.Response.Redirect("/loginsuccess");
                    return;
                }
                if (result.IsLockedOut)
                {
                    loginInfo.Error = "You are locked out. Please contact support.";
                }
                else
                {
                    loginInfo.Error = "Login failed. Check your username and password.";
                    await _next.Invoke(context);
                }
            }
            else if (context.Request.Path.StartsWithSegments("/logout"))
            {
                await signInMgr.SignOutAsync();
                context.Response.Redirect("/login");
                return;
            }

            //Continue http middleware chain:
            await _next.Invoke(context);
        }
    }
}
