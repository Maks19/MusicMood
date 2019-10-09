using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class SoundTag
    {
        [Key]
        public int Id { get; set; }
        public virtual Sound Sound { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
