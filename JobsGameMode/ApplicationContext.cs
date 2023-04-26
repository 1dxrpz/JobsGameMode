using JobsGameMode.entity;
using Microsoft.EntityFrameworkCore;

namespace JobsGameMode
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql("User ID=postgres; Server=localhost; port=5432; Database=webproject.dev; Password=123456; Pooling=true;");
		}
	}
}