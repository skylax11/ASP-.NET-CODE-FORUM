using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace letsDoThis.RegisterModel
{
    public class RegMod
    {
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(20, ErrorMessage = "Maksimum 20 karakter olmalıdır.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(20, ErrorMessage = "Maksimum 20 karakter olmalıdır.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(20, ErrorMessage = "Maksimum 20 karakter olmalıdır.")]
        public string REPassword { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(100, ErrorMessage = "Maksimum 100 karakter olmalıdır.")]
        public string email { get; set; }
    }
}