namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Courses = new List<Course>();
            Questions = new List<Question>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]

        public string Img { get; set; }

        public virtual List<Course> Courses { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
