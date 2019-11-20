using LibraryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Threading;
using MusicMood.Models;
using System.Text.RegularExpressions;

namespace MusicMood.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Upload(UploadModel model)
        {

            if (model.Title != null && Regex.IsMatch(model.Title, "^[\\wа-яА-Я]+$"))
            {
                
                if (DbService.GetSoundByName(model.Title)?.Title == model.Title ||
                    DbService.GetSoundByMusicFileName(model.SoundObj.FileName)?.MusicName == model.SoundObj.FileName)
                {
                    ModelState.AddModelError(nameof(model.Title), "Song with this name is exsists!");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.Title), "Necessary field 'Title'");
            }

            if (model.Color == null)
            {
                ModelState.AddModelError(nameof(model.Title), "Necessary field 'Color'");
            }

            if (model.SoundImg != null)
            {
                if (model.SoundImg.ContentType != "image/jpeg" && model.SoundImg.ContentType != "image/png"
                    && model.SoundImg.ContentType != "image/tiff" && model.SoundImg.ContentType != "image/gif"
                    && model.SoundImg.ContentType != "image/bmp")
                {
                    ModelState.AddModelError(nameof(model.SoundImg), "Cannot load with current file format!");
                }
            }

            if (model.SoundObj != null)
            {
                if (model.SoundObj?.ContentType != "audio/mpeg" && model.SoundObj?.ContentType != "audio/ogg"
                    && model.SoundObj?.ContentType != "image/tiff" && model.SoundObj?.ContentType != "audio/vnd.wave"
                    && model.SoundObj?.ContentType != "audio/x-ms-wma" && model.SoundObj?.ContentType != "audio/mp3")
                {

                    ModelState.AddModelError(nameof(model.SoundObj), "Cannot load audiofile with current format!");

                }
            }
            

            if (ModelState.IsValid)
            {
                BusinessLogic.SaveInRootFolder(model.SoundObj, "MusicStorage");
                BusinessLogic.SaveInRootFolder(model.SoundImg, "SoundImg");
                DbService.CreateSound(model.Title, model.Album, model.Artist, model.Color, model.Description, model.SoundObj.FileName, model.SoundImg.FileName);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }


        }

    }
}
