using Microsoft.EntityFrameworkCore;
using SQLConnection.Models;

namespace SQLConnection.Services
{
	public class DatabaseConnection : DbContext
	{
		public DbSet<Plant> Plants => Set<Plant>();

		public DatabaseConnection() => this.Database.EnsureCreated();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=production.db");
		}
	}
}
