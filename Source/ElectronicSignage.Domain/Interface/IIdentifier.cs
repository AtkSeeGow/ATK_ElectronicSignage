using System;

namespace ElectronicSignage.Domain.Interface
{
    public interface IIdentifier<TIdType> where TIdType : IEquatable<TIdType>
    {
        TIdType Id
        {
            get;
        }
    }
}