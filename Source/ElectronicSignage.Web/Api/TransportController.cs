using ElectronicSignage.Domain.Options;
using ElectronicSignage.Domain.Transport;
using ElectronicSignage.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ElectronicSignage.Web.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "PermissionHandler")]
    public class TransportController : ControllerBase
    {
        private readonly TransportOptions transportOptions;
        private readonly IHttpClientFactory httpClientFactory;

        public TransportController(
            TransportOptions transportOptions,
            IHttpClientFactory httpClientFactory)
        {
            this.transportOptions = transportOptions;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IList<BusStop> GetBusStops()
        {
            var date = DateTime.Now.ToUniversalTime().ToString("r");
            var signatureDate = $"x-date: {date}";
            
            var signature = SecurityUtility.Signature(signatureDate, this.transportOptions.Key);
            var authorization = "hmac username=\"" + this.transportOptions.Id + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + signature + "\"";

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", authorization);
            client.DefaultRequestHeaders.Add("x-date", date);
            
            var responseMessage = client.GetStringAsync(this.transportOptions.GetBusStopsUrl).Result;

            var result = JsonConvert.DeserializeObject<IList<BusStop>>(responseMessage);
            return result;
        }

        [HttpGet]
        public IList<BusEstimateTime> GetBusEstimateTimes()
        {
            var date = DateTime.Now.ToUniversalTime().ToString("r");
            var signatureDate = $"x-date: {date}";

            var signature = SecurityUtility.Signature(signatureDate, this.transportOptions.Key);
            var authorization = "hmac username=\"" + this.transportOptions.Id + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + signature + "\"";

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", authorization);
            client.DefaultRequestHeaders.Add("x-date", date);

            var responseMessage = client.GetStringAsync(this.transportOptions.GetGetBusEstimateTimesUrl).Result;

            var busEstimateTimes = JsonConvert.DeserializeObject<IList<BusEstimateTime>>(responseMessage);

            var result = busEstimateTimes.Where(item =>
                this.transportOptions.StopIds.Contains(item.StopID) &&
                item.EstimateTime != 0 &&
                item.PlateNumb != "-1").ToList();
            return result;
        }
    }
}
