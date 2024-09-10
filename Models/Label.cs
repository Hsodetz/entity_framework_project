using System;
using System.ComponentModel.DataAnnotations;

namespace entityFrameworkProyect.Models
{
	public class Label
	{
        
        public int Id { get; set; }

        public string Titulo { get; set; }

    
        public DateTime Date { get; set; }

        // para relacion de muchos a muchos entre articulo y labels
        public ICollection<ArticleLabel> ArticleLabels { get; set; }
    }
}
