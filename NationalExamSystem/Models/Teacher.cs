namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new List<Course>();
            Questions = new List<Question>();
        }

        public int TeacherId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthdate { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Specialization { get; set; }

        [StringLength(255)]
        public string Img { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public virtual List<Course> Courses { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
