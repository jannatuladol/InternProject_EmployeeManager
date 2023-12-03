using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(150)]
        [DisplayName("Employee Name")]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";

        [Required]
        [Range(18, 55, ErrorMessage = "Currently,We Have no Positions Vacant for Your Age")]
        [DisplayName("Age in Years")]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Qualification { get; set; } = "";

        [Required]

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone Number is invalid")]
        [DisplayName("Phone Number")]
        public string Phone { get; set; } = "";

        public virtual List<EducationQualification> EducationQualifications { get; set; } = new List<EducationQualification>();//detail show

        public string PhotoUrl { get; set; } = "";


        //[Required(ErrorMessage = "Please choose the Profile Photo")]

        [NotMapped]
        [Display(Name = "Profile Photo")]
        public IFormFile? ProfilePhoto { get; set; } 

    }
}
