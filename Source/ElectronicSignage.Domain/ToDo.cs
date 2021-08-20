using System;
using System.Runtime.Serialization;

namespace ElectronicSignage.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ToDo: AbstractDomain
    {
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        [DataMember]
        public DateTime? ExpiryDate { get; set; }
    }
}
