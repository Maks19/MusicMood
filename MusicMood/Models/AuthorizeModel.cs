using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicMood.Models
{
    public class AuthorizeModel
    {
        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}