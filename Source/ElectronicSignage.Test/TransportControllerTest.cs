using ElectronicSignage.Domain.Options;
using ElectronicSignage.Web.Api;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;

namespace ElectronicSignage.Test
{
    public class TransportControllerTest
    {
        private IHttpClientFactory httpClientFactory;
        private TransportOptions transportOptions;

        public TransportControllerTest()
        {
            var httpClient = new HttpClient();

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);
            this.httpClientFactory = httpClientFactory.Object;

            this.transportOptions = new TransportOptions()
            {
                Id = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF",
                Key = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF",
                GetBusStopsUrl = "https://ptx.transportdata.tw/MOTC/v2/Bus/Stop/NearBy?$top=100&$spatialFilter=nearby(25.138749%2C%20121.71844%2C%20100)&$format=JSON",
                GetGetBusEstimateTimesUrl = "https://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/NearBy?$top=100&$spatialFilter=nearby(25.138749%2C%20121.71844%2C%2050)&$format=JSON"
            };
        }

        [Fact]
        public void Test1()
        {
            var transportController = new TransportController(this.transportOptions, this.httpClientFactory);
            var busStops = transportController.GetBusStops();

            var positionLons = new HashSet<double>();
            var positionLats = new HashSet<double>();
            foreach (var busStop in busStops)
            {
                positionLons.Add(busStop.StopPosition.PositionLon);
                positionLats.Add(busStop.StopPosition.PositionLat);
            }

            using (var file = new StreamWriter(@"C:\Users\Webster\Downloads\Test1.csv"))
            {
                foreach (var busStop in busStops)
                    file.WriteLineAsync($"{busStop.StopID},{busStop.StopPosition.PositionLat},{busStop.StopPosition.PositionLon}");
            }
        }

        [Fact]
        public void Test2()
        {
            var transportController = new TransportController(this.transportOptions, this.httpClientFactory);
            var busEstimateTimes = transportController.GetBusEstimateTimes();
        }
    }
}
