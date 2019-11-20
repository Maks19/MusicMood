using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class MusicContextInitializer : 
        DropCreateDatabaseAlways<LibraryServices.MusicContext>
    {
        protected override void Seed(MusicContext context)
        {
            var roles = new List<Role>()
            {
                new Role(){Id = 1, Name = "admin"},
                new Role(){Id = 2,Name = "user"}
            };
            roles.ForEach(role => context.Roles.Add(role));
            context.SaveChanges();

            var person = new Person()
            {
                Id = 1,
                FirstName = "Sasha",
                SecondName = "Kovalov",
                Login = "alexmars232",
                Email = "oleksandr.kovalov@nure.ua",
                Password = BusinessLogic.CryptographyMD5("123456"),
                DateOfBirth = new DateTime(1999,12,8),
                RoleId = 1
            };

            context.Persons.Add(person);
            context.SaveChanges();
        }
    }
}
