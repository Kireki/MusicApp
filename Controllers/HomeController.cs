using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MusicApp.APIObjectClasses;
using MusicApp.Models;
using MusicApp.ViewModels;
using Newtonsoft.Json;

namespace MusicApp.Controllers
{
    public class HomeController : AppController
    {
        private readonly UserManager<User, int> _userManager;
        private AppDbContext _db = new AppDbContext();

        private Uri FacebookRedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url)
                {
                    Query = null,
                    Fragment = null,
                    Path = Url.Action("FacebookCallback")
                };
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

        
        public LikedFbArtists GetFbArtistLikes()
        {
            if (Session["CurrentUser"] == null)
            {
                var userData = _db.Users.FirstOrDefault(u => u.UserName == CurrentUserClaims.UserName);
                if (userData == null)
                {
                    RedirectToAction("Login", "Home");
                }
                Session["CurrentUser"] = userData;
            }

            var currentUser = (User) Session["CurrentUser"];

            try
            {
                var fbcl = new FacebookClient(currentUser.FacebookAccessToken);
                return JsonConvert.DeserializeObject<LikedFbArtists>(fbcl.Get("me/music").ToString());
            }
            catch (FacebookOAuthException)
            {
                try
                {
                    var fbcl = new FacebookClient();
                    dynamic result = fbcl.Get("oauth/access_token", new
                    {
                        client_id = "547825262011313",
                        client_secret = "bfa4057d2c74fc3c2086f2c10576255f",
                        grant_type = "fb_exchange_token",
                        fb_exchange_token = currentUser.FacebookAccessToken
                    });
                    _db.Users.FirstOrDefault(u => u.Id == currentUser.Id).FacebookAccessToken
                        = result.access_token;
                    _db.SaveChangesAsync();
                    currentUser.FacebookAccessToken = result.access_token;
                    Session["CurrentUser"] = currentUser;
                    return JsonConvert.DeserializeObject<LikedFbArtists>(fbcl.Get("me/music").ToString()); ;
                }
                catch (NullReferenceException e)
                {
                    Debug.WriteLine(e.Message + " " + e.Source);
                    return null;
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message + " " + e.Source);
                return null;
            }
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            if (CurrentUserClaims == null)
            {
                RedirectToAction("Login", "Home");
            }

            if (Session["CurrentUser"] == null)
            {
                Session["CurrentUser"] = _db.Users.FirstOrDefault(u => u.UserName == CurrentUserClaims.UserName);
            }
            var fbArtistsResult = GetFbArtistLikes();

            var currentUser = (User) Session["CurrentUser"];

            var artistsToAdd = new List<Artist>();


//            var artistTable = _db.Artists;
//            Artist addableArtist;
//            foreach (var artist in fbArtistsResult.data.Where(artist => artistTable.FirstOrDefault(a => a.FacebookId == artist.id) == null))
//            {
//                addableArtist = new Artist
//                {
//                    Name = artist.name,
//                };
//                artistsToAdd.Add(addableArtist);
//            }
//
//            if (artistsToAdd.Count != 0)
//            {
//                _db.Artists.AddRange(artistsToAdd);
//                _db.SaveChanges();
//            }
//
//            var userArtistsToAdd = new List<Artist>();
//            foreach (var artist in fbArtistsResult.data.Where(artist => currentUser.Artists.FirstOrDefault(a => a.FacebookId == artist.id) == null))
//            {
//                addableArtist = artistTable.FirstOrDefault(a => a.FacebookId == artist.id);
//                userArtistsToAdd.Add(addableArtist);
//            }
//
//            if (userArtistsToAdd.Count == 0) return View();
//            var updatableUser = (User) Session["CurrentUser"];
//            foreach (var artist in userArtistsToAdd)
//            {
//                updatableUser.Artists.Add(artist);
//            }

            Artist check;
            foreach (var artist in fbArtistsResult.data)
            {
                check = new Artist
                {
                    Id = artist.id,
                    Name = artist.name
                };
                if (!currentUser.Artists.Contains(check))
                {
                    artistsToAdd.Add(check);
                }
            }
            if (artistsToAdd.Count != 0)
            {
                foreach (var artist in artistsToAdd)
                {
                    currentUser.Artists.Add(artist);
                    Debug.WriteLine(artist.Name + " - added");
                }
                _db.SaveChanges();
            }
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
                var identity = await _userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);
                Session["CurrentUser"] = user;
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
                redirect_uri = FacebookRedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email,user_actions.music,user_likes"
            });
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
                redirect_uri = FacebookRedirectUri.AbsoluteUri,
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

            var userData = new User
            {
                FacebookName = String.Format("{0} {1}", me.first_name, me.last_name),
                FacebookUserId = me.id,
                FacebookAccessToken = accessToken,
                UserName = me.email
            };

            var user = _db.Users.FirstOrDefault(u => u.UserName == userData.UserName);

            if (user == null)
            {
                Task.Factory.StartNew(() =>
                {
                    _db.Users.Add(userData);
                    _db.SaveChanges();
                });
                user = userData;
            }

            var identity = await _userManager.CreateIdentityAsync(
                    userData, DefaultAuthenticationTypes.ApplicationCookie);
            GetAuthenticationManager().SignIn(identity);

            Session["AccessToken"] = accessToken;
            Session["CurrentUser"] = user;

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

            var user = new User
            {
                UserName = model.Email,
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