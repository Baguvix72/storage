using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Class
{
    public class ItemContent
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<spoilers> Spoilers { get; set; }
        public List<images> Poster { get; set; }
        public programs Program { get; set; }
        public DateTime DatePublic { get; set; }
    }
}