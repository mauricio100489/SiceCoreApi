using Microsoft.EntityFrameworkCore;
using SiceCoreApi.Models.Security;

namespace SiceCoreApi.Context
{
    public class SICECoreContext : DbContext
    {
        public SICECoreContext()
        {
        }

        public SICECoreContext(DbContextOptions<SICECoreContext> options)
            : base(options)
        {
        }

        #region DbSet_Menu
        public virtual DbSet<UvwMenu> UvwMenus { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity_Menu
            modelBuilder.Entity<UvwMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("uvw_Menu");

                entity.Property(e => e.OptionIcon)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OptionIconColor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OptionName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OptionRoute)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OptionType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });
            #endregion
        }
    }
}
