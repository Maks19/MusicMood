using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SoundTag> SoundTags { get; set; }
        public virtual ICollection<PlayListTag> PlayListTags { get; set; }
    }
}
