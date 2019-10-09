using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class PlayListTag
    {
        [Key]
        public int Id { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual PlayList PlayList { get; set; }
    }
}
