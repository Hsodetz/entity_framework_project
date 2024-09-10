using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace entityFrameworkProyect.Models
{
	public class ArticleLabel
	{

        // Estos campos son llaves primarias, pero aqui no puedo usar ambos como llave primaria
        // para ello hacemos uso de la api fluente sobreescribiendo desde el metodo OnModelCreating
        // de la clase ApplicationDbContext
        
        public int ArticleId { get; set; }

        
        public int LabelId { get; set; }


        // instanciamos el objeto Article y el objeto Label, para poder hacer la relacion de muchos a muchos
        public Article Article { get; set; }
        public Label Label { get; set; }


    }
}

