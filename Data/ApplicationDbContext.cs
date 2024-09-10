using System;
using entityFrameworkProyect.Models;
using Microsoft.EntityFrameworkCore;

namespace entityFrameworkProyect.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		// Aqui van los modelos que representan las tablas, osea cada modelo es una tabla
		public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<DetailUser> DetailUsers { get; set; }

        public DbSet<Label> Labels { get; set; }

		public DbSet<ArticleLabel> ArticleLabels { get; set; }


		// haciendo uso de la api fluente, para poder ejecutar la relacion de muchos a muchos, en este caso entre articulo y etiqueta
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Usamos para la relacion muchos a muchos
			//modelBuilder.Entity<ArticleLabel>().HasKey(al => new { al.LabelId, al.ArticleId });

			// La siembra de datos se realiza en este metodo OnModelCreating() en el ApplicationDbContext
			// para la misma creamos una instancia de los datos a sembrar, como veremos a continuacion para categorias
			//Category category5 = new Category { Id = 45, Name = "Categoria 5", CreatedAt = new DateTime(2022, 06, 22), Active = true };
			//         Category category6 = new Category { Id = 46, Name = "Categoria 6", CreatedAt = new DateTime(2022, 06, 22), Active = false };
			//         Category category7 = new Category { Id = 47, Name = "Categoria 7", CreatedAt = new DateTime(2022, 06, 22), Active = true };

			// ahora con el modelbuilder le pasamos el array de categorias
			//modelBuilder.Entity<Category>().HasData(new Category[] { category5, category6, category7 });



			// Trabajando para categorias con la Api Fluente
			// Indicandole llave primaria
			modelBuilder.Entity<Category>().HasKey(c => c.Id); // asi le indicamos la llave primaria con la api fluente

			// Para indicarle q el campo es requerido con la api fluente
			modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();

			// Para indicarle que es un campo tipo fecha
			modelBuilder.Entity<Category>().Property(c => c.CreatedAt).HasColumnType("date");

			// Fluent api para articulo
			modelBuilder.Entity<Article>().HasKey(a => a.Id);
			modelBuilder.Entity<Article>().Property(a => a.Titulo).IsRequired().HasMaxLength(20); // definimos para el campo titulo q sea requerido y q maximo sea de 20
            modelBuilder.Entity<Article>().Property(a => a.Descripcion).IsRequired().HasMaxLength(500); // definimos para el campo descripcion q sea requerido y q maximo sea de 500
			modelBuilder.Entity<Article>().Property(a => a.Date).HasColumnType("date");
            modelBuilder.Entity<Article>().Property(a => a.Titulo).HasColumnName("Título"); // cambiamos el nombre a la columna


			// Fluent api para usuario
			modelBuilder.Entity<User>().HasKey(c => c.Id);
			modelBuilder.Entity<User>().Ignore(u => u.age);  // este es como el mapped en data annotations

            // Fluent api para detalle de usuario
            modelBuilder.Entity<DetailUser>().HasKey(c => c.Id);
            modelBuilder.Entity<DetailUser>().Property(du => du.Identification).IsRequired();

			// Fluent Api para etiqueta
			modelBuilder.Entity<Label>().HasKey(l => l.Id);
			modelBuilder.Entity<Label>().Property(l => l.Date).HasColumnType("date");


			// Fluent Api: Relación de uno a uno entre Usuario y Detalle de Usuario
			modelBuilder.Entity<User>().HasOne(u => u.DetailUser).WithOne(u => u.User).HasForeignKey<User>("DetailUserId");

			// Fluent api: Relacion de uno a muchos entre Categoria y articulo
			modelBuilder.Entity<Article>().HasOne(a => a.Category).WithMany(c => c.Article).HasForeignKey(a => a.CategoryId);

			// Fluent api: Relación de muchos a muchos entre Articulo y Etiqueta
			modelBuilder.Entity<ArticleLabel>().HasKey(ae => new { ae.LabelId, ae.ArticleId });
			// Ahora referenciamos articuloEtiqueta con Articulo
			modelBuilder.Entity<ArticleLabel>().HasOne(a => a.Article).WithMany(a => a.ArticleLabels).HasForeignKey(c => c.ArticleId);
            // Ahora referenciamos articuloEtiqueta con Etiqueta
            modelBuilder.Entity<ArticleLabel>().HasOne(ae => ae.Label).WithMany(a => a.ArticleLabels).HasForeignKey(c => c.LabelId);



            base.OnModelCreating(modelBuilder);
			//luego en la terminal ejecutamos el comando para una nueva migracion, y de esta manera se sembrarias los datos.

        }

    }
}

