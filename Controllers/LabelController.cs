using System;
using entityFrameworkProyect.Data;
using entityFrameworkProyect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace entityFrameworkProyect.Controllers
{
    public class LabelController : Controller
    {
        public readonly Data.ApplicationDbContext _dbcontext;

        public LabelController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Label>>> Index()
        {
            List<Label> labels = await _dbcontext.Labels.ToListAsync();

            return View(labels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Label label)
        {
            if (!ModelState.IsValid)
                return View(label);

            _dbcontext.Labels.Add(label);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            Label label = await _dbcontext.Labels.FirstOrDefaultAsync(l => l.Id == id);

            if (label is null)
                return NotFound();

            return View(label);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Label label)
        {
            if (!ModelState.IsValid)
                return View(label);

            _dbcontext.Labels.Update(label);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            Label label = await _dbcontext.Labels.FirstOrDefaultAsync(l => l.Id == id);

            if (label is null)
                return NotFound();

            _dbcontext.Labels.Remove(label);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

