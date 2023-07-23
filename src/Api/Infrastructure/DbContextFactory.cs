using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure;
public class CatalogContextFactory : IDbContextFactory
{
    private AsyncLocal<CatalogContext> _threadContext = new();

    public Dictionary<string, string> ConnectionStrings { get; private set; }

    public void SetConnectionString(Dictionary<string, string> connectionStrings)
    {
        ConnectionStrings = connectionStrings;
    }

    public CatalogContext Create(string user)
    {
        if (_threadContext.Value == null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
            optionsBuilder.UseNpgsql(ConnectionStrings[user]);
            var context = new CatalogContext(optionsBuilder.Options);
            _threadContext.Value = context;
            return context;
        }

        return _threadContext.Value;

    }    
}
