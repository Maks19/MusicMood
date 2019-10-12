using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class MusicContextInitializer : 
        CreateDatabaseIfNotExists<LibraryServices.MusicContext>
    {
        protected override void Seed(MusicContext context)
        {
            var roles = new List<Role>()
            {
                new Role(){Id = 1, Name = "Admin"},
                new Role(){Id = 2,Name = "User"}
            };
            roles.ForEach(role => context.Roles.Add(role));
            context.SaveChanges();
        }
    }
}
