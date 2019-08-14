using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Post
{
    public class PostTag
    {
        [Key]
        public int PostTagId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        public int PostId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
