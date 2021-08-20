using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ElectronicSignage.Domain
{
    /// <summary>
    /// 驗證結果
    /// </summary>
    [DataContract]
    public class ValidResult
    {
        /// <summary>
        /// 是否驗證通過
        /// </summary>
        public bool IsValid
        {
            get
            {
                isValid = this.ErrorMessages.Count == 0;
                return isValid;
            }
        }

        [DataMember]
        public bool isValid;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> SuccessMessages
        {
            get
            {
                if (successMessages == null)
                    this.successMessages = new Dictionary<string, string>();
                return this.successMessages;
            }
        }

        [DataMember]
        private Dictionary<string, string> successMessages;

        /// <summary>
        /// 驗證錯誤訊息
        /// </summary>
        public Dictionary<string, string> ErrorMessages
        {
            get
            {
                if (errorMessages == null)
                    this.errorMessages = new Dictionary<string, string>();
                return this.errorMessages;
            }
        }

        [DataMember]
        private Dictionary<string, string> errorMessages;

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            var x = this.IsValid;
            var y = this.ErrorMessages;
            var z = this.SuccessMessages;
        }
    }
}
