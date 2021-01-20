using Catalog.Api.Data.Interfaces;
using Catalog.Api.Entities;
using Catalog.Api.Settings;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(ICatalogDatabaseSettings catalogDatabaseSettings)
        {
            var client = new MongoClient(catalogDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(catalogDatabaseSettings.DatabaseName);

            Products = database.GetCollection<Product>(catalogDatabaseSettings.CollectionName);
            CatalogContextSeeder.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
