using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PetPalsProfile.Infrastructure.Database;
public sealed class ApplicationDbContext : DbContext
{
	private static readonly JsonSerializerSettings JsonSerializerSettings = new()
	{
		TypeNameHandling = TypeNameHandling.All
	};

	public ApplicationDbContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("identity");
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

		base.OnModelCreating(modelBuilder);
	}
}