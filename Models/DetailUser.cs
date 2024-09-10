using System;
using System.ComponentModel.DataAnnotations;

namespace entityFrameworkProyect.Models
{
	public class DetailUser
	{
        
        public int Id { get; set; }

        
        public string Identification { get; set; }
        public string Sport { get; set; }
        public string Pet { get; set; }

        // Aqui instanciamos el objeto de la tabla padre (Users), usando el modelo User, esto para relacion 1 a 1
        public User User { get; set; }

    }
}

