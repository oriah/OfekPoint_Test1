using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sisma.Project1.DAL.Data
{
    public class SismaContext : DbContext
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentInClass> StudentInClasses { get; set; }

        public SismaContext(DbContextOptions<SismaContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new StudentInClassConfiguration());

            //modelBuilder.Entity<StudentInClass>().HasKey(vf => new { vf.StudentId, vf.ClassId });
            //modelBuilder.Entity<StudentInClass>()
            //    .HasRequired(c => c.Stage)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Side>()
            //    .HasRequired(s => s.Stage)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            this.Seed(modelBuilder);
        }


        private void Seed(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<School>().HasData(
            //    new School { SchoolId = Guid.NewGuid()},
            //    new School { SchoolId = Guid.NewGuid() },
            //    new School { SchoolId = Guid.NewGuid() }
            //);
        }
    }

    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasIndex("RefId").IsUnique();
            builder.HasMany(m => m.Classes).WithOne(item => item.School);
            builder.HasMany(m => m.Students).WithOne(item => item.School);
        }
    }
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasIndex("RefId").IsUnique();
            builder.HasMany(m => m.StudentInClasses).WithOne(item => item.Class).OnDelete(DeleteBehavior.NoAction);
        }
    }


    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasIndex("RefId").IsUnique();
            builder.HasMany(m => m.StudentInClasses).WithOne(item => item.Student).OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class StudentInClassConfiguration : IEntityTypeConfiguration<StudentInClass>
    {
        public void Configure(EntityTypeBuilder<StudentInClass> builder)
        {
            builder.HasIndex("ClassId", "StudentId").IsUnique();
            builder.HasIndex("RefId").IsUnique();
            builder.HasOne(m => m.Class).WithMany(item => item.StudentInClasses).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(m => m.Student).WithMany(item => item.StudentInClasses).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
