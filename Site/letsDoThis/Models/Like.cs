using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace letsDoThis.Models
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public virtual Post likedPost { get; set; }
        public virtual User likedUser { get; set; }
    }
}