using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicMood.Models
{
    public class RegisterModel
    {
        [Display(Name = "Имя")]
        [StringLength(12, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [StringLength(12, MinimumLength = 3)]
        [Required]
        public string SecondName { get; set; }
        [Display(Name = "Логин")]
        [StringLength(12, MinimumLength = 3)]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9_\\.\\+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-\\.]+")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(15,MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}