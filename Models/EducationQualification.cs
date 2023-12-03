using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;



namespace EmployeeManager.Models

{
    public class EducationQualification
    {
        

        [Key]
        public int EducationQualificationId { get; set; }

        [ForeignKey("Applicant")]//FK 
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; private set; } //ONE-MANY implementation

        public string DegreeName { get; set; } = "";
        public string Institution { get; set; } = "";
        [Required]
        public int PassingYear { get; set; }



    }

}
