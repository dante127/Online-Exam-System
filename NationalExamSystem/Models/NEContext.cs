namespace NationalExamSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NEContext : DbContext
    {
        public NEContext()
            : base("name=NEContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Choosen_Question> Choosen_Question { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.Img)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CateId);

            modelBuilder.Entity<Course>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Reference)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Img)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.ShortDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.LongDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.ExamName)
                .IsUnicode(false);

            modelBuilder.Entity<Option>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Question1)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.QuestionFile)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Img)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.NationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.InvoiceImage)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.InvoiceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Specialization)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.Img)
                .IsUnicode(false);


        }
    }
}
