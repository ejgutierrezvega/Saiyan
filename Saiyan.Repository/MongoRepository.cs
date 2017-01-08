using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Saiyan.Domain.Entities;
using Saiyan.Pluralization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;

namespace Saiyan.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private const int DefaultLimit = 100;

        private readonly IMongoClient conn;
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T> collection;

        private readonly IRepositorySettings settings;
        private readonly IPluralizationService pluralizationService;
        private readonly IMongoQueryable<T> collectionQueryable;

        public MongoRepository(IRepositorySettings _settings, IPluralizationService _pluralizationService)
        {
            settings = _settings;
            pluralizationService = _pluralizationService;

            conn = _settings.GetClientConnection();
            db = settings.GetDatabase();
            collection = db.GetCollection<T>(pluralizationService.Pluralize(typeof(T).Name));
            collectionQueryable = collection.AsQueryable();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return collectionQueryable.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await collectionQueryable.AnyAsync(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return collectionQueryable.Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await collectionQueryable.CountAsync(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return collectionQueryable.First(predicate);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await collectionQueryable.FirstAsync(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, IFilter options)
        {
            var cursor = GetCursor(predicate, options);
            return cursor.FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, IFilter options)
        {
            var cursor = await GetCursorAsync(predicate, options);
            return await cursor.FirstOrDefaultAsync();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return collectionQueryable.AsExpandable().Where(predicate).Take(DefaultLimit);
        }

        public IList<T> ToList(Expression<Func<T, bool>> predicate)
        {
            return ToList(predicate, null);
        }

        public IList<T> ToList(Expression<Func<T, bool>> predicate, IFilter filter)
        {
            var cursor = collection.FindSync(predicate, GetOptions(filter));
            return cursor.ToList();
        }

        public async Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate)
        {
            return await ToListAsync(predicate, null);
        }

        public async Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, IFilter filter)
        {
            var cursor = await collection.FindAsync(predicate, GetOptions(filter));
            return await cursor.ToListAsync();
        }
        
        public void Insert(T item)
        {
            //include insert timestamp

            collection.InsertOne(item);
        }

        public async Task InsertAsync(T item)
        {
            //include insert timestamp

            await collection.InsertOneAsync(item);
        }

        public bool Update(T item)
        {
            //include updated timestamp

            var result = collection.ReplaceOne(p => p.id == item.id, item, new UpdateOptions { IsUpsert = true });
            return result.IsAcknowledged;
        }

        public async Task<bool> UpdateAsync(T item)
        {
            //include updated timestamp

            var result = await collection.ReplaceOneAsync(p => p.id == item.id, item, new UpdateOptions { IsUpsert = true });
            return result.IsAcknowledged;
        }

        public bool Delete(T item)
        {
            var result = collection.DeleteOne(d => d.id == item.id);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(T item)
        {
            var result = await collection.DeleteOneAsync(d => d.id == item.id);
            return result.IsAcknowledged;
        }

        private IAsyncCursor<T> GetCursor(Expression<Func<T, bool>> predicate, IFilter options)
        {
            return collection.FindSync(predicate, GetOptions(options));
        }

        private async Task<IAsyncCursor<T>> GetCursorAsync(Expression<Func<T, bool>> predicate, IFilter options)
        {
            return await collection.FindAsync(predicate, GetOptions(options));
        }

        private static FindOptions<T> GetOptions(IFilter filter)
        {
            var options = new FindOptions<T>();
            options.Limit = filter?.Limit ?? DefaultLimit;
            options.Sort = GetSort(filter);
            return options;
        }

        private static SortDefinition<T> GetSort(IFilter filter)
        {
            if (string.IsNullOrEmpty(filter?.SortColumn))
                return default(SortDefinition<T>);

            var sortDefinition = new SortDefinitionBuilder<T>();
            return filter.isDescending ? sortDefinition.Descending(filter.SortColumn)
                : sortDefinition.Ascending(filter.SortColumn);
        }
    }


}
