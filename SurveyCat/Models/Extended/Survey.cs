using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyCat.Models
{
    [MetadataType(typeof(SurveyMetadata))]
    public partial class Survey
    {
    }

    public class SurveyMetadata
    {
        [Display(Name ="Id")]
        public int SID { get; set; }
        [Display(Name = "Survey Name")]
        public string name { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:MM/dd/yyy}")]
        public Nullable<System.DateTime> startdate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        public Nullable<System.DateTime> enddate { get; set; }

    }
}