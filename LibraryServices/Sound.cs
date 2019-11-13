using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class Sound
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Player { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<PersonSound> PersonSounds { get; set; }
        public virtual ICollection<PlayListSound> PlayListSound { get; set; }
        public virtual ICollection<SoundTag> SoundTags { get; set; }
    }
}
