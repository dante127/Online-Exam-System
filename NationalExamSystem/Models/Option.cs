namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Option")]
    public partial class Option
    {
        public int OptionId { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Answer { get; set; }

        public bool? CorrectAnswer { get; set; }

        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
