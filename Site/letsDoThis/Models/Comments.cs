using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace letsDoThis.Models
{
    [Table("Comment")]
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual User Owner { get; set; }
        public virtual Post thePost { get; set; }
    }
}