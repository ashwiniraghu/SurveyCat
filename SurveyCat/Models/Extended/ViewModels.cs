using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyCat.Models
{
    public class ViewModels
    {
    }
    public class QQViewModel
    {
        public QQViewModel()
        {
            this.questionnaire = new Questionnaire();
            this.selectedQuestions = new List<Quesion>();
            this.questions=new List<Quesion>();
        }
        public Questionnaire questionnaire { get; set; }

        public IList<Quesion> questions { get; set; }

        public IList<Quesion> selectedQuestions { get; set; }
    }

    public class SurveyViewModel
    {
        public SurveyViewModel()
        {
            this.survey = new Survey();
            this.questionnaire = new Questionnaire();
            this.User = new User();
            this.Userslist = new List<User>();
            this.questionnaires = new List<Questionnaire>();
        }
        //public class DemoModel
        //{
            public int id { get; set; }
           public string name { get; set; }
           public string mobile { get; set; }
        //}
        public Survey survey { get; set; }

        public Questionnaire questionnaire { get; set; }

        public User User { get; set; }

        public IList<Questionnaire> questionnaires { get; set; }

        public IList<User> Userslist { get; set; }

    }
}