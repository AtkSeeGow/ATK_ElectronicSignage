using System.Runtime.Serialization;

namespace ElectronicSignage.Domain.Transport
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Estimate
    {
        /// <summary>
        /// 車輛車牌號碼
        /// </summary>
        [DataMember]
        public string PlateNumb { get; set; }

        /// <summary>
        /// 車輛之到站時間預估(秒)
        /// </summary>
        [DataMember]
        public int EstimateTime { get; set; }

        /// <summary>
        /// 是否為末班車
        /// </summary>
        [DataMember]
        public bool IsLastBus { get; set; }

        /// <summary>
        /// 車輛於該站之進離站狀態 : [0:'離站',1:'進站']
        /// </summary>
        [DataMember]
        public int VehicleStopStatus { get; set; }
    }
}
