using ElectronicSignage.Domain;
using ElectronicSignage.Domain.Options;
using System;

namespace ElectronicSignage.Repository
{
    public class AuthorizationRepository : GenericRepository<Authorization, Guid>
    {
        public AuthorizationRepository(MongoDBOptions mongoDBOptions) : base(mongoDBOptions)
        {
        }
    }
}