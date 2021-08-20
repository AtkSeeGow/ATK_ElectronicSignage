using AutoMapper;
using ElectronicSignage.Domain;
using ElectronicSignage.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;

namespace ElectronicSignage.Web.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "PermissionHandler")]
    public class HeartbeatController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HeartbeatRepository heartbeatRepository;

        public HeartbeatController(
            ILogger<HeartbeatController> logger,
            IHttpClientFactory httpClientFactory,
            HeartbeatRepository heartbeatRepository)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.heartbeatRepository = heartbeatRepository;
        }

        [HttpPost]
        public IEnumerable<Heartbeat> FetchBy()
        {
            var result = this.heartbeatRepository.FetchAll().Result.OrderBy(item => item.Name).ToList();
            return result;
        }

        [HttpGet]
        public IEnumerable<Heartbeat> GetStatus()
        {
            var result = new List<HeartbeatViewModel>();

            var heartbeats = this.heartbeatRepository.FetchAll().Result.OrderBy(item => item.Name).ToList();
            foreach(var heartbeat in heartbeats)
            {
                var heartbeatViewModel = HeartbeatViewModel.CreateInstance(heartbeat);
                result.Add(heartbeatViewModel);

                var request = new HttpRequestMessage(HttpMethod.Get, heartbeat.Url);

                try
                {
                    var client = httpClientFactory.CreateClient("Heartbeat");
                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                        heartbeatViewModel.Status = "正常";
                    else
                        heartbeatViewModel.Status = "異常";
                }
                catch (Exception)
                {
                    heartbeatViewModel.Status = "異常";
                }

            }
            return result;
        }

        [HttpPost]
        public ActionResult SaveBy([FromBody] Heartbeat heartbeat)
        {
            var validResult = new ValidResult();

            if (heartbeatRepository.Exist(heartbeat.Id))
                heartbeatRepository.Update(heartbeat);
            else
                heartbeatRepository.Create(heartbeat).Wait();

            if (!validResult.IsValid)
                return BadRequest(validResult);
            else
                return Ok(validResult);
        }

        [HttpPost]
        public ActionResult DeleteBy([FromBody] Heartbeat heartbeat)
        {
            heartbeatRepository.Delete(item => item.Id == heartbeat.Id).Wait();
            return Ok(new ValidResult());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class HeartbeatViewModel : Heartbeat
    {
        public static HeartbeatViewModel CreateInstance(Heartbeat heartbeat)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Heartbeat, HeartbeatViewModel>());
            var mapper = config.CreateMapper();

            var heartbeatViewModel = mapper.Map<HeartbeatViewModel>(heartbeat);

            return heartbeatViewModel;
        }

        [DataMember]
        public string Status { get; set; }
    }
}
