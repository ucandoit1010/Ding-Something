using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DINGSOMETHING.Models.BusinessAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DINGSOMETHING.Controllers
{

    public class UserController : Controller
    {
        private Member memberOperation;


        public UserController(){
            memberOperation = new Member();

        }

        public IActionResult Logout(){

            if(HttpContext.User.Identity.IsAuthenticated){
                
                HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
                
            }
            
            return RedirectToAction("Login");
        }


        public IActionResult Login(string url = null){
            
            if(Url.IsLocalUrl(url)){
                return Redirect(url);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Validate(string Account , string Password) {
            if(string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password)) {
                TempData["Error"] = "WRONG Account or Password !";

                return RedirectToAction("Login");
            }

            Member member = new Member();
            member.Account = Account;
            member.Password = Password;

            var data = memberOperation.Validate(member);

            if(data == null){
                TempData["Error"] = "Can not Found User !";

                return RedirectToAction("Login");
            }

            var claimList = new List<Claim>();
            Claim claim = new Claim(ClaimTypes.Name, data.Name);
            Claim claimId = new Claim("User", 
                Models.Helper.EncryptHelper.EncryptString(data.Id.ToString()));
            
            claimList.Add(claim);
            claimList.Add(claimId);
            
            var claimsIdentity = new ClaimsIdentity(claimList,"Password");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            // System.Threading.Thread.CurrentPrincipal = claimsPrincipal;

            HttpContext.SignInAsync("MyCookieAuthenticationScheme",
                claimsPrincipal,
                new AuthenticationProperties{
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(5)
                });

            return RedirectToAction("Index","Product");
        }


    }

}