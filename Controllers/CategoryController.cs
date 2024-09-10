using System;
using entityFrameworkProyect.Data;
using entityFrameworkProyect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace entityFrameworkProyect.Controllers
{
	public class CategoryController : Controller
	{
		public readonly ApplicationDbContext _dbcontext;

        public CategoryController(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Index()
        {
            // Con este traemos la consulta completa de las categorias.
            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            // Con este traemos la cosulta filtrada por una fecha, esto como ejemplo
            //DateTime DateCompare = new DateTime(2022, 06, 22);
            //List<Category> categories = await _dbcontext.Categories.Where(c => c.CreatedAt >= DateCompare).ToListAsync();

            // Tambien podemos hacer consultas sql convencionales 
            //List<Category> categories = _dbcontext.Categories.FromSqlRaw("select * from categories where active=1").ToList();

            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateMultipleFromViewForm()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _dbcontext.Add(category);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> CreateMultipleTwo()
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < 2; i++)
                categories.Add(new Category { Name = Guid.NewGuid().ToString() });


            await _dbcontext.AddRangeAsync(categories);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> CreateMultipleFive()
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < 5; i++)
                categories.Add(new Category { Name = Guid.NewGuid().ToString() });

            await _dbcontext.Categories.AddRangeAsync(categories);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Metodo en el cual se recibe multiple datos de un formulario
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateMultipleCategory(string[] name, [Bind("Name")] List<Category> categories)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(CreateMultipleFive));

            foreach (var n in name)
            {
                Category category = new Category
                {
                    Name = n
                };
                categories.Add(category);
            }

            await _dbcontext.AddRangeAsync(categories);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return View();

            Category category = await _dbcontext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _dbcontext.Categories.Update(category);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Index));

            Category category = await _dbcontext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                return RedirectToAction(nameof(Index));

            _dbcontext.Categories.Remove(category);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> DeleteMultipleTwo()
        {
            List<Category> categories = _dbcontext.Categories.OrderByDescending(c => c.Id).Take(2).ToList();

            _dbcontext.Categories.RemoveRange(categories);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> DeleteMultipleFive()
        {
            IEnumerable<Category> categories = _dbcontext.Categories.OrderByDescending(c => c.Id).Take(5);

            _dbcontext.Categories.RemoveRange(categories);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

