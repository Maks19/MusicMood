using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryServices
{
    public class DbService
    {
        public static void CreatePerson
        (string firstName, string secondName, string login, string email, string password, DateTime dateOfBirth)
        {
            Person person = new Person()
            {
                FirstName = firstName,
                SecondName = secondName,
                Email = email,
                Password = password,
                Login = login,
                DateOfBirth = dateOfBirth,
                RoleId = 2
            };
            using (var ctx = new MusicContext("MusicContext"))
            {
                ctx.Persons.Add(person);
                ctx.SaveChanges();
            }
        }
        public static void CreateSound(string title, string album, string artist, string color, string description, string musicName, string imgName)
        {
            Sound sound = new Sound()
            {
                Title = title,
                Album = album,
                Artist = artist,
                Color = color,
                Description = description,
                MusicName = musicName,
                ImgName = imgName
            };

            using (var ctx = new MusicContext("MusicContext"))
            {
                ctx.Sounds.Add(sound);
                ctx.SaveChanges();
            }

        }

        public static void UpdatePersonData(int id, string login, string firstName, string secondName)
        {
            using (var ctx = new MusicContext("MusicContext"))
            {
                Person person = ctx.Persons.FirstOrDefault(p => p.Id == id);
                person.Login = login;
                person.FirstName = firstName;
                person.SecondName = secondName;

                ctx.Entry(person).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void UpdatePassword(int personId, string password)
        {

            using (var ctx = new MusicContext("MusicContext"))

            {
                Person person = ctx.Persons.FirstOrDefault(p => p.Id == personId);
                person.Password = password;
                ctx.Entry(person).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }


        public static Person GetPersonByEmail(string email)
        {
            Person person;
            using (var ctx = new MusicContext("MusicContext"))
            {
                person = ctx.Persons.FirstOrDefault(p => p.Email == email);
            }

            return person;
        }
        public static Person GetPersonByLogin(string login)
        {
            Person person;
            using (var ctx = new MusicContext("MusicContext"))
            {
                person = ctx.Persons.FirstOrDefault(p => p.Login == login);
            }

            return person;
        }
        public static Sound GetSoundByMusicFileName(string musicName)
        {
            Sound sound;
            using (var ctx = new MusicContext("MusicContext"))
            {
                sound = ctx.Sounds.FirstOrDefault(s => s.MusicName == musicName);

            }

            return sound;
        }
        public static Sound GetSoundByName(string soundname)
        {

            Sound sound;
            using (var ctx = new MusicContext("MusicContext"))
            {

                sound = ctx.Sounds.FirstOrDefault(s => s.Title == soundname);
            }
            return sound;
        }



        public static Sound GetSoundById(int id)
        {

            Sound sound;
            using (var ctx = new MusicContext("MusicContext"))
            {

                sound = ctx.Sounds.FirstOrDefault(s => s.Id == id);
            }
            return sound;
        }
        public static List<Sound> FetchAllMusic()
        {
            List<Sound> sounds;

            using (var ctx = new MusicContext("MusicContext"))
            {
                sounds = ctx.Sounds.Include(s => s.PersonSounds)
                    .Include(s => s.PlayListSound).ToList();
            }

            return sounds;
        }
        public static List<Sound> FindTrackByName(string trackTitle)
        {
            List<Sound> sounds;
            using (var ctx = new MusicContext("MusicContext"))
            {
                sounds = ctx.Sounds.Include(p => p.PlayListSound)
                    .Include(s => s.SoundTags).Where(t => t.MusicName == trackTitle).ToList();
            }
            return sounds;
        }
        public static Person AutorizeConfirm(string login, string password)
        {
            Person person;

            using (var ctx = new MusicContext("MusicContext"))
            {
                person = ctx.Persons.FirstOrDefault(p => p.Login == login);
            }

            return (person != null && person?.Password == password) ? person : null;

        }

        public static string[] GetUserRoles(string login)
        {
            string[] roles = new string[1];

            using (var ctx = new MusicContext("MusicContext"))
            {
                string role = ctx.Roles.FirstOrDefault(rol => rol.Id == ctx.Persons.FirstOrDefault(p => p.Login == login).RoleId)?.Name;
                roles[0] = role;
            }

            return roles;
        }

        public static void CreatePlayList(string name, string color)
        {
            PlayList playList = new PlayList()
            {
                Name = name,
                Color = color,
                AddingDate = DateTime.Now
            };

            using (var ctx = new MusicContext("MusicContext"))
            {
                ctx.PlayLists.Add(playList);
                ctx.SaveChanges();
            }
        }

        public static PlayList GetPalyListWithMaxId()
        {
            PlayList playList;
            using (var ctx = new MusicContext("MusicContext"))
            {
                int max = ctx.PlayLists.Max(p => p.Id);
                playList = ctx.PlayLists.FirstOrDefault(p => p.Id == max);
            }

            return playList;
        }
        public static void CreatePlayListSound(int soundId, int playListId)
        {
            PlayListSound playListSound = new PlayListSound()
            {
                SoundId = soundId,
                PlayListId = playListId
            };

            using (var ctx = new MusicContext("MusicContext"))
            {
                ctx.PlayListSounds.Add(playListSound);
                ctx.SaveChanges();
            }
        }

        public static List<PlayList> GetAllPlayLists()
        {
            List<PlayList> playLists = new List<PlayList>();
            using (var ctx = new MusicContext("MusicContext"))
            {
                playLists = ctx.PlayLists.ToList();
            }

            return playLists;
        }
        public static List<PlayListSound> GetPlayListSounds(int id)
        {
            List<PlayListSound> playListSounds = new List<PlayListSound>();
            using (var ctx = new MusicContext("MusicContext"))
            {
                playListSounds = ctx.PlayListSounds.Include(p => p.Sound).Include(p => p.PlayList).Where(p => p.PlayListId == id).ToList();
            }

            return playListSounds;
        }

        public static List<PersonSound> GetAllUserSounds(int userId)
        {
            List<PersonSound> personSounds = new List<PersonSound>();
            using (var ctx = new MusicContext("MusicContext"))
            {
                personSounds = ctx.PersonSounds.Include(p => p.Sound).Include(p => p.Person).Where(p => p.Person.Id == userId).ToList();
            }

            return personSounds;
        }

        public static void CreateUserSound(int userId, int soundId)
        {
            using (var ctx = new MusicContext("MusicContext"))
            {
                PersonSound personSound = new PersonSound()
                {
                    Person = ctx.Persons.FirstOrDefault(p => p.Id == userId),
                    Sound = ctx.Sounds.FirstOrDefault(s => s.Id == soundId)
                };

                List<PersonSound> userSounds = GetAllUserSounds(userId);
                bool b = true;
                foreach (PersonSound userSound in userSounds)
                {
                    if (userSound.Sound.Id == soundId)
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    ctx.PersonSounds.Add(personSound);
                    ctx.SaveChanges();
                }
            }
        }

        public static List<PersonPlayList> GetAllUserPlayLists(int userId)
        {
            List<PersonPlayList> personPlayLists = new List<PersonPlayList>();
            using (var ctx = new MusicContext("MusicContext"))
            {
                personPlayLists = ctx.PersonPlayLists.Include(p => p.Person).Include(p => p.PlayList).Where(p => p.Person.Id == userId).ToList();
            }

            return personPlayLists;
        }

        public static void CreateUserPlayList(int userId, int playListId)
        {
            using (var ctx = new MusicContext("MusicContext"))
            {
                PersonPlayList personPlayList = new PersonPlayList()
                {
                    Person = ctx.Persons.FirstOrDefault(p => p.Id == userId),
                    PlayList = ctx.PlayLists.FirstOrDefault(s => s.Id == playListId)
                };

                List<PersonPlayList> userPlayLists = GetAllUserPlayLists(userId);
                bool b = true;
                foreach (PersonPlayList userPlayList in userPlayLists)
                {
                    if (userPlayList.PlayList.Id == playListId)
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    ctx.PersonPlayLists.Add(personPlayList);
                    ctx.SaveChanges();
                }
            }
        }

        public static void DeleteUserPlayList(int userId, int playListId)
        {

            using (var ctx = new MusicContext("MusicContext"))
            {
                PersonPlayList personPlayList = ctx.PersonPlayLists.FirstOrDefault(p => p.Person.Id == userId && p.PlayList.Id == playListId);
                ctx.Entry(personPlayList).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static void DeleteUserSound(int personSoundId)
        {

            using (var ctx = new MusicContext("MusicContext"))
            {
                PersonSound personSound = ctx.PersonSounds.FirstOrDefault(p => p.Id == personSoundId);
                ctx.Entry(personSound).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
}