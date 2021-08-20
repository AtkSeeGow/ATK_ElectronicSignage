using System.Runtime.Serialization;

namespace ElectronicSignage.Domain.Transport
{
    /// <summary>
    /// 站牌位置
    /// </summary>
    [DataContract]
    public class PointType
    {
        /// <summary>
        /// 位置經度(WGS84)
        /// </summary>
        [DataMember]
        public double PositionLon { get; set; }

        /// <summary>
        /// 位置緯度(WGS84)
        /// </summary>
        [DataMember]
        public double PositionLat { get; set; }

        /// <summary>
        /// 地理空間編碼
        /// </summary>
        [DataMember]
        public string GeoHash { get; set; }
    }
}
