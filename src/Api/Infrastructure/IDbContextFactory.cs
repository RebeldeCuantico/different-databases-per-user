namespace Api.Infrastructure
{
    public interface IDbContextFactory
    {
        void SetConnectionString(Dictionary<string, string> connectionStrings);
        CatalogContext Create(string user);
    }
}