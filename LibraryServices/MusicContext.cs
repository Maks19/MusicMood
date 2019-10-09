using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class MusicContext : DbContext
    {
        public MusicContext(string nameOrConnectionString) 
            : base(nameOrConnectionString) 
        { }
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<Sound> Sounds { get; set; }
        public IDbSet<PlayList> PlayLists { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<PersonSound> PersonSounds { get; set; }
        public IDbSet<PersonPlayList> PersonPlayLists { get; set; }
        public IDbSet<SoundTag> SoundTags { get; set; }
        public IDbSet<PlayListSound> PlayListSounds { get; set; }
        public IDbSet<PlayListTag> PlayListTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonSound>().HasKey(p => p.Id);
        }

    }
}
