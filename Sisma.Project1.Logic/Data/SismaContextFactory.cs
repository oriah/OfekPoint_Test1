using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sisma.Project1.Logic.Data
{
    public class SismaContextFactory : IDesignTimeDbContextFactory<SismaContext>
    {
        public SismaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SismaContext>();
            //Dev:
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;initial catalog=SismaDB;Integrated Security=True; MultipleActiveResultSets=True");
            //Prod:
            optionsBuilder.UseSqlServer(@"Server=tcp:oria.database.windows.net,1433;Initial Catalog=SismaDB;Persist Security Info=False;User ID=sa1;Password=ids8eWE234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new SismaContext(optionsBuilder.Options);
        }
    }
}
