using System.Runtime.Serialization;

namespace ElectronicSignage.Domain
{
    /// <summary>
    /// 授權資料
    /// </summary>
    [DataContract]
    public class Authorization : AbstractDomain
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        #region Public
        
        #endregion
    }
}
