using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text;

namespace SurveyCat.Models
{
    public static class DatabaseHelper
    {

        #region userverification
        public static User VerifyUserDetails(User userModel)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                var userDetails = db.Users.Where(x => x.Login == userModel.Login && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails != null)
                    return userDetails;
                else
                    return null;
            }
        }
        #endregion

        #region Questions
        public static List<Quesion> GetQuestions(object userId)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                List<Quesion> questions = new List<Quesion>();

                int id = Convert.ToInt32(userId);
                var userRoles = db.UserRoles.Where(r=>r.UserID== id).ToList();
                foreach (var userRole in userRoles)
                {
                    Role role = db.Roles.Find(userRole.RoleID);
                    var qId = role.name.Split('_');
                    int idq = Convert.ToInt32(qId[0]);
                    if (qId[1] == "Question")
                    {
                        var question = db.Quesions.Find(idq);
                        questions.Add(question);
                    }
                }
                //var questions = db.Quesions.ToList();
                if (questions != null)
                    return questions;
                else
                    return null;
            }
        }

        public static void DeleteQuestions(int Id)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                Quesion qs = db.Quesions.Find(Id);

                var questions = db.Quesions.Remove(qs);
                db.SaveChanges();
            }
        }

        public static void AddQuestions(Quesion question, object userId)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                db.Quesions.Add(question);

                db.SaveChanges();
                Role role = new Role();
                StringBuilder roleName = new StringBuilder();
                roleName.Append(question.QID);
                roleName.Append("_Question");
                role.name = roleName.ToString();
                db.Roles.Add(role);
                db.SaveChanges();
                UserRole uRole = new UserRole();
                uRole.RoleID = role.RoleID;
                uRole.Active = true;
                User userDetails = db.Users.Find(userId);
                if (userDetails != null)
                    uRole.UserID = userDetails.UserID;
                db.UserRoles.Add(uRole);
                db.SaveChanges();
                //QuestionProperty p = new QuestionProperty();
                //p.QID = question.QID;
                //p.Property = question.QuestionProperty.Property;
                //db.QuestionProperties.Add(p);
                //db.SaveChanges();
            }
        }

        public static Quesion EditQuestions(int Id)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                Quesion qs = db.Quesions.Find(Id);
                return qs;
            }
        }
        #endregion

        #region Survey
        public static List<Survey> GetSurveyDetails()
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                var surveyDetails = db.Surveys.ToList();
                if (surveyDetails != null)
                    return surveyDetails;
                else
                    return null;
            }
        }

        public static void AddSurveyDetails(Survey survey)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
            }
        }
        #endregion

        #region Questionnaire
        public static List<Questionnaire> GetQuestionnaire(object userId)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                List<Questionnaire> questionnaires = new List<Questionnaire>();

                int id = Convert.ToInt32(userId);
                var userRoles = db.UserRoles.Where(r => r.UserID == id).ToList();
                foreach (var userRole in userRoles)
                {
                    Role role = db.Roles.Find(userRole.RoleID);
                    var qId = role.name.Split('_');
                    int idq = Convert.ToInt32(qId[0]);
                    if (qId[1] == "Questionnaire")
                    {
                        var questionnaire = db.Questionnaires.Find(idq);
                        questionnaires.Add(questionnaire);
                    }
                }
                //for each questionnaire get quedesc qid and fill question list and add qqviewmodel
               
                if (questionnaires != null)
                    return questionnaires;
                else
                    return null;
            }
        }

        public static void AddQuestionnaire(QQViewModel qQViewModel, object userId)
        {
            using (SurveyMonkeyEntities1 db = new SurveyMonkeyEntities1())
            {
                db.Questionnaires.Add(qQViewModel.questionnaire);

                db.SaveChanges();
                int order = 1;
                foreach(Quesion questions in qQViewModel.selectedQuestions)
                {
                    QuesDesc qD = new QuesDesc();
                    qD.QID = questions.QID;
                    qD.QAID = qQViewModel.questionnaire.QAID;
                    qD.Order = (order++).ToString();
                    db.QuesDescs.Add(qD);
                    db.SaveChanges();
                }
                //get question id from question table
                //insert into QuescDesc table
                Role role = new Role();
                StringBuilder roleName = new StringBuilder();
                roleName.Append(qQViewModel.questionnaire.QAID);
                roleName.Append("_Questionnaire");
                role.name = roleName.ToString();
                db.Roles.Add(role);
                db.SaveChanges();
                UserRole uRole = new UserRole();
                uRole.RoleID = role.RoleID;
                uRole.Active = true;
                User userDetails = db.Users.Find(userId);
                if (userDetails != null)
                    uRole.UserID = userDetails.UserID;
                db.UserRoles.Add(uRole);
                db.SaveChanges();
            }
        }

        #endregion

    }

    public partial class SurveyMonkeyEntities1 : DbContext
    {
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException vex)
            {
                var exception = HandleDbEntityValidationException(vex);
                throw exception;
            }
            catch (DbUpdateException dbu)
            {
                var exception = HandleDbUpdateException(dbu);
                throw exception;
            }
        }

        //protected override void OnModelCreating(DbModelBuilder model_builder)
        //{
        //    base.OnModelCreating(model_builder);
        //    model_builder.Entity<QuestionProperty>().HasKey(t => new { t.QID, t.Property });

        //}
        private Exception HandleDbEntityValidationException(DbEntityValidationException vex)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in vex.EntityValidationErrors)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entry.Entity.GetType().Name);
                    foreach (var errormsg in result.ValidationErrors)
                    {
                        builder.AppendFormat("Type: {0} was part of the problem. {1}", errormsg.ErrorMessage, errormsg.PropertyName);
                    }
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, vex);
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }

    }
}