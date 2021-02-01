using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyCat.Models
{
    [MetadataType(typeof(QuesionMetadata))]
    public partial class Quesion
    {
        
    }
    public class QuesionMetadata
    {
        
        public int QID { get; set; }

        //[Display(Name = "Id")]
        //public int tempID { get; set; }

        [Display(Name = "Question")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="please enter valid question")]
        public string QText { get; set; }
    }
}