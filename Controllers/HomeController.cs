using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MusicApp.Models;
using MusicApp.ViewModels;

namespace MusicApp.Controllers
{
    public class HomeController : AppController
    {
        private readonly UserManager<User, int> _userManager;
        private AppDbContext _db = new AppDbContext();

        private Uri _facebookRedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public HomeController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public HomeController(UserManager<User, int> userManager)
        {
            this._userManager = userManager;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        //GET: Login
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("invalid state");
                return View();
            }

            var user = await _userManager.FindAsync(model.UserName, model.Password);

            if (user != null)
            {
                Debug.WriteLine("correct creds");
                var identity = await _userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid email or password");
            Debug.WriteLine("invalid creds");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        [AllowAnonymous]
        public ActionResult FacebookLogin()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "547825262011313",
                client_secret = "bfa4057d2c74fc3c2086f2c10576255f",
                redirect_uri = _facebookRedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email,user_actions.music,user_likes"
            });

            Debug.WriteLine(loginUrl.AbsoluteUri);

            return Redirect(loginUrl.AbsoluteUri);
        }

        [AllowAnonymous]
        public async Task<ActionResult> FacebookCallback()
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "547825262011313", //App ID
                client_secret = "bfa4057d2c74fc3c2086f2c10576255f", //App Secret
                redirect_uri = _facebookRedirectUri.AbsoluteUri,
                code = Request.QueryString["code"]
            });

            string accessToken = result.access_token;

            if (String.IsNullOrEmpty(accessToken))
            {
                ModelState.AddModelError("", "Was unable to login, please try again");
                RedirectToAction("Login", "Home");
            }

            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email");

            var user = new User
            {
                UserName = String.Format("{0} {1}", me.first_name, me.last_name),
                PasswordHash = null,
                Email = me.email,
                FacebookUserId = me.id
            };

            var isNewUser = _db.Users.FirstOrDefault(u => u.FacebookUserId == user.FacebookUserId && u.PasswordHash == null) == null;

            if (isNewUser)
            {
                Task.Factory.StartNew(() =>
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                });
            }

            var identity = await _userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);
            GetAuthenticationManager().SignIn(identity);

            Session["AccessToken"] = accessToken;
            Session["FacebookLogin"] = true;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Invalid state!");
                return View();
            }

            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FacebookUserId = "false"
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private async Task SignIn(User user)
        {
            var identity = await _userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}