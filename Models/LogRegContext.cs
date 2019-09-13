using Microsoft.EntityFrameworkCore;
using LogReg.Models;

namespace LogReg.Models 
{
	public class LogRegContext : DbContext
	{
		public LogRegContext(DbContextOptions options) : base(options) { }

		public DbSet<RUser> RUsers {get;set;}
		public DbSet<LUser> LUsers {get;set;}
	}
}