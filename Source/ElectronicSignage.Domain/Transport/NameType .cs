using System.Runtime.Serialization;

namespace ElectronicSignage.Domain.Transport
{
    /// <summary>
    /// 站牌名稱
    /// </summary>
    [DataContract]
    public class NameType
    {
        /// <summary>
        /// 中文繁體名稱
        /// </summary>
        [DataMember]
        public string Zh_tw { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DataMember]
        public string En { get; set; }
    }
}
