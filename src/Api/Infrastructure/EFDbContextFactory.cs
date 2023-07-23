using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure
{
    public class EFDbContextFactory : IDbContextFactory<CatalogContext>
    {
        public EFDbContextFactory()
        {
            
        }

        public CatalogContext CreateDbContext()
        {
            throw new NotImplementedException();
        }
    }
}
