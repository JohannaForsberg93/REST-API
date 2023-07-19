using Koduppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Koduppgift.Data
	{
	public class DataContext : DbContext
		{
			public DataContext(DbContextOptions<DataContext> options) : base(options)
			{
				
			}

			public DbSet<User> Users { get; set; }
			public DbSet<Role> Roles { get; set; }
			public DbSet<Group> Groups { get; set; }




		}
	}
