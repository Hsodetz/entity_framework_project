using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entityFrameworkProyect.Models
{
	public class Article
	{
        
        public int Id { get; set; }

       
        public string Titulo { get; set; }

        
        public string Descripcion { get; set; }

        
        public DateTime Date { get; set; }

        [Range(0.1, 5.0)]
        public double Qualification { get; set; }

        
        public int CategoryId { get; set; }

        // Agregando llave foranea para realacion entre articulo y categoria desde el DBContext
        public Category Category { get; set; }


        // para relacion de muchos a muchos entre articulo y labels
        public ICollection<ArticleLabel> ArticleLabels { get; set; }

    }
}

