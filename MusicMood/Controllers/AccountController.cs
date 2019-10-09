using LibraryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicMood.Models;

namespace MusicMood.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (DbService.PersonEmailExists(registerModel.Email))
            {
                ModelState.AddModelError(nameof(registerModel.Email), "this email is existed in system");
            }

            if (DbService.PersonLoginExists(registerModel.Login))
            {
                ModelState.AddModelError(nameof(registerModel.Login), "this login is existed in system");
            }

            if (!(registerModel.DateOfBirth > DateTime.Now.AddYears(-120) 
                && registerModel.DateOfBirth < DateTime.Now.AddYears(-3)))
            {
                ModelState.AddModelError(nameof(registerModel.DateOfBirth), "incorrect Date of Birth");
            }

            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(registerModel.Login,true);
                DbService.CreatePerson(registerModel.FirstName,registerModel.SecondName,registerModel.Login,
                    registerModel.Email,registerModel.Password,registerModel.DateOfBirth);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Authorize()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(AuthorizeModel model)
        {
            if (!DbService.AutorizeConfirm(model.Login,model.Password))
            {
                ModelState.AddModelError("Confirm", "Email или пароль не совпадают");
            }

            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}