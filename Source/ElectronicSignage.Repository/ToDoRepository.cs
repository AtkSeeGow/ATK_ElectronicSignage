using ElectronicSignage.Domain;
using ElectronicSignage.Domain.Options;
using System;

namespace ElectronicSignage.Repository
{
    public class ToDoRepository : GenericRepository<ToDo, Guid>
    {
        public ToDoRepository(MongoDBOptions mongoDBOptions) : base(mongoDBOptions)
        {
        }
    }
}