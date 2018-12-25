using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StDb2EFRP.Models;

namespace StDb2EFRP.Pages.Enroll
{
    public class RegisterModel : PageModel
    {
        private readonly StDb2EFRP.Models.StDb2SqlContext _context;

        public RegisterModel(StDb2EFRP.Models.StDb2SqlContext context)
        {
            _context = context;
        }

        public SelectList CSList { get; private set; }

        public SelectList StudentList { get; private set; }

        [BindProperty]
        public string SelectedCourse { get; set; }

        [BindProperty]
        public int SelectedStudent { get; set; }

        [BindProperty]
        public Enrollment Enrollment { get; set; }

        public async Task OnGetAsync()
        {
            IList<CoursesOffered> CoursesOfferedList = await _context.CoursesOffered.OrderBy(c => c.CourseNum).ToListAsync();
            string firstCourse = "";

            if (CoursesOfferedList.Count > 0)
            {
                firstCourse = CoursesOfferedList[0].CourseNum;
            }

            //IList<Enrollment> EnrollList = await _context.Enrollment 
            //        .Include(e => e.Student)
            //        .Where(e => e.CourseNum == firstCourse)
            //        .ToListAsync();

            IList<Students> StuList = await _context.Students
                .OrderBy(s => s.StudentId)
                .ToListAsync();
            int firstStudent = 0;

            //IList<Students> RemStuList = StuList;

            //foreach (var student in StuList)
            //{
            //    foreach(var enroll in EnrollList)
            //    {
            //        if(student.StudentId == enroll.StudentId)
            //        {
            //            RemStuList.Remove(student);
            //        }
            //    }
            //}

            if (StuList.Count > 0)
            {
                firstStudent = StuList[0].StudentId;
            }

            SelectedCourse = firstCourse;
            CSList = new SelectList(CoursesOfferedList, "CourseNum", "CourseNum");

            SelectedStudent = firstStudent;
            StudentList = new SelectList(StuList, "StudentId", "FirstName");
            //ViewData["msg"] = "";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IList<CoursesOffered> CoursesOfferedList = await _context.CoursesOffered.OrderBy(c => c.CourseNum).ToListAsync();
            string selCourse = SelectedCourse;
            IList<Students> StuList = await _context.Students.OrderBy(s => s.StudentId).ToListAsync();
            int selStudent = SelectedStudent;


            IList<Prerequisites> PrereqList = null;

            if(CoursesOfferedList.Count > 0)
            {
                PrereqList = await _context.Prerequisites
                //.Include(p => p.PrereqCnum)
                .Include(p => p.PrereqCnumNavigation)
                .Where(p => p.CourseNum == selCourse)
                .ToListAsync();
            }
            IList<CoursesTaken> coursesTakenList = null;

            if(StuList.Count > 0)
            {
                coursesTakenList = await _context.CoursesTaken
                    .Include(s => s.Student)
                    .Where(s => s.StudentId == selStudent)
                    .ToListAsync();
            }
            int prereqcount = 0;
            foreach(var item in PrereqList)
            {
                foreach(var courseTaken in coursesTakenList)
                {
                    if(courseTaken.CourseNumNavigation == item.PrereqCnumNavigation)
                    {
                        prereqcount++;
                    }
                }
            }

            if(prereqcount == PrereqList.Count)
            {
                Enrollment.CourseNum = selCourse;
                Enrollment.StudentId = selStudent;
                _context.Enrollment.Add(Enrollment);
                await _context.SaveChangesAsync(); 
                ViewData["msg"] = "";
                return RedirectToPage("./Enrollm");
            }
            else
            {
                ViewData["msg"] = "Student could not be registered due to Prerequisite requirement fail";
                string firstCourse = "";
                if (CoursesOfferedList.Count > 0)
                {
                    firstCourse = CoursesOfferedList[0].CourseNum;
                }
                int firstStudent = 0;
                if (StuList.Count > 0)
                {
                    firstStudent = StuList[0].StudentId;
                }
                SelectedCourse = firstCourse;
                CSList = new SelectList(CoursesOfferedList, "CourseNum", "CourseNum");
                SelectedStudent = firstStudent;
                StudentList = new SelectList(StuList, "StudentId", "FirstName");
                return Page();
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Enrollment.Add(Enrollment);
            //await _context.SaveChangesAsync(); 

            //return RedirectToPage("./Enrollm");
        }
    }
}