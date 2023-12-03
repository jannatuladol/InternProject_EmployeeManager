using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        

        public EmployeeController (EmployeeDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();

            return View(applicants);
        }

        [HttpGet]
        public IActionResult Create()
        {

            Applicant applicant = new Applicant();
            applicant.EducationQualifications.Add(new EducationQualification() { EducationQualificationId = 1 });

            ViewBag.Gender = GetGender();
            ViewBag.DegreeName = GetDegreeName();

           

            return View(applicant);
        }


        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;
            //foreach (EducationQualification educationQualification in applicant.EducationQualifications)
            //{
            //    if (educationQualification.DegreeName == null || educationQualification.DegreeName.Length == 0) applicant.EducationQualifications.Remove(educationQualification);
            //}

            ViewBag.Gender = GetGender();
            _context.Add(applicant);
            _context.Entry(applicant).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("index");
        }



        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;

            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Details(int Id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.EducationQualifications).Where(a =>a.Id == Id).FirstOrDefault();
            return View(applicant);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.EducationQualifications).Where(a => a.Id == Id).FirstOrDefault();
            return View(applicant);
        }
        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Add(applicant);
            _context.Entry(applicant).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("index");
        }



        private List<SelectListItem> GetGender()
        {
            List<SelectListItem> selGender = new List<SelectListItem>();
            var selItem = new SelectListItem() { Value=" ", Text = "Select Gender" };

            selGender.Insert(0, selItem);

            selItem = new SelectListItem()
            {
                Value = "Male",
                Text = "Male"
            };
            selGender.Add(selItem);


            selItem = new SelectListItem()
            {
                Value = "Female",
                Text = "Female"
            };
            selGender.Add(selItem);

            selItem = new SelectListItem()
            {
                Value = "Other",
                Text = "Other"
            };

            selGender.Add(selItem);
            return selGender;



        }


        private List<SelectListItem> GetDegreeName()
        {
            List<SelectListItem> selDegreeName = new List<SelectListItem>();
            var selItem = new SelectListItem() { Value = " ", Text = "Select Degree Name" };

            selDegreeName.Insert(0, selItem);

        

            selItem = new SelectListItem()
            {
                Value = "SSC",
                Text = "SSC"
            };
            selDegreeName.Add(selItem);


            selItem = new SelectListItem()
            {
                Value = "HSC",
                Text = "HSC"
            };
            selDegreeName.Add(selItem);

            selItem = new SelectListItem()
            {
                Value = "BSc,Engineering",
                Text = "BSc,Engineering"
            };

            selDegreeName.Add(selItem);

            selItem = new SelectListItem()
            {
                Value = "MBA",
                Text = "MBA"
            };
            selDegreeName.Add(selItem);

            selItem = new SelectListItem()
            {
                Value = "MSC",
                Text = "MSC"
            };
            selDegreeName.Add(selItem);

            selItem = new SelectListItem()
            {
                Value = "BBA",
                Text = "BBA"
            };
            selDegreeName.Add(selItem);

            return selDegreeName;

        }




        //private List<SelectListItem> GetInstitution()
        //{
        //    List<SelectListItem> selInstitution = new List<SelectListItem>();
        //    var selItem = new SelectListItem() { Value = " ", Text = "Select Degree Name" };

        //    selInstitution.Insert(0, selItem);



        //    selItem = new SelectListItem()
        //    {
        //        Value = "SSC",
        //        Text = "SSC"
        //    };
        //    selInstitution.Add(selItem);


        //    selItem = new SelectListItem()
        //    {
        //        Value = "HSC",
        //        Text = "HSC"
        //    };
        //    selInstitution.Add(selItem);

        //    selItem = new SelectListItem()
        //    {
        //        Value = "BSc,Engineering",
        //        Text = "BSc,Engineering"
        //    };

        //    selInstitution.Add(selItem);

        //    selItem = new SelectListItem()
        //    {
        //        Value = "MBA",
        //        Text = "MBA"
        //    };
        //    selInstitution.Add(selItem);

        //    selItem = new SelectListItem()
        //    {
        //        Value = "MSC",
        //        Text = "MSC"
        //    };
        //    selInstitution.Add(selItem);

        //    selItem = new SelectListItem()
        //    {
        //        Value = "BBA",
        //        Text = "BBA"
        //    };
        //    selInstitution.Add(selItem);

        //    return selInstitution;

        //}

       


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.EducationQualifications).Where(a => a.Id == Id).FirstOrDefault();

          

            ViewBag.Gender = GetGender();

            ViewBag.DegreeName = GetDegreeName();
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Edit(Applicant applicant)
        {
            List<EducationQualification> eduDetails = _context.EducationQualifications.Where(d => d.ApplicantId == applicant.Id).ToList();
            _context.EducationQualifications.RemoveRange(eduDetails);
            _context.SaveChanges();



            if (applicant.ProfilePhoto != null)
            {
                string uniqueFileName = GetUploadedFileName(applicant);
                applicant.PhotoUrl = uniqueFileName;

            }
            //string uniqueFileName = GetUploadedFileName(applicant);
            //applicant.PhotoUrl = uniqueFileName;


            _context.Attach(applicant);

            _context.Entry(applicant).State = EntityState.Modified;
            _context.EducationQualifications.AddRange(applicant.EducationQualifications);
            _context.SaveChanges();
            return RedirectToAction("index");


        }




    }
}
