using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Post
{
    public class PostGroup
    {
        [Key]
        public int PostGroupId { get; set; }

        public string GroupName { get; set; }

        [ForeignKey("PostGroupId")]
        public int? ParentId { get; set; }
        public virtual PostGroup Parent { get; set; }
    }
}
