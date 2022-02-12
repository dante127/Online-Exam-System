namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int CourseId { get; set; }

        [StringLength(50)]
        public string CourseName { get; set; }

        [Column(TypeName = "text")]
        public string Reference { get; set; }

        public int? TeacherId { get; set; }

        [StringLength(255)]
        public string Img { get; set; }

        [Column(TypeName = "text")]
        public string ShortDesc { get; set; }
        [Column(TypeName = "text")]
        public string LongDesc { get; set; }
        public int? CateId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
