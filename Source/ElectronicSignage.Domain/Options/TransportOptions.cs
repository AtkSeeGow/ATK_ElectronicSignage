using System.Collections.Generic;

namespace ElectronicSignage.Domain.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class TransportOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GetBusStopsUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GetGetBusEstimateTimesUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> StopIds { get; set; }
    }
}