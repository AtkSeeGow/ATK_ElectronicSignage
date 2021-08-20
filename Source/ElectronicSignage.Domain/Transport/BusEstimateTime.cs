using System.Runtime.Serialization;

namespace ElectronicSignage.Domain.Transport
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BusEstimateTime : Estimate
    {
        /// <summary>
        /// 地區既用中之站牌代碼(為原資料內碼)
        /// </summary>
        [DataMember]
        public string StopID { get; set; }

        /// <summary>
        /// 車輛距離本站站數
        /// </summary>
        [DataMember]
        public int StopCountDown { get; set; }

        /// <summary>
        /// 車輛狀態備註:[0:'正常',1:'尚未發車',2:'交管不停靠',3:'末班車已過',4:'今日未營運']
        /// </summary>
        [DataMember]
        public int StopStatus { get; set; }

        /// <summary>
        /// 路線名稱
        /// </summary>
        [DataMember]
        public NameType RouteName { get; set; }

        /// <summary>
        /// 子路線名稱
        /// </summary>
        [DataMember]
        public NameType SubRouteName { get; set; }
    }
}
