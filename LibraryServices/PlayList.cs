using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class PlayList
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [StringLength(15, MinimumLength = 3,ErrorMessage ="Название может содержать от 3х до 15ти символов")]
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime AddingDate { get; set; }
        public virtual ICollection<PersonPlayList> PersonPlayLists { get; set; }
        public virtual ICollection<PlayListSound> PlayListSounds { get; set; }
        public virtual ICollection<PlayListTag> PlayListTag { get; set; }
    }
}
