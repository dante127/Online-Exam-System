using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;
using System.IO;
using NationalExamSystem.Models;
using System.Web.UI;

namespace NationalExamSystem.Controllers
{
    public class ExportController : Controller
    {

        private NEContext db = new NEContext();
        // GET: Home
        public ActionResult Index()
        {

            List<Question> ch = db.Questions.ToList();
            foreach(var i in ch)
            {
                i.Options = db.Options.Where(s => s.QuestionId == i.Id).ToList();
                
            }

            return View(ch);
        }

      
        [ValidateInput(false)]
        public EmptyResult Export()
        {
            List<Question> ch = db.Questions.ToList();
            foreach (var i in ch)
            {
                i.Options = db.Options.Where(s => s.QuestionId == i.Id).ToList();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Grid.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
          //  Response.Output.Write(GridHtml);
            Response.Output.Write(ch);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }


    }
}