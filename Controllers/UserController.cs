using System;
using entityFrameworkProyect.Data;
using entityFrameworkProyect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace entityFrameworkProyect.Controllers
{
    public class UserController : Controller
    {
        public readonly Data.ApplicationDbContext _dbcontext;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Index()
        {
            List<User> users = await _dbcontext.Users.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return View();

            User user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return View();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (user is null)
                return View(user);

            _dbcontext.Users.Update(user);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return View();

            User user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return View();

            _dbcontext.Users.Remove(user);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
                return View();

            User user = await _dbcontext.Users.Include(d => d.DetailUser).FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetail(User user)
        {
            if (user.DetailUser.Id != 0)
                return View();

            // Creamos los detalles para ese usuario
            _dbcontext.DetailUsers.Add(user.DetailUser);
            await _dbcontext.SaveChangesAsync();

            //Despues de crear el detalle del usuario, obtenemos el usuario de la base de datos y le actualizamos el campo Id
            var userBd = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            userBd.DetailUserId = user.DetailUser.Id;
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }


}

