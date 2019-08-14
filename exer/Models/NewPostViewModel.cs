using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
    public class NewPostViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public FileStream PostImage { get; set; }
        public string Context { get; set; }
    }
}
