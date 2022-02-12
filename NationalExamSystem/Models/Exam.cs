namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exam")]
    public partial class Exam
    {
        public Exam()
        {
            Choosen_Question = new List<Choosen_Question>();
            Students = new List<Student>();
            StudentExams = new List<StudentExam>();
        }

        public int ExamId { get; set; }

        [StringLength(50)]
        public string ExamName { get; set; }

        public int? Period { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartTime { get; set; }

        public bool? IsActive { get; set; }

        public virtual List<Choosen_Question> Choosen_Question { get; set; }

        public virtual List<Student> Students { get; set; }

        public virtual List<StudentExam> StudentExams { get; set; }
    }
}
