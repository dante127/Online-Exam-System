using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalExamSystem.Models
{
    public  class QuestionModel
    {
        public int qid { get; set; }
        public string question { get; set; }
        public List<Option> Options { get; set; }
        public string opans { get; set; }
        public double mark { get; set; }
        public int option { get; set; }

        public static List<string> chs = new List<string>();

        public static Dictionary<int, string> dic =
                        new Dictionary<int, string>();
        

    }





}