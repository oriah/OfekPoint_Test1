using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sisma.Project1.Logic.Data
{
    public class SismaContextFactory : IDesignTimeDbContextFactory<SismaContext>
    {
        public SismaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SismaContext>();
            optionsBuilder.UseSqlServer(@"Data Source=tcp:oria.database.windows.net,1433;initial catalog=SismaDB;Integrated Security=True; MultipleActiveResultSets=True");
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;initial catalog=SismaDB;Integrated Security=True; MultipleActiveResultSets=True");
            return new SismaContext(optionsBuilder.Options);
        } 
    }
}
