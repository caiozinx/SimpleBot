using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBotCore.Repositories
{
    public class MongoDbAskRepository : IAskRepository
    {
        MongoClient _client;
        IMongoCollection<BsonDocument> _collection;

        public MongoDbAskRepository(MongoClient client)
        {
            _client = client;
            var db = client.GetDatabase("db19");
            var storeCol = db.GetCollection<BsonDocument>("ask");
            _collection = storeCol;
        }

        public void StoreAsk(string ask)
        {
            var persistAsk = new BsonDocument()
            {
                { "ask", ask }
            };

            _collection.InsertOne(persistAsk);
        }
    }
}
