using MongoDB.Driver;

namespace Saiyan.Repository
{
    public interface IRepositorySettings
    {
        string GetConnectionUrl();
        string GetDatabaseName();
        IMongoDatabase GetDatabase();
        string GetDefaultCollection();
        string GetCollectionByEntity(string entityName);
        IMongoClient GetClientConnection();
        MongoClientSettings GetRepositorySettings();
    }
}
