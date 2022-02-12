using NationalExamSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalExamSystem.Controllers
{
    public class ExamStartController : Controller
    {
        NEContext db = new NEContext();
        // GET: ExamStart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Exam(Question q = null)
        {
            if(Session["Login"]==null)
            {
                return RedirectToAction("Login", "home");
            }
            else
            {
                var e = db.Choosen_Question.ToList();
                ViewBag.e = e;
                if (q != null)
                {
                    List<Option> ops = db.Options.Where(s => s.QuestionId == q.Id).ToList();
                    ViewBag.ops = ops;
                    return View(q);
                }
                else
                return View();
            }
        }
        public ActionResult next(int id)
        {
            // Question q = db.Questions.Where(s=>s.Id==id).FirstOrDefault();
            Question q = db.Choosen_Question.Where(s => s.questionId == id).Select(aq => aq.Question).FirstOrDefault();
            return RedirectToAction("Exam",q);
        }

        static List<QuestionModel> list = new List<QuestionModel>();
        static double mark = 0;
        int flag = 0;
        
        public ActionResult postans(int quesid,string option , int OptionId)
        {
          
            var correct = db.Choosen_Question.Where(s => s.questionId == quesid).Select(a => new
            {
                a.Question,
                a.Question.Mark,
                opId = a.Question.Options.Select(z => z.OptionId),
                ans = a.Question.Options.Where(q => q.CorrectAnswer == true).Select(x => x.Answer).FirstOrDefault()
            }).FirstOrDefault();

            foreach(var i in list)
            {
                if(i.qid== quesid)
                {
                    flag = 1;
                    i.option = OptionId;
                    i.opans = option;
                    if (i.opans == correct.ans)
                    {
                        i.mark = (double)correct.Mark;
                    }
                    else
                    {
                        i.mark = 0;
                    }
                }
            }
             if (flag == 0)
            {
                QuestionModel ql = new QuestionModel();
                ql.qid = quesid;
                ql.question = db.Choosen_Question.Where(s => s.questionId == quesid).Select(d => d.Question.Question1).FirstOrDefault();
                ql.option = OptionId;

                ql.opans = option;

                if (ql.opans == correct.ans)
                {
                    ql.mark = (double)correct.Mark;
                }
                else
                    ql.mark = 0;
                list.Add(ql);
            }

            mark = 0;
            foreach (var i in list)
            {
                mark += i.mark;
            }

            Session["mark"] = mark;

           //show the user's answers
            if(QuestionModel.dic.ContainsKey(quesid))
            {
                QuestionModel.dic[quesid] = option;
            }
            else
            {
                QuestionModel.dic.Add(quesid, option);
            }

            return RedirectToAction("Exam");
        }


        public ActionResult EndExam()
        {
            string StuEmail = Session["Login"].ToString();
            string Mark = "0";
            if (Session["mark"] != null)
            {
                Mark = Session["mark"].ToString();
            }
                Student stu = db.Students.Where(a => a.Email.Equals(StuEmail)).FirstOrDefault();
                StudentExam se = new StudentExam();
                se.Student = stu;
                se.Exam = db.Exams.Where(s => s.ExamId == stu.ExamId).FirstOrDefault();

                se.Mark = Convert.ToDouble(Mark);
                db.StudentExams.Add(se);
                db.SaveChanges();
           
            return RedirectToAction("UserProfile","home",new { email= StuEmail });
        }

    }
}