using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryServices
{
    public class Sound: IComparable
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string MusicName { get; set; }
        public string ImgName { get; set; }
        public virtual ICollection<PersonSound> PersonSounds { get; set; }
        public virtual ICollection<PlayListSound> PlayListSound { get; set; }
        public virtual ICollection<SoundTag> SoundTags { get; set; }

        public int CompareTo(object obj)
        {
            Sound sound = obj as Sound;

            return this.Id.CompareTo(sound.Id);
        }
    }
}
