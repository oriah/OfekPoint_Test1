using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sisma.Project1.Logic.Data;

namespace Sisma.Project1.Logic.Models
{
    public class SismaContextFactory : IDesignTimeDbContextFactory<SismaContext>
    {
        public SismaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SismaContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;initial catalog=SismaDB;Integrated Security=True; MultipleActiveResultSets=True");
            return new SismaContext(optionsBuilder.Options);
        }
    }
}
