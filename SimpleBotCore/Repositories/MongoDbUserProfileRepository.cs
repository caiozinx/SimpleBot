using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories.Interfaces;
using System;

namespace SimpleBotCore.Repositories
{
    public class MongoDbUserProfileRepository : IUserProfileRepository
    {
        MongoClient _client;
        IMongoCollection<SimpleUser> _collection;

        public MongoDbUserProfileRepository(MongoClient client)
        {
            _client = client;
            var db = _client.GetDatabase("db19");
            var storeCol = db.GetCollection<SimpleUser>("userProfile");
            _collection = storeCol;            
        }

        public void AtualizaCor(string userId, string cor)
        {
            var builderFilter= Builders<SimpleUser>.Filter;
            var filter = builderFilter.Eq("_id", userId);

            var user = _collection.Find(filter);

            var builderUpdate = Builders<SimpleUser>.Update;
            var update = builderUpdate
                .Set("_id", userId)
                .Set("Nome", user.FirstOrDefault().Nome)
                .Set("Idade", user.FirstOrDefault().Idade)
                .Set("Cor", cor);

            _collection.FindOneAndUpdate(filter, update);
        }

        public void AtualizaIdade(string userId, int idade)
        {
            var builderFilter = Builders<SimpleUser>.Filter;
            var filter = builderFilter.Eq("_id", userId);

            var user = _collection.Find(filter);

            var builderUpdate = Builders<SimpleUser>.Update;
            var update = builderUpdate
                .Set("_id", userId)
                .Set("Nome", user.FirstOrDefault().Nome)
                .Set("Idade", idade)
                .Set("Cor", user.FirstOrDefault().Cor);

            _collection.FindOneAndUpdate(filter, update);
        }

        public void AtualizaNome(string userId, string name)
        {
            var builderFilter = Builders<SimpleUser>.Filter;
            var filter = builderFilter.Eq("_id", userId);

            var user = _collection.Find(filter);

            var builderUpdate = Builders<SimpleUser>.Update;
            var update = builderUpdate
                .Set("_id", userId)
                .Set("Nome", name)
                .Set("Idade", user.FirstOrDefault().Idade)
                .Set("Cor", user.FirstOrDefault().Cor);

            _collection.FindOneAndUpdate(filter, update);
        }

        public SimpleUser Create(SimpleUser user)
        {
            var userExists = TryLoadUser(user.Id);

            if (userExists != null)
            {
                return userExists;
            }

            var persistUser = new SimpleUser(user.Id, user.Nome, user.Idade, user.Cor);

            _collection.InsertOne(persistUser);

            var userCreated = TryLoadUser(user.Id);

            return userCreated;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            var builderFilter = Builders<SimpleUser>.Filter;
            var filter = builderFilter.Eq("_id", userId);

            return _collection.Find(filter).FirstOrDefault();
        }
    }
}
