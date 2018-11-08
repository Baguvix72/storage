using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCsoftStorage.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PostContext : DbContext
    {
        public DbSet<prePost> preposts { get; set; }
    }
}