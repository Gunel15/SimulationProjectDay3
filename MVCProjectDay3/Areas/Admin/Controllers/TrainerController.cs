using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProjectDay3.DataAccessLayer;
using MVCProjectDay3.Models;
using MVCProjectDay3.ViewModels.Trainers;

namespace MVCProjectDay3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController(FitnessDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var datas = await _context.Trainers.Select(x => new TrainerGetVM
            {
                Name = x.Name,
                Id = x.Id
            }).ToListAsync();
            return View(datas);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            await _context.Trainers.AddAsync(new Trainer
            {
                Name = vm.Name,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            var trainer = await _context.Trainers.Where(x => x.Id == id).Select(x => new TrainerUpdateVM
            {
                Name = x.Name
            }).FirstOrDefaultAsync();
            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, TrainerUpdateVM vm)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(vm);
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
                return NotFound();
            trainer.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue || id.Value < 1)
                return BadRequest();
            int result = await _context.Trainers.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (result == 0)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
