using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entityFrameworkProyect.Data;
using entityFrameworkProyect.Models;
using entityFrameworkProyect.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace entityFrameworkProyect.Controllers
{
    public class ArticleController : Controller
    {
        public readonly ApplicationDbContext _dbcontext;

        public ArticleController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // GET: /<controller>/
        public async Task<ActionResult<List<Article>>> Index()
        {
            // De esta manera nos ytraemos el id de la categoria
            //List<Article> articles = await _dbcontext.Articles.ToListAsync();

            // Pero como necesitamos el nombre de la categoria para mostrar en la tabla, usaremos explicit loading, que hace menos consultas, y es un poco mas rapido
            //foreach (var article in articles)
            //{
            //    await _dbcontext.Entry(article).Reference(c => c.Category).LoadAsync();
            //}

            // Pero para traer datos mas eficientemente, debemos usar eager loading (carga diligente)
            List<Article> articles = await _dbcontext.Articles.Include(c => c.Category).ToListAsync();

            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleCategoryViewModel articleCategories = new ArticleCategoryViewModel();
            articleCategories.ListCategories = _dbcontext.Categories.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            return View(articleCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            if (!ModelState.IsValid)
                return View(article);

            _dbcontext.Add(article);
            await _dbcontext.SaveChangesAsync();

            // Para que al retornar la vista por algun error, tambien retorne la lista de categorias.
            ArticleCategoryViewModel articleCategories = new ArticleCategoryViewModel();
            articleCategories.ListCategories = _dbcontext.Categories.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return View();

            ArticleCategoryViewModel articleCategories = new ArticleCategoryViewModel();
            articleCategories.ListCategories = _dbcontext.Categories.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });

            articleCategories.Article = await _dbcontext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (articleCategories is null)
                return NotFound();

            return View(articleCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleCategoryViewModel articleCategoryViewModel)
        {
            if (articleCategoryViewModel.Article.Id == 0)
                return View(articleCategoryViewModel.Article);

            _dbcontext.Articles.Update(articleCategoryViewModel.Article);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Index));

            Article article = await _dbcontext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article is null)
                return RedirectToAction(nameof(Index));

            _dbcontext.Articles.Remove(article);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> ManageTask(int id)
        {
            ArticleLabelViewModel articleLabelViewModels = new ArticleLabelViewModel
            {
                ListArticleLabels = _dbcontext.ArticleLabels.Include(e => e.Label).Include(a => a.Article).Where(a => a.ArticleId == id),

                ArticleLabel =  new ArticleLabel()
                {
                    ArticleId = id,
                },

                Article = await _dbcontext.Articles.FirstOrDefaultAsync(a => a.Id == id),
            };
            List<int> ListTemporaryArticleLabel = articleLabelViewModels.ListArticleLabels.Select(e => e.LabelId).ToList();

            // Obtener todas las etiquetas cuyos id's no esten en ListTemporaryArticleLabel
            // Crear un NOT IN usando LINQ

            var ListTemporary = _dbcontext.Labels.Where(e => !ListTemporaryArticleLabel.Contains(e.Id)).ToList();

            // Crear listas de etiquetas para el dropdown
            articleLabelViewModels.ListLabels = ListTemporary.Select(i => new SelectListItem
            {
                Text = i.Titulo,
                Value = i.Id.ToString(),
            });

            return View(articleLabelViewModels);

        }

        [HttpPost]
        public async Task<IActionResult> ManageTask(ArticleLabelViewModel articleLabelViewModel)
        {

            if (articleLabelViewModel.ArticleLabel.ArticleId != 0 && articleLabelViewModel.ArticleLabel.LabelId != 0)
            {
                await _dbcontext.ArticleLabels.AddAsync(articleLabelViewModel.ArticleLabel);
                await _dbcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManageTask), new {@id = articleLabelViewModel.ArticleLabel.ArticleId});

        }

        [HttpPost]
        public async Task<IActionResult> DeleteLabel(int idLabel, ArticleLabelViewModel articleLabelViewModel)
        {
            int idArticle = articleLabelViewModel.Article.Id;
            ArticleLabel articleLabel = await _dbcontext.ArticleLabels.FirstOrDefaultAsync(u => u.LabelId == idLabel && u.ArticleId == idArticle);

            _dbcontext.ArticleLabels.Remove(articleLabel);
            await _dbcontext.SaveChangesAsync();
            

            return RedirectToAction(nameof(ManageTask), new { @id = idArticle });

        }
    }
}

