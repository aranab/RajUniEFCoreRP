using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RajUniEFCoreRP.Models;
using RajUniEFCoreRP.Models.SchoolViewModels;

namespace RajUniEFCoreRP.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorIndex { get;set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorIndex = new InstructorIndexData();
            InstructorIndex.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Department)
                //.Include(i => i.CourseAssignments)
                //.ThenInclude(i => i.Course)
                //.ThenInclude(i => i.Enrollments)
                //.ThenInclude(i => i.Student)
                //.AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorIndex.Instructors
                    .Single(i => i.ID == id.Value);
                InstructorIndex.Courses = instructor.CourseAssignments
                    .Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                //InstructorIndex.Enrollments = InstructorIndex.Courses
                //    .Single(x => x.CourseID == courseID)
                //    .Enrollments;
                var selectedCourse = InstructorIndex.Courses
                    .Where(x => x.CourseID == courseID)
                    .Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                InstructorIndex.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
