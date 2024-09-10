using System;
using entityFrameworkProyect.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace entityFrameworkProyect.ViewModels
{
    public class ArticleCategoryViewModel
    {
        public Article Article { get; set; }

        public IEnumerable<SelectListItem> ListCategories { get; set; }


    }
}

