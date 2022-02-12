namespace NationalExamSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Difficulty")]
    public partial class Difficulty
    {
        public Difficulty()
        {
            Questions = new List<Question>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
