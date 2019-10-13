using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        [StringLength(12,MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [StringLength(12, MinimumLength = 3)]
        [Required]
        public string SecondName { get; set; }
        [Display(Name = "Логин")]
        [StringLength(12, MinimumLength = 3)]
        [Required]
        [RegularExpression("[a-zA-Z0-9_\\.]+",ErrorMessage = "Поле содержит недопустимые символы")]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<PersonSound> PersonSounds { get; set; }
        public virtual ICollection<PersonPlayList> PersonPlayLists { get; set; }
    }
}
