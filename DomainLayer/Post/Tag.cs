using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainLayer.Post
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        public string TagName { get; set; }
    }
}
