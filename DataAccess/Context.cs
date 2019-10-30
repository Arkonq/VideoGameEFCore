using Domain;
using Microsoft.EntityFrameworkCore;

namespace HighSchoolLesson.DataAccess
{
	public class Context : DbContext
	{
		public Context()
		{
			Database.EnsureCreated();
		}

		public DbSet<Rating> Ratings { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Videogame> Videogames { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=BorisHome\\Boris;Database=VideoGameEFCore;Trusted_Connection=true;");

		}
	}
}
