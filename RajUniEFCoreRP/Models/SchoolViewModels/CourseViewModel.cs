using System.ComponentModel.DataAnnotations;

namespace RajUniEFCoreRP.Models.SchoolViewModels
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
