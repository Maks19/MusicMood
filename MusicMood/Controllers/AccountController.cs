using LibraryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicMood.Models;
using System.Text.RegularExpressions;
using System.Web.Helpers;

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

            if (registerModel.Email != null && Regex.IsMatch(registerModel.Email, "^[a-zA-Z0-9_\\.\\+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-\\.]+$"))
            {
                if (DbService.GetPersonByEmail(registerModel.Email) != null)
                {
                    ModelState.AddModelError(nameof(registerModel.Email), "this email is existed in system");
                }

            }
            else {
                ModelState.AddModelError(nameof(registerModel.Email), "Email is not valid");
            }

            if (registerModel.Login != null && Regex.IsMatch(registerModel.Login, "^[a-zA-Z0-9_\\.]+$")) {
                if (DbService.GetPersonByLogin(registerModel.Login) != null)
                {
                    ModelState.AddModelError(nameof(registerModel.Login), "this login is existed in system");
                }
            }
            else {
                ModelState.AddModelError(nameof(registerModel.Login), "Login is not valid");
            }

            if (!(registerModel.DateOfBirth > DateTime.Now.AddYears(-120) 
                && registerModel.DateOfBirth < DateTime.Now.AddYears(-3)))
            {
                ModelState.AddModelError(nameof(registerModel.DateOfBirth), "incorrect Date of Birth");
            }

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(registerModel.ConfirmPassword), "Passwords dont match");
            }

            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(registerModel.Login,true);
                DbService.CreatePerson(registerModel.FirstName,registerModel.SecondName,registerModel.Login,
                    registerModel.Email,BusinessLogic.CryptographyMD5(registerModel.Password),registerModel.DateOfBirth);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Recovery() {
            return View();
        }
        [HttpPost]
        public ActionResult Recovery(FormCollection formCollection)
        {
            var user = DbService.GetPersonByLogin(formCollection["Login"]);
          
            if (user != null)
            {
                string password = BusinessLogic.GenerateStrongPassword();
                BusinessLogic.Notify(user,password);
                string cryptoPassword = BusinessLogic.CryptographyMD5(user.Password);
                DbService.UpdatePassword(user.Id, cryptoPassword);
              
                ViewBag.Msg = "Пароль успешно обновлен!Проверьте свою почту";
                
                
            }
            else {
                ViewBag.Msg = "Пользователя с таким логином не сущетствует!";
            }
            return View();

        }

        [HttpGet]
        public ActionResult Authorize()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(AuthorizeModel model)
        {
            if (DbService.AutorizeConfirm(model.Login, BusinessLogic.CryptographyMD5(model.Password)) == null)
            {
                ModelState.AddModelError("Confirm", "Логин или пароль не совпадают");
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