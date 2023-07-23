using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;
public class CatalogContextFactory : IDbContextFactory
{
    public Dictionary<string, string> ConnectionStrings { get; private set; }

    public void SetConnectionString(Dictionary<string, string> connectionStrings)
    {
        ConnectionStrings = connectionStrings;
    }

    public CatalogContext Create(string user)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
        optionsBuilder.UseNpgsql(ConnectionStrings[user]);
        return new CatalogContext(optionsBuilder.Options);
    }    
}
