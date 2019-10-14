using LibraryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;
using MusicMood.Models;

namespace MusicMood.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("AdminPage");
            }

            return View();
        }

        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie1 = new  HttpCookie(FormsAuthentication.FormsCookieName,"");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            return RedirectToAction("Authorize", "Account");
        }

        [Authorize(Roles = "user")]
        public ActionResult Setting()
        {
            Person person = DbService.GetPersonByLogin(HttpContext.User.Identity.Name);
            return View(person);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult Setting(FormCollection formResult)
        {
            Person currentPerson = DbService.GetPersonByLogin(HttpContext.User.Identity.Name);
            if (formResult["Login"] != null )
            {
                Person updatingPerson = new Person()
                {
                    FirstName = formResult["FirstName"],
                    SecondName = formResult["SecondName"],
                    Login = formResult["Login"]
                };

                if (formResult["FirstName"].Length < 3 || formResult["FirstName"].Length > 12)
                {
                    ModelState.AddModelError("FirstName",
                        "Имя не должно быть короче 3х символов и длинее 12ти");
                }

                if (formResult["SecondName"].Length < 3 || formResult["SecondName"].Length > 12)
                {
                    ModelState.AddModelError("SecondName",
                        "Фамилия не должна быть короче 3х символов и длинее 12ти");
                }

                if (updatingPerson.Login != null && Regex.IsMatch(updatingPerson.Login, "^[a-zA-Z0-9_\\.]+$"))
                {
                    Person tempPerson = DbService.GetPersonByLogin(updatingPerson.Login);
                    if (!(tempPerson == null || tempPerson?.Login == HttpContext.User.Identity.Name))
                    {
                        ModelState.AddModelError("Login", "this login is existed in system");
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Login is not valid");
                }

                if (!ModelState.IsValid)
                {
                    ViewData["PersonErrorMessage"] = "Не все поля заполнены правильно";
                    ViewData["PersonConfirmedMessage"] = "";
                }
                else
                {
                    ViewData["PersonErrorMessage"] = "";
                    ViewData["PersonConfirmedMessage"] = "Данные успешно обновлены";
                    DbService.UpdatePersonData(currentPerson.Id,updatingPerson.Login,updatingPerson.FirstName,updatingPerson.SecondName);
                    FormsAuthentication.SetAuthCookie(updatingPerson.Login, true);
                }

                return View(updatingPerson);
            }
            else
            {
                if (DbService.AutorizeConfirm(currentPerson.Login,
                        BusinessLogic.CryptographyMD5(formResult["OldPassword"])) == null)
                {
                    ModelState.AddModelError("OldPassword","Текущий пароль введён не правильно");
                }

                if (formResult["newPassword"].Length >= 6 && formResult["newPassword"].Length <= 15)
                {
                    if (formResult["newPassword"] != formResult["NewConfirmedPassword"])
                    {
                        ModelState.AddModelError("NewConfirmedPassword",
                            "Пароль не совпадают");
                    }
                }
                else
                {
                    ModelState.AddModelError("newPassword",
                        "Новый пароль не должна быть короче 6ти символов и длинее 15ти");
                }

                if (!ModelState.IsValid)
                {
                    ViewData["PasswordErrorMessage"] = "Не все поля заполнены правильно";
                    ViewData["PasswordConfirmedMessage"] = "";
                }
                else
                {
                    ViewData["PasswordErrorMessage"] = "";
                    ViewData["PasswordConfirmedMessage"] = "Данные успешно обновлены"; 
                    DbService.UpdatePassword(currentPerson.Id, BusinessLogic.CryptographyMD5(formResult["newPassword"]));
                }
                return View(currentPerson);
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminPage()
        {
            return View();
        }
    }
}