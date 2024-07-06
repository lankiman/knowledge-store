﻿using Microsoft.AspNetCore.Mvc;
using e_learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using e_learning.ViewModels;
using Microsoft.CodeAnalysis.CSharp;


namespace e_learning.Controllers
{
    [Authorize(Roles = "InstructorModel")]
    public class InstructorController(
        ILessonService lessonService,
        IInstructorService instructorService) : Controller
    {
        // GET:Creator
        public async Task<IActionResult> InstructorDashboard()
        {
            var user = await instructorService.GetAuthenticatedInstructor();

            return View(user);
        }

        public async Task<IActionResult> InstructorLessons()
        {
            var lessons = await instructorService.GetAuthenticatedInstructorLessons();

            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLesson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonViewModel lessonData)
        {
            if (ModelState.IsValid)
            {
                var result = await instructorService.CreateLesson(lessonData);

                switch (result)
                {
                    case OkObjectResult:
                        break;

                    case ObjectResult { StatusCode: 500 }:
                        ModelState.AddModelError("", "An Error Occured");
                        break;
                }
            }

            return View(lessonData);
        }
    }
}