using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.User
{
    class Marketer
    {
        public int Id { set; get; }
        [StringLength(450)]
        public string UserId { set; get; }
        [ForeignKey("UserId")]
        public virtual User User { set; get; }



    }
}
