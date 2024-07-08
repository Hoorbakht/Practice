using Microsoft.EntityFrameworkCore;
using Practice.Model;

namespace Practice.DataAccess;

public class PracticeContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Person> Persons { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Person>().HasData(new List<Person>
		{
			new()
			{
				Id = 1,
				Name = "John",
				Family = "Doe"
			}
		});

		base.OnModelCreating(modelBuilder);
	}
}