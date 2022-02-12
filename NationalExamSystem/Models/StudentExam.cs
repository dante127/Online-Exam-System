namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentExam")]
    public partial class StudentExam
    {
        public int Id { get; set; }

        public int? ExamId { get; set; }

        public int? StudentId { get; set; }

        public double? Mark { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual Student Student { get; set; }
    }
}
