using ElectronicSignage.Domain;
using ElectronicSignage.Domain.Options;
using ElectronicSignage.Repository;
using System;
using Xunit;

namespace ElectronicSignage.Test
{
    public class AuthorizationRepositoryTest
    {
        private AuthorizationRepository authorizationRepository;

        public AuthorizationRepositoryTest()
        {
            this.authorizationRepository = new AuthorizationRepository(new MongoDBOptions() {
                ConnectionString = "mongodb://localhost:27017/",
                CollectionName = "ATK_ElectronicSignage"
            });
        }

        [Fact]
        public void Test1()
        {
            this.authorizationRepository.Create(new Authorization()
            {
                Account = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            }).Wait();
        }
    }
}
