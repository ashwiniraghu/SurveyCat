using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyCat.Models
{
    [MetadataType(typeof(QuestionPropertyMetadata))]
    public partial class QuestionProperty
    {
    }
    public class QuestionPropertyMetadata
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int QID { get; set; }

        [DisplayName("QProperty")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "QProperty Cannot be empty")]
        public string Property { get; set; }
    }
}