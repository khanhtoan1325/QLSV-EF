using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace lab4.khanhtoan
{
    public partial class StudenDBContext : DbContext
    {
        public StudenDBContext()
            : base("name=StudenDBContext")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
