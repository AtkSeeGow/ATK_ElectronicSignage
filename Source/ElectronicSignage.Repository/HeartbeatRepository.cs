using ElectronicSignage.Domain;
using ElectronicSignage.Domain.Options;
using System;

namespace ElectronicSignage.Repository
{
    public class HeartbeatRepository : GenericRepository<Heartbeat, Guid>
    {
        public HeartbeatRepository(MongoDBOptions mongoDBOptions) : base(mongoDBOptions)
        {
        }
    }
}