using ElectronicSignage.Domain.Interface;
using ElectronicSignage.Domain.Options;
using ElectronicSignage.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElectronicSignage.Repository
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IIdentifier<TKey> where TKey : IEquatable<TKey>
    {
        protected IMongoCollection<BsonDocument> BsonDocumentCollection
        {
            get { return bsonDocumentCollection; }
        }
        private readonly IMongoCollection<BsonDocument> bsonDocumentCollection;

        public IMongoCollection<TEntity> TEntityCollection
        {
            get { return tEntityCollection; }
        }
        private readonly IMongoCollection<TEntity> tEntityCollection;

        #region constructors

        public GenericRepository(MongoDBOptions mongoDBOptions)
        {
            var mongoUrl = new MongoUrl(mongoDBOptions.ConnectionString + mongoDBOptions.CollectionName);
            var client = new MongoClient(mongoUrl);
            this.bsonDocumentCollection = client.GetDatabase(mongoUrl.DatabaseName).GetCollection<BsonDocument>(GetCollectionName());
            this.tEntityCollection = client.GetDatabase(mongoUrl.DatabaseName).GetCollection<TEntity>(GetCollectionName());
        }

        public GenericRepository(string connectionString, string collectionName)
        {
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            this.tEntityCollection = client.GetDatabase(mongoUrl.DatabaseName).GetCollection<TEntity>(collectionName);
        }

        #endregion

        #region public methods

        public virtual async Task Create(TEntity entity)
        {
            await this.TEntityCollection.InsertOneAsync(entity);
        }

        public virtual async Task CreateAll(IEnumerable<TEntity> entities)
        {
            await TEntityCollection.InsertManyAsync(entities);
        }

        public virtual async Task<TEntity> FindBy(object id)
        {
            return await this.TEntityCollection.Find<TEntity>(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FetchAll()
        {
            return await TEntityCollection.Find(new BsonDocument()).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FetchAll(Expression<Func<TEntity, bool>> filter)
        {
            return await this.TEntityCollection.Find(filter).ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            this.TEntityCollection.ReplaceOne<TEntity>(e => entity.Id.Equals(e.Id), entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await this.TEntityCollection.ReplaceOneAsync<TEntity>(e => entity.Id.Equals(e.Id), entity);
            return entity;
        }

        public virtual async Task Delete(object id)
        {
            await this.TEntityCollection.DeleteOneAsync<TEntity>(e => e.Id.Equals(id));
        }

        public virtual async Task Delete(Expression<Func<TEntity, bool>> filter)
        {
            await this.TEntityCollection.DeleteOneAsync<TEntity>(filter);
        }

        public virtual async Task<long> DeleteAll()
        {
            var task = await TEntityCollection.DeleteManyAsync(new BsonDocument());
            return task.DeletedCount;
        }

        public virtual bool Exist(object id)
        {
            return this.TEntityCollection.CountDocuments(e => e.Id.Equals(id)) > 0;
        }

        #endregion

        #region IEnumerable<TEntity> Members

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.TEntityCollection.AsQueryable().GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return this.TEntityCollection.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return this.TEntityCollection.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this.TEntityCollection.AsQueryable().Provider; }
        }

        #endregion

        private string GetCollectionName()
        {
            return typeof(TEntity).Name;
        }
    }
}