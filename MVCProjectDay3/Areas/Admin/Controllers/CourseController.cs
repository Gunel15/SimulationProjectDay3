using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProjectDay3.DataAccessLayer;
using MVCProjectDay3.Models;
using MVCProjectDay3.ViewModels.Courses;

namespace MVCProjectDay3.Areas.Admin.Controllers
{
    public class CourseController(FitnessDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Courses.Select(x => new CourseGetVM
            {
                Title = x.Title,
                Description = x.Description,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Date = x.Date,
                TrainerName = x.Trainer.Name
            }).ToListAsync();
            return View(datas);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Trainers = await _context.Trainers.ToListAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateVM vm)
        {
            if (vm.ImageFile != null)
            {
                if (vm.ImageFile.ContentType.StartsWith("image")) ;
                ModelState.AddModelError("ImageFile", "File format must be image");
                if (vm.ImageFile.Length / 1024 > 200)
                    ModelState.AddModelError("ImageFile", "File size must be less than 200kb");
            }
            if (!ModelState.IsValid)
                return View(vm);
            if (!await _context.Trainers.AnyAsync(x => x.Id == vm.TrainerId))
            {
                ModelState.AddModelError("TrainerId", "Trainer does not exit");
                ViewBag.Trainers = await _context.Trainers.ToListAsync();
                return View(vm);
            }
            string newImgName = Guid.NewGuid().ToString() + vm.ImageFile.FileName;
            string path = Path.Combine("wwwroot", "imgs", "courses", newImgName);

            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            await vm.ImageFile.CopyToAsync(fs);

            Course course = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                TrainerId = vm.TrainerId,
                ImageUrl = newImgName,
                Date = vm.Date
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            ViewBag.Trainers = await _context.Trainers.ToListAsync();
            var course = await _context.Courses.Where(x => x.Id == id).Select(x => new CourseUpdateVM
            {
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                TrainerId = x.TrainerId,
                Date = x.Date
            }).FirstOrDefaultAsync();
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(int? id, CourseUpdateVM vm)
        //{
        //    if (!id.HasValue || id.Value < 1)
        //        return BadRequest();

        //    if (vm.ImageFile != null)
        //    {
        //        if (vm.ImageFile.ContentType.StartsWith("image")) ;
        //        ModelState.AddModelError("ImageFile", "File format must be image");
        //        if (vm.ImageFile.Length / 1024 > 200)
        //            ModelState.AddModelError("ImageFile", "File size must be less than 200kb");
        //    }
        //    if (!ModelState.IsValid)
        //        return View(vm);
        //    if (!await _context.Trainers.AnyAsync(x => x.Id == vm.TrainerId))
        //    {
        //        ModelState.AddModelError("TrainerId", "Trainer does not exit");
        //        ViewBag.Trainers = await _context.Trainers.ToListAsync();
        //        return View(vm);
        //    }
        //    var course = await _context.Trainers.FindAsync(id);
        //    if (course == null)
        //        return NotFound();
        //    course.Title = vm.Title;
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}
