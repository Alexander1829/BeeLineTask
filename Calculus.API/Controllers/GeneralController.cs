using Calculus.APIImpl.Abstract;
using Calculus.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Calculus.API.Controllers
{
    /// <summary>
    /// Основной контроллер
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralControllerImpl _implementation;
        private readonly ILogger<GeneralController> _logger;

        public GeneralController(ILogger<GeneralController> logger, IGeneralControllerImpl implement)
        {
            _implementation = implement;
            _logger = logger;
        }

        /// <summary>
        /// Получить Current, Total 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Action_Data_Total> Get()
        {
            try
            {
                var result = _implementation.Get();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString(), null);
                return new JsonResult(e.ToString()) { StatusCode = 503 };
            }
        }

        /// <summary>
        /// Добавить операцию в очередь на выполнение
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostCalc([FromBody] Action_Data body)
        {
            try
            {
                _implementation.PostCalc(body);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString(), null);
                return new JsonResult(e.ToString()) { StatusCode = 503 };
            }
        }
    }
}

