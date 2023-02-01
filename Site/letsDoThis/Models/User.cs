using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace letsDoThis.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(20,ErrorMessage ="Maksimum 20 karakter olmalıdır.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(20,ErrorMessage = "Maksimum 20 karakter olmalıdır.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), StringLength(100, ErrorMessage = "Maksimum 100 karakter olmalıdır.")]
        public string email { get; set; }
        public Guid ActivateGuid { get; set; }
        public string ProfileImage { get; set; }
        public bool isAdmin { get; set; }
        public bool isActivate{ get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual List<Post> post { get; set; }
        public virtual List<Comments> comment { get; set; }
        public virtual List<Like> like { get; set; }
        public User()
        {
            post = new List<Post>();
            comment = new List<Comments>();
            like = new List<Like>();

        }

    }
}