using ElectronicSignage.Domain;
using ElectronicSignage.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicSignage.Web.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "PermissionHandler")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ToDoRepository toDoRepository;

        public ToDoController(
            ILogger<ToDoController> logger, 
            ToDoRepository toDoRepository)
        {
            this.logger = logger;
            this.toDoRepository = toDoRepository;
        }

        [HttpGet]
        public IEnumerable<ToDo> FetchBy()
        {
            var result = this.toDoRepository.FetchAll().Result.OrderBy(item => item.ExpiryDate).ToList();
            return result;
        }

        [HttpPost]
        public ActionResult SaveBy([FromBody] ToDo toDo)
        {
            var validResult = new ValidResult();

            if (toDoRepository.Exist(toDo.Id))
                toDoRepository.Update(toDo);
            else
                toDoRepository.Create(toDo).Wait();

            if (!validResult.IsValid)
                return BadRequest(validResult);
            else
                return Ok(validResult);
        }

        [HttpPost]
        public ActionResult DeleteBy([FromBody] ToDo toDo)
        {
            toDoRepository.Delete(item => item.Id == toDo.Id).Wait();
            return Ok(new ValidResult());
        }
    }
}
