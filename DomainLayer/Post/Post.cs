using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DomainLayer.Post
{
    public class Post
    {

        public Post()
        {
            Tags = new List<PostTag>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public string PostType { get; set; }

        public int PostGroupId { get; set; }
        [ForeignKey("PostGroupId")]
        public virtual PostGroup PostGroup { get; set; }

        public virtual IList<PostTag> Tags { get; set; }
    }
}
