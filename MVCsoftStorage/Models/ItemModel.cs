using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCsoftStorage.Models
{
    public class ItemModel
    {
        public ProgramModel CurrPost { get; set; }
        public List<ProgramModel> SimilarPost { get; set; }
    }
}