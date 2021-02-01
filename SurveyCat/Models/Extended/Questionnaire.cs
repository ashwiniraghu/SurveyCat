using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyCat.Models
{
    public partial class Questionnaire
    {

    }

    public class QuestionnaireMetadata
    {
        [Display(Name = "Questionnaire")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter valid Questionnaire name")]
        public string Name { get; set; }

        [Display(Name = "comments")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter valid Questionnaire comments")]
        public string Comment { get; set; }
    }
}