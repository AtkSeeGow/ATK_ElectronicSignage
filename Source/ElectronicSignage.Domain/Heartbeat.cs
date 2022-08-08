using System.Runtime.Serialization;

namespace ElectronicSignage.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Heartbeat: AbstractDomain
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Url { get; set; }
    }
}
