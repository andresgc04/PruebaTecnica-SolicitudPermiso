using System;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data
{
    public partial class PermisosDBContext:DbContext
    {
        public PermisosDBContext()
        {
        }

        public PermisosDBContext(DbContextOptions<PermisosDBContext>options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=148.0.107.181; Database=PermisosDB; user id=AndresGc; password= Ag%04071997;");
            }
        }
    }
}
