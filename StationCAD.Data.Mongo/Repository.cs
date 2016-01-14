
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace StationCAD.Data.Mongo
{
    public class Repository<T>
        where T : AbstractDocument
    {

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        protected IMongoCollection<T> DocumentCollection
        {
            get
            {
                IMongoCollection<T> coll = _database.GetCollection<T>(typeof(T).ToString());
                return coll;
            }
        }

        public Repository()
        {
            var environment = ConfigurationManager.AppSettings["environment"];
            var databaseName = string.Format("stationcad{0}{1}",
                                            environment.ToLower() == "prod" ? string.Empty : "-",
                                            environment.ToLower() == "prod" ? string.Empty : environment);
            var mongoDBUri = ConfigurationManager.AppSettings["mongoDBUri"];
            MongoClientSettings config = new MongoClientSettings();
            _client = new MongoClient(mongoDBUri);
            _database = _client.GetDatabase(databaseName);
        }

        public async Task Create(T document)
        {

            try
            {
                await DocumentCollection.InsertOneAsync(document);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                
            }
        }

        public async Task Create(T[] documents)
        {
            try
            {
                await DocumentCollection.InsertManyAsync(documents);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public async Task Replace(T document)
        {
            try
            {
                var builder = Builders<T>.Filter;
                var filter = builder.Eq("_id", document.Id);
                await DocumentCollection.ReplaceOneAsync(
                    filter,
                    document);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public async Task Delete(T document)
        {
            try
            {
                var builder = Builders<T>.Filter;
                var filter = builder.Eq("_id", document.Id);
                await DocumentCollection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        public ICollection<T> Find(ICollection<KeyValuePair<string, string>> parameters)
        {
            ICollection<T> results = null;
            try
            {
                results = DocumentCollection.AsQueryable().Where(
                    x => x.DocumentName == "").ToList<T>();
            }
            catch(Exception ex)
            {

            }
            finally
            {  }
            return results;
        }
    }
}
