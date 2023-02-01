using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace letsDoThis.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        [Required, StringLength(100,ErrorMessage ="Max 100 karakter")]
        public string Title { get; set; }
        [Required, StringLength(2000, ErrorMessage = "Max 2000 karakter")]
        public string Desc { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual User owner { get; set; }
        public virtual List<Like> like { get; set; }
        public virtual List<Comments> comment { get; set; }
        public Post()
        {
            comment = new List<Comments>();
            like = new List<Like>();
        }

    }
}