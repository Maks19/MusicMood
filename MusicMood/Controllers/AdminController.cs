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

            if (model.Title != null && Regex.IsMatch(model.Title, "^[\\wа-яА-Я\\s_$#&*!]+$"))
            {
                
                if (DbService.GetSoundByName(model.Title)?.Title == model.Title)
                {
                    ModelState.AddModelError(nameof(model.Title), "Song with this name is exsists!");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.Title), "Necessary field 'Title' or not valid");
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
            else {
                ModelState.AddModelError(nameof(model.SoundObj), "Load audio file!");
            }


            if (ModelState.IsValid)
            {
                BusinessLogic.SaveInRootFolder(model.SoundObj, "MusicStorage");
                if (model.SoundImg != null)
                {
                    BusinessLogic.SaveInRootFolder(model.SoundImg, "SoundImg");
                }
                DbService.CreateSound(model.Title, model.Album, model.Artist, model.Color, model.Description, model.SoundObj.FileName, model.SoundImg?.FileName ?? "default-release.png");
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult PlayListCreating()
        {
            List<Sound> sounds = DbService.FetchAllMusic();

            IEnumerable<SelectListItem> soundSelector =
                sounds.Select( 
                    s=>new SelectListItem()
                    {
                        Text = s.MusicName,
                        Value = s.Id.ToString()
                    });
            ViewData["Sounds"] = soundSelector;

            return View();
        }

        [HttpPost]
        public ActionResult PlayListCreating(PlayList playList)
        {
            if (playList.Name == null)
            {
                ModelState.AddModelError(nameof(playList.Name), "Поле 'Название' не заполненно");
            }

            if(playList.Color == null)
            {
                ModelState.AddModelError(nameof(playList.Color), "Цвет не выбран");
            }

            HttpCookie soundsCookie = ControllerContext.HttpContext.Request.Cookies["sounds"];
            SortedSet<Sound> sounds = new SortedSet<Sound>();

            if (soundsCookie != null)
            {
                string[] soundsId = soundsCookie.Value.Split(',');

                for (int i = 0; i < soundsId.Length; i++)
                {
                    sounds.Add(DbService.GetSoundById(Convert.ToInt32(soundsId[i])));
                }

                if (sounds.Count < 2)
                {
                    ModelState.AddModelError("Sounds-Error", "Добавьте хотябы 2 песни");
                }
            }
            else
            {
                ModelState.AddModelError("Sounds-Error", "Добавьте песни");
            }

            if (ModelState.IsValid)
            {
                DbService.CreatePlayList(playList.Name,playList.Color);
                PlayList lastPlayList = DbService.GetPalyListWithMaxId();
                foreach (Sound sound in sounds)
                {
                    DbService.CreatePlayListSound(sound.Id,lastPlayList.Id);
                }
                soundsCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(soundsCookie);
                return RedirectToAction("Index");
            }

            List<Sound> sounds1 = DbService.FetchAllMusic();

            IEnumerable<SelectListItem> soundSelector =
                sounds1.Select(
                    s => new SelectListItem()
                    {
                        Text = s.MusicName,
                        Value = s.Id.ToString()
                    });
            ViewData["Sounds"] = soundSelector;

            return View();
        }

        public ActionResult PartialSounds(string soundId)
        {
            Thread.Sleep(2000);

            HttpCookie soundsCookie = ControllerContext.HttpContext.Request.Cookies["sounds"];
            SortedSet<Sound> sounds = new SortedSet<Sound>();

            if (Request.Cookies["sounds"] == null)
            {
                soundsCookie = new HttpCookie("sounds");
                soundsCookie.Value = soundId;
                sounds.Add(DbService.GetSoundById(Convert.ToInt32(soundId)));
            }
            else
            {
                string[] valArray = soundsCookie.Value.Split(',');
                soundsCookie.Value += $",{soundId}";

                foreach (string val in valArray)
                {
                    sounds.Add(DbService.GetSoundById(Convert.ToInt32(val)));
                }
                sounds.Add(DbService.GetSoundById(Convert.ToInt32(soundId)));
            }

            HttpContext.Response.Cookies.Add(soundsCookie);

            return View(sounds);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminPage()
        {
            return View();
        }
    }
}