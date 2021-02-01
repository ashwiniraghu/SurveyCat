using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyCat.Models;

namespace SurveyCat.Controllers
{
    public class MySurveyController : Controller
    {
        private SurveyMonkeyEntities1 db;
        public MySurveyController()
        {
            db = new SurveyMonkeyEntities1();
        }
        // GET: MySurvey
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                List<Survey> surveyDetails = DatabaseHelper.GetSurveyDetails();

                return View(surveyDetails);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult AddSurvey()
        {
            //var questionarie = db.Questionnaires.ToList();
            //ViewBag.Questionaires = new SelectList(questionarie, "QAID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddSurvey(SurveyViewModel surveyViewModel)
        {
            if (ModelState.IsValid)
            {
                DatabaseHelper.AddSurveyDetails(surveyViewModel.survey);
                return RedirectToAction("Index");
            }
            return View(surveyViewModel);
        }


        public JsonResult AddParticipant(SurveyViewModel surveyViewModel)
        {
            surveyViewModel.Userslist.Add(surveyViewModel.User);
            //insert into user table
            //insert into role and userrole and roleperm
            //display added user in table 
            ViewBag.userList = surveyViewModel.Userslist;
            return Json(surveyViewModel);
        }

        public ActionResult ShowContacts(SurveyViewModel surveyViewModel)
        {
            //surveyViewModel.Userslist.Add(surveyViewModel.User);
            //insert into user table
            //insert into role and userrole and roleperm
            //display added user in table 
            ViewBag.userList = surveyViewModel.Userslist;

            return PartialView("PartialContacts");
        }

        public JsonResult AddQuestionnairetoSurvey(int id)
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel();
            //surveyViewModel.Userslist.Add(surveyViewModel.User);
            //insert into user table
            //insert into role and userrole and roleperm
            //display added user in table 
            var questionnaire = db.Questionnaires.Find(id);
            surveyViewModel.questionnaires.Add(questionnaire);
            return Json(surveyViewModel);
            //return PartialView("AddQuestionnairetoSurvey");
        }

        public JsonResult GetSearchValue(string search)
        {
            var allsearch1 = db.Questionnaires.Where(x => x.Name.Contains(search)).Select(x => new
            {
                QAID = x.QAID,
                Name = x.Name,
            });

            List<Questionnaire> allsearch = allsearch1.ToList().Select(r => new Questionnaire
            {
                QAID = r.QAID,
                Name = r.Name,
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Autocomplete(string term)
        {
            //var result = new List<KeyValuePair<string, string>>();

            //IList<SelectListItem> List = new List<SelectListItem>();
            //List.Add(new SelectListItem { Text = "test1", Value = "0" });
            //List.Add(new SelectListItem { Text = "test2", Value = "1" });
            //List.Add(new SelectListItem { Text = "test3", Value = "2" });
            //List.Add(new SelectListItem { Text = "test4", Value = "3" });

            //foreach (var item in List)
            //{

            //    result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));


            //}
            //var result3 = result.Where(s => s.Value.ToLower().Contains(term.ToLower())).Select(w => w).ToList();
            var allsearch1 = db.Questionnaires.Where(x => x.Name.Contains(term)).Select(x => new
            {
                QAID = x.QAID,
                Name = x.Name,
            });

            List<Questionnaire> result3 = allsearch1.ToList().Select(r => new Questionnaire
            {
                QAID = r.QAID,
                Name = r.Name,
            }).ToList();
            return Json(result3, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDetail(int id)
        {
            SurveyViewModel model = new SurveyViewModel();
            // select data by id here display static data;
            //IList<DemoModel> model = new List<DemoModel>();
            //model.Add(new DemoModel { id=1, name = "test1", mobile = "0" });
            //model.Add(new DemoModel { id = 1, name = "Yogesh", mobile = "0" });
            //model.Add(new DemoModel { id = 1, name = "Pratham", mobile = "0" });
            //model.Add(new DemoModel { id = 1, name = "Tyagi", mobile = "0" });
          
            var questionnaire = db.Questionnaires.Find(id);
            model.questionnaires.Add(questionnaire);
            //return Json(surveyViewModel);
            //if (id == 0)
            //{
            //model.id = 1;
            //model.name = "Yogesh Tyagi";
            //model.mobile = "9460516787";

            //}
            //else
            //{
            //    model.id = 2;
            //    model.name = "Pratham Tyagi";
            //    model.mobile = "9460516787";

            //}

            return Json(model);

        }
    }
}