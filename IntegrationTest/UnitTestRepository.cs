using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NUnit.Framework;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;

namespace IntegrationTest
{
    public class Tests
    {
        private readonly IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRepositoryMongoDB()
        {
            var client = new MongoClient();
            var perguntas = new MongoDbAskRepository(client);
            perguntas.StoreAsk("funciona?");
            Assert.Pass();
        }

        [Test]
        public void TestRepositoryMockAsk()
        {
            var perguntas = new MockAskRepository();
            perguntas.StoreAsk("funciona?");
            Assert.Pass();
        }

        [Test]
        public void TestRepositoryMockUserProfileCreate()
        {
            SimpleUser user = new SimpleUser("100");

            var UserProfile = new MockUserProfileRepository();
            SimpleUser newUser = UserProfile.Create(user);
            Assert.Pass();
        }

        [Test]
        public void TestRepositoryMockUserProfileTryLoadUser()
        {
            var UserProfile = new MockUserProfileRepository();
            UserProfile.TryLoadUser("");
            Assert.Pass();
        }

        [Test]
        public void TestRepositorySqlUserProfileCreate()
        {
            SqlAskRepository ask = new SqlAskRepository(_configuration);
            ask.StoreAsk("Pergunta 1");
            Assert.Pass();
        }

        [Test]
        public void TestRepositorySqlUserProfileTryLoadUser()
        {
            SqlUserProfileRepository UserProfile = new SqlUserProfileRepository(_configuration);
            UserProfile.TryLoadUser("");
            Assert.Pass();
        }
    }
}