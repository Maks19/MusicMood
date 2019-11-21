using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class PlayListSound
    {
        [Key]
        public int Id { get; set; }
        public int SoundId { get; set; }
        public virtual Sound Sound { get; set; }
        public int PlayListId { get; set; }
        public virtual PlayList PlayList { get; set; }
    }
}
