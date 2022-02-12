namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public Question()
        {
            Choosen_Question = new List<Choosen_Question>();
            Options = new List<Option>();
        }

        public int Id { get; set; }

        [Column("Question", TypeName = "nvarchar")]
        [Display(Name ="Question")]
        [Required(ErrorMessage ="*")]
        public string Question1 { get; set; }


        [StringLength(255)]
        public string QuestionFile { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "*")]

        public string Img { get; set; }

        public double? Mark { get; set; }

        public int? CategoryId { get; set; }

        public int? DifficultyId { get; set; }

        public int? TeacherId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Choosen_Question> Choosen_Question { get; set; }

        public virtual Difficulty Difficulty { get; set; }

        public virtual List<Option> Options { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
