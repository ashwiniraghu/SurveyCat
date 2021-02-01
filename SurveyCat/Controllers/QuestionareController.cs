using SurveyCat.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SurveyCat.Controllers
{
    public class QuestionareController : Controller
    {
        private SurveyMonkeyEntities1 db;
        public QuestionareController()
        {
            db = new SurveyMonkeyEntities1();
        }
        // GET: Questionare
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                List<Questionnaire> questionnaire = DatabaseHelper.GetQuestionnaire(Session["UserID"]);

                return View(questionnaire);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult AddQuestionnaire()
        {
            //get all questions for current user and fill grid view
            QQViewModel model = new QQViewModel();

            if (Session["UserID"] != null)
            {
                IList<Quesion> allQuestions = DatabaseHelper.GetQuestions(Session["UserID"]);
                IList<Quesion> selectedQuestions = new List<Quesion>();
                model.questions = allQuestions;
                model.selectedQuestions = selectedQuestions;
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult Assign([Bind]QQViewModel model)
        {
            // if (ModelState.IsValid)
            {
                DatabaseHelper.AddQuestionnaire(model, Session["UserID"]);
            }
            return View("AddQuestionnaire", model);
        }


        public ActionResult SelectQuestions(string[] ids, [Bind]QQViewModel model)
        {
            int[] id = null;
            //if(model.selectedQuestions != null)
            //    model = new QQViewModel();
            IList<Quesion> questions = new List<Quesion>();
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }
            }
            if (id != null && id.Length > 0)
            {
                foreach (int i in id)
                {
                    var question = db.Quesions.Find(i);
                    if (model.selectedQuestions == null)
                    {
                        model.selectedQuestions = new List<Quesion>();
                        model.selectedQuestions.Add(question);
                    }
                    else if (!model.selectedQuestions.Contains(question, new Comparer()))
                        model.selectedQuestions.Add(question);
                }

            }
            IList<Quesion> allQuestions = DatabaseHelper.GetQuestions(Session["UserID"]);
            model.questions = allQuestions;
            return View("AddQuestionnaire", model);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QQViewModel qQViewModel = new QQViewModel();
            Questionnaire questionnaire = db.Questionnaires.Find(Id);
            qQViewModel.questionnaire.QAID = questionnaire.QAID;
            qQViewModel.questionnaire.Name = questionnaire.Name;
            qQViewModel.questionnaire.Comment = questionnaire.Comment;
            foreach (QuesDesc id in questionnaire.QuesDescs)
            {
                Quesion question = db.Quesions.Find(id.QID);
                qQViewModel.selectedQuestions.Add(question);
            }
            IList<Quesion> allQuestions = DatabaseHelper.GetQuestions(Session["UserID"]);
            qQViewModel.questions = allQuestions;

            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(qQViewModel);
        }

        [HttpPost]
        public ActionResult Save(QQViewModel qQViewModel)
        {
            if (ModelState.IsValid)
            {

                //db.Entry(qQViewModel.questionnaire).State = EntityState.Modified;
                //db.SaveChanges();
                //foreach(Quesion qs in qQViewModel.selectedQuestions)
                //{
                //    Quesion qsTemp = db.Quesions.Find(qs.QID);
                //    qs.QTypeID = qsTemp.QTypeID;
                //    db.Entry(qs).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
                //return RedirectToAction("Index");
            }
            return View(qQViewModel);
        }

        [HttpPost]
        public ActionResult SelectEditQuestions(QQViewModel model)
        {
            //    int[] id = null;
            //    IList<Quesion> questions = new List<Quesion>();
            //    if (ids != null)
            //    {
            //        id = new int[ids.Length];
            //        int j = 0;
            //        foreach (string i in ids)
            //        {
            //            int.TryParse(i, out id[j++]);
            //        }
            //    }
            //    if (id != null && id.Length > 0)
            //    {
            //        foreach (int i in id)
            //        {
            //            var question = db.Quesions.Find(i);
            //            if (model.selectedQuestions == null)
            //            {
            //                model.selectedQuestions = new List<Quesion>();
            //                model.selectedQuestions.Add(question);
            //            }
            //            else if (!model.selectedQuestions.Contains(question, new Comparer()))
            //                model.selectedQuestions.Add(question);
            //        }

            //    }
            //    IList<Quesion> allQuestions = DatabaseHelper.GetQuestions(Session["UserID"]);
            //    model.questions = allQuestions;
            return View(model);
        }

    }

    public class Comparer : IEqualityComparer<Quesion>
    {
        public bool Equals(Quesion x, Quesion y)
        {
            return x.QID == y.QID;
        }

        public int GetHashCode(Quesion obj)
        {
            return obj.GetHashCode();
        }
    }

}