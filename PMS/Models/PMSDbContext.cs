using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PMS.Models
{
    public partial class PMSDbContext : DbContext
    {
        public PMSDbContext()
        {
        }

        public PMSDbContext(DbContextOptions<PMSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdvanceSalary> TblAdvanceSalaries { get; set; }
        public virtual DbSet<TblBaseSalary> TblBaseSalaries { get; set; }
        public virtual DbSet<TblDepartment> TblDepartments { get; set; }
        public virtual DbSet<TblElectricalSafety> TblElectricalSafeties { get; set; }
        public virtual DbSet<TblMenu> TblMenus { get; set; }
        public virtual DbSet<TblMenuRole> TblMenuRoles { get; set; }
        public virtual DbSet<TblNightShift> TblNightShifts { get; set; }
        public virtual DbSet<TblOvertime> TblOvertimes { get; set; }
        public virtual DbSet<TblPhoneInfo> TblPhoneInfos { get; set; }
        public virtual DbSet<TblPhoneLimit> TblPhoneLimits { get; set; }
        public virtual DbSet<TblPosition> TblPositions { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblTimeKeeping> TblTimeKeepings { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserDepartment> TblUserDepartments { get; set; }
        public virtual DbSet<TblUserPosition> TblUserPositions { get; set; }
        public virtual DbSet<TblUserRole> TblUserRoles { get; set; }
        public virtual DbSet<TblWorkPerformance> TblWorkPerformances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=118.70.182.193;Database=PMS;User Id=PMS; Password = PMS@123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAdvanceSalary>(entity =>
            {
                entity.ToTable("tblAdvanceSalary");

                entity.Property(e => e.BackCharge).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.BackPay).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblAdvanceSalaries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAdvanceSalary_User");
            });

            modelBuilder.Entity<TblBaseSalary>(entity =>
            {
                entity.ToTable("tblBaseSalary");

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.ToTable("tblDepartment");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblElectricalSafety>(entity =>
            {
                entity.ToTable("tblElectricalSafety");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Kcvi).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Ki).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Knqi).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TotalDay).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblElectricalSafeties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblElectricalSafety_User");
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.ToTable("tblMenu");

                entity.Property(e => e.IconUrl).HasMaxLength(150);

                entity.Property(e => e.MetaTitle).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ParentId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TblMenuRole>(entity =>
            {
                entity.ToTable("tblMenuRole");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.TblMenuRoles)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMenuRole_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblMenuRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMenuRole_Role");
            });

            modelBuilder.Entity<TblNightShift>(entity =>
            {
                entity.ToTable("tblNightShift");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblNightShifts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblNightShift_User");
            });

            modelBuilder.Entity<TblOvertime>(entity =>
            {
                entity.ToTable("tblOvertime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOvertimes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOvertime_User");
            });

            modelBuilder.Entity<TblPhoneInfo>(entity =>
            {
                entity.ToTable("tblPhoneInfo");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblPhoneInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPhoneInfo_User");
            });

            modelBuilder.Entity<TblPhoneLimit>(entity =>
            {
                entity.ToTable("tblPhoneLimit");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.Posion)
                    .WithMany(p => p.TblPhoneLimits)
                    .HasForeignKey(d => d.PosionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPhoneLimit_Position");
            });

            modelBuilder.Entity<TblPosition>(entity =>
            {
                entity.ToTable("tblPosition");

                entity.Property(e => e.BasicPosiontConfficient).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PosiontConfficient).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ResponsibilityCoefficient).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.ToTable("tblRole");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblTimeKeeping>(entity =>
            {
                entity.ToTable("tblTimeKeeping");

                entity.Property(e => e.Cd)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CD");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.H).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Kl)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("KL");

                entity.Property(e => e.L).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Lv)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("LV");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ots)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("OTS");

                entity.Property(e => e.P).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblTimeKeepings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTimeKeeping_User");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.AccountName).HasMaxLength(100);

                entity.Property(e => e.BankNo).HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmploymentDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.SalaryConfficient).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TblUserDepartment>(entity =>
            {
                entity.ToTable("tblUserDepartment");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OrderNumber).HasDefaultValueSql("((-1))");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblUserDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserDepartment_Department");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserDepartments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserDepartment_User");
            });

            modelBuilder.Entity<TblUserPosition>(entity =>
            {
                entity.ToTable("tblUserPosition");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OrderNumber).HasDefaultValueSql("((-1))");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.TblUserPositions)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserPosition_Position");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserPositions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserPosition_User");
            });

            modelBuilder.Entity<TblUserRole>(entity =>
            {
                entity.ToTable("tblUserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserRole_User");
            });

            modelBuilder.Entity<TblWorkPerformance>(entity =>
            {
                entity.ToTable("tblWorkPerformance");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Kdhi).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Kncdi).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Kti).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUser)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Ndhi).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Nncdi).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Nti).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblWorkPerformances)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblWorkPerformance_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
