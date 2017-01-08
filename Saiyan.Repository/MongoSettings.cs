using MongoDB.Driver;
using System;

namespace Saiyan.Repository
{
    public class MongoSettings : IRepositorySettings
    {
        IMongoClient conn = null;
        string host = "localhost";
        string port = "27017";

        public string GetCollectionByEntity(string entityName)
        {
            throw new NotImplementedException();
        }

        public IMongoClient GetClientConnection()
        {
            MongoUrl url = new MongoUrl(GetConnectionUrl());
            conn = new MongoClient(url);
            return conn;
        }

        public string GetConnectionUrl()
        {
            return $"mongodb://{host}:{port}";
        }

        public string GetDatabaseName()
        {
            return "dragon";
        }

        public IMongoDatabase GetDatabase()
        {
            return conn.GetDatabase(GetDatabaseName());
        }

        public string GetDefaultCollection()
        {
            return "persons";
        }

        public MongoClientSettings GetRepositorySettings()
        {
            var clientSettings = new MongoClientSettings();
            return clientSettings;
        }
    }
}
