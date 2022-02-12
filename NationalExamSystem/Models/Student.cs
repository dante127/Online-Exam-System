namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int StudentId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]

        [Column(TypeName = "date")]
        public DateTime? birthdate { get; set; }
        [Required(ErrorMessage = "*")]

        [StringLength(50)]
        public string phone { get; set; }
        [Required(ErrorMessage = "*")]

        [StringLength(50)]
        public string NationNumber { get; set; }
        [Required(ErrorMessage = "*")]

        [StringLength(255)]
        public string InvoiceImage { get; set; }
        [Required(ErrorMessage = "*")]

        [StringLength(100)]
        public string InvoiceNumber { get; set; }
        [Required(ErrorMessage = "*")]

        [StringLength(255)]
        public string Email { get; set; }

        public int? ExamId { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
