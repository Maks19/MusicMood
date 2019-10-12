using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

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
                DateOfBirth = dateOfBirth
            };
            using (var ctx = new MusicContext("MusicContext"))
            {
                ctx.Persons.Add(person);
                ctx.SaveChanges();
            }
        }

        public static void UpdatePassword(Person user) {

            using (var ctx = new MusicContext("MusicContext"))

            {
                ctx.Persons.Attach(user);
                var entry = ctx.Entry(user);
                entry.Property(x => x.Password).IsModified = true;
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }
        public static Person PersonEmailExists(string email)
        {
            Person person;
            using (var ctx = new MusicContext("MusicContext"))
            {
                person = ctx.Persons.FirstOrDefault(p => p.Email == email);
            }

            return person;
        }
        public static Person PersonLoginExists(string login)
        {
            Person person;
            using (var ctx = new MusicContext("MusicContext"))
            {
                person = ctx.Persons.FirstOrDefault(p => p.Login == login);
            }

            return person;
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
    }
}