//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SurveyCat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quesion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quesion()
        {
            this.QuesDescs = new HashSet<QuesDesc>();
            this.ResponseDetails = new HashSet<ResponseDetail>();
        }
    
        public int QID { get; set; }
        public int QTypeID { get; set; }
        public string QText { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuesDesc> QuesDescs { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual QuestionProperty QuestionProperty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponseDetail> ResponseDetails { get; set; }
    }
}