using System;
using entityFrameworkProyect.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace entityFrameworkProyect.ViewModels
{
    public class ArticleLabelViewModel
    {
        public ArticleLabel ArticleLabel { get; set; }
        public Article Article { get; set; }

        public IEnumerable<ArticleLabel> ListArticleLabels { get; set; }
        public IEnumerable<SelectListItem> ListLabels { get; set; }

        public ArticleLabelViewModel()
        {
            
        }
    }
}

