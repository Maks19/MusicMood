﻿using System;
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
        public static IEnumerable<Sound> FetchAllMusic()
        {
            return null;

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
    }
}