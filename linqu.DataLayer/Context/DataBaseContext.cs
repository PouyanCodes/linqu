
using linqu.DataLayer.Entities.Links;
using linqu.DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace linqu.DataLayer.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Filter Query Items

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDeleted);

            modelBuilder.Entity<Link>()
                .HasQueryFilter(l => !l.IsDeleted);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Role> Roles { get; set; }      
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        #endregion

        #region Link

        public DbSet<Link> Links { get; set; }

        #endregion

    }
}
