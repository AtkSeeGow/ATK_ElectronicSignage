using System;
using System.Runtime.Serialization;

namespace ElectronicSignage.Domain.Transport
{
    /// <summary>
    /// 站牌資訊
    /// </summary>
    [DataContract]
    public class BusStop
    {
        /// <summary>
        /// 站牌唯一識別代碼，規則為 {業管機關簡碼} + {StopID}，其中 {業管機關簡碼} 可於Authority API中的AuthorityCode欄位查詢
        /// </summary>
        [DataMember]
        public string StopUID { get; set; }

        /// <summary>
        /// 地區既用中之站牌代碼(為原資料內碼)
        /// </summary>
        [DataMember]
        public string StopID { get; set; }

        /// <summary>
        /// 業管機關代碼
        /// </summary>
        [DataMember]
        public string AuthorityID { get; set; }

        /// <summary>
        /// 站牌名稱
        /// </summary>
        [DataMember]
        public NameType StopName { get; set; }

        /// <summary>
        /// 站牌位置
        /// </summary>
        [DataMember]
        public PointType StopPosition { get; set; }

        /// <summary>
        /// 站牌地址
        /// </summary>
        [DataMember]
        public string StopAddress { get; set; }

        /// <summary>
        /// 方位角，E:東行;W:西行;S:南行;N:北行;SE:東南行;NE:東北行;SW:西南行;NW:西北行
        /// </summary>
        [DataMember]
        public string Bearing { get; set; }

        /// <summary>
        /// 站牌所屬的站位ID
        /// </summary>
        [DataMember]
        public string StationID { get; set; }

        /// <summary>
        /// 站牌所屬的組站位ID
        /// </summary>
        [DataMember]
        public string StationGroupID { get; set; }

        /// <summary>
        /// 站牌詳細說明描述
        /// </summary>
        [DataMember]
        public string StopDescription { get; set; }

        /// <summary>
        /// 站牌權管所屬縣市(相當於市區公車API的City參數)[若為公路/國道客運路線則為空值]
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// 站牌權管所屬縣市之代碼(國際ISO 3166-2 三碼城市代碼)[若為公路/國道客運路線則為空值]
        /// </summary>
        [DataMember]
        public string CityCode { get; set; }

        /// <summary>
        /// 站牌位置縣市之代碼(國際ISO 3166-2 三碼城市代碼)[若為公路/國道客運路線則為空值]
        /// </summary>
        [DataMember]
        public string LocationCityCode { get; set; }

        /// <summary>
        /// 資料更新日期時間(ISO8601格式:yyyy-MM-ddTHH:mm:sszzz)
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 資料版本編號
        /// </summary>
        [DataMember]
        public int VersionID { get; set; }
    }
}