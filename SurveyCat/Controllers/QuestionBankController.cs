using SurveyCat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyCat.Controllers
{
    public class QuestionBankController : Controller
    {
        private SurveyMonkeyEntities1 db;
        public QuestionBankController()
        {
            db = new SurveyMonkeyEntities1();
        }
        
        // GET: QuestionBank
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                List<Quesion> questions = DatabaseHelper.GetQuestions(Session["UserID"]);
                
                return View(questions);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult AddQuestion()
        {
            //SurveyMonkeyEntities db = new SurveyMonkeyEntities();
            var questionType = db.QuestionTypes.ToList();
            ViewBag.QuestionType = new SelectList(questionType, "QTypeID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult SaveQuestion(Quesion question)
        {
            var questionType = db.QuestionTypes.ToList();
            ViewBag.QuestionType = new SelectList(questionType, "QTypeID", "Name");

            if (ModelState.IsValid)
            {
                //Quesion qs = new Quesion();
                //qs.QText = question.QText;
                //qs.QTypeID = question.QTypeID;
                //if(question.QuestionProperty.Property )
                DatabaseHelper.AddQuestions(question, Session["UserID"]);
                return View("AddQuestion");
            }
            //List<Quesion> questions = db.Quesions.ToList();
           
            return View("Index");
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesion questions = db.Quesions.Find(Id);
            var questionType = db.QuestionTypes.ToList();
            ViewBag.QuestionType = new SelectList(questionType, "QTypeID", "Name");
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        [HttpPost]
        public ActionResult Edit(Quesion question)
        {
            var questionType = db.QuestionTypes.ToList();
            ViewBag.QuestionType = new SelectList(questionType, "QTypeID", "Name");

            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry(question.QuestionProperty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        public ActionResult Delete(int Id)
        {
            //DatabaseHelper.DeleteQuestions(Id);
            //List<Quesion> questions = DatabaseHelper.GetQuestions();

            return View("Index");
        }
    }
}