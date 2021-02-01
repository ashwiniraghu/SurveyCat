using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SurveyCat.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
       
    }

    public class UserMetadata
    {
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings =false, ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage ="Minimum 6 Character required")]
        public string Password { get; set; }

        [DisplayName("User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}