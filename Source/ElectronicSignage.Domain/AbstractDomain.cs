using ElectronicSignage.Domain.Interface;
using System;
using System.Runtime.Serialization;

namespace ElectronicSignage.Domain
{
    [DataContract]
    public partial class AbstractDomain : IIdentifier<Guid>
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
