using System;
using System.ComponentModel.DataAnnotations;

namespace entityFrameworkProyect.Models
{
	public class Category
	{
       
        public int Id { get; set; }
        public string Name { get; set; }

       
        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }

        // Relacion de uno a muchos, en este caso en la tabla padre añado una lista de la tabla hija
        // osea que quedaria de la siguiente manera para la relacion de uno a muchos con articulos
        // Indicando que con esta propiedad le digo q es una relacion de muchos a muchos.
        public List<Article> Article { get; set; }

    }
}
