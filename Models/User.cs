using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entityFrameworkProyect.Models
{
	public class User
	{
        public int Id { get; set; }
        public string Name { get; set; }

        // el email se puede usar exoresion regular, o directamente con EmailAddress, ya que .net core tiene todo lo necesario para validar 
        //[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Favor ingrese un email valido")]
        [EmailAddress(ErrorMessage = "Favor ingrese un email valido")]
        public string Email { get; set; }

        [Display(Name = "Dirección del usuario")]
        public string Address { get; set; }

        public int age { get; set; }

        // Relacion uno a uno, donde vamos a relacionar la tabla de usuario con detalle de usuario
        // Aqui damos el foreignkey, y la tabla a relacionar, ahora en el modelo detailuser colocamos
        //  public User User { get; set; } para que referencie el uno a uno entre las dos tablas
        
        public int? DetailUserId { get; set; } // le agregamos el signo de interrogacion al int -> int?, para que el campo sea null en la tabla
        // Aqui instanciamos el objeto de la tabla hija (DetailUsers), usando el modelo DetailUser, esto para relacion 1 a 1
        public DetailUser DetailUser { get; set; }

    }
}

