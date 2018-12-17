using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Models
{
    public class ProgramModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Id { get; set; }
        public string Poster { get; set; }
        public List<spoilers> Spoilers { get; set; }
        public List<images> Imgs { get; set; }
        public programs Program { get; set; }
        public DateTime DatePublic { get; set; }
    }
}