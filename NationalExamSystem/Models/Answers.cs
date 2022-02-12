using Newtonsoft.Json.Linq;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalExamSystem.Models
{
    public class Answers
    {
        public int quesid { get; set; }
        public string option { get; set; }
        public int OptionId { get; set; }
    }
}