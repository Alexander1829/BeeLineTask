using Calculus.APIImpl.Abstract;
using Calculus.DTO;
using Calculus.DTO.Config;
using Calculus.DTO.Singltones;
using Calculus.Servs;
using Calculus.Servs.Schedulers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        /// Получить Total
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































    /// <summary>
    /// Контроллер для прогонки тестовых данных
    /// </summary>
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ICalculationsSingl _calculations;
        public TestController(ICalculationsSingl calculations)
        {
            _calculations = calculations;
        }


        [HttpGet]
        public /*string */void Get()
        {


        }

        ///// <summary>
        ///// Возвращает "Ok"
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public /*string */void Get()
        //{
        //    //Scheduler1 s = new Scheduler1(_calculations);
        //    //s.SchedullerWork();

        //    ////return "Ok";

        //    {

        //        var cmd = "<Item><Action>*</Action><Data>2</Data></Item>";
        //        cmd = cmd.Replace("/", "\\/");
        //        //var cmd = "121234";
        //        var process = new Process()
        //        {
        //            StartInfo = new ProcessStartInfo
        //            {
        //                //FileName = "bash",                        
        //                //FileName = "C:\\Program Files\\Git\\git-bash.exe",
        //                FileName = "bash",
        //                //Arguments = string.Format($"sed -i '1s/^/{s.ToString()}/' {AppConfig.FileName_Items}")
        //                //Arguments = string.Format($"sed -i '1s/^/asd/' DataStore_Items.xml"),
        //                Arguments = string.Format($"-c \"sed -i '1s/^/{cmd}/' /C/Users/User/source/repos/CalculusSite/Calculus.API/f.txt\""),
        //                //""
        //                CreateNoWindow = true,
        //                RedirectStandardError = true,
        //                UseShellExecute = false
        //            }
        //        };

        //        try
        //        {
        //            process.Start();
        //        }
        //        catch (Win32Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Message;
        //        }
        //        catch (Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Data.ToString();
        //        }

        //        try
        //        {
        //            Process.Start("cmd");
        //        }
        //        catch (Exception e)
        //        {
        //        }

        //        try
        //        {
        //            Process.Start("bash");
        //        }
        //        catch (Exception e)
        //        {
        //        }

        //        try
        //        {
        //            Process.Start("git-bash");
        //        }
        //        catch (Exception e)
        //        {
        //        }

        //    }

        //    {
        //        var process = new Process()
        //        {
        //            StartInfo = new ProcessStartInfo
        //            {
        //                FileName = "git-bash",
        //                //Arguments = string.Format($"sed -i '1s/^/{s.ToString()}/' {AppConfig.FileName_Items}")
        //                //Arguments = string.Format($"sed -i '1s/^/asd/' DataStore_Items.xml"),
        //                Arguments = string.Format($"-c \"sed -i '1s/^/s.ToString()/' /C/Users/User/source/repos/CalculusSite/Calculus.API/f.txt\""),
        //                //""
        //                CreateNoWindow = true,
        //                RedirectStandardError = true,
        //                UseShellExecute = false /*!=true*/
        //            }
        //        };

        //        try
        //        {
        //            process.Start();
        //        }
        //        catch (Win32Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Message;
        //        }
        //        catch (Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Data.ToString();
        //        }
        //    }

        //    {
        //        var process = new Process()
        //        {
        //            StartInfo = new ProcessStartInfo
        //            {
        //                FileName = "bash",
        //                WorkingDirectory = "C:\\Users\\User\\source\\repos\\CalculusSite\\Calculus.API",
        //                //Arguments = string.Format($"sed -i '1s/^/{s.ToString()}/' {AppConfig.FileName_Items}")
        //                //Arguments = string.Format($"sed -i '1s/^/asd/' DataStore_Items.xml"),
        //                Arguments = string.Format($"touch f.txt"),
        //                CreateNoWindow = true,
        //                RedirectStandardError = true,
        //                UseShellExecute = false
        //            }
        //        };

        //        try
        //        {
        //            process.Start();
        //        }
        //        catch (Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Data.ToString();
        //        }
        //    }

        //    {
        //        var process = new Process()
        //        {
        //            StartInfo = new ProcessStartInfo
        //            {
        //                FileName = "C:\\Program Files\\Git\\git-bash.exe",
        //                WorkingDirectory = "C:\\Users\\User\\source\\repos\\CalculusSite\\Calculus.API",
        //                //Arguments = string.Format($"sed -i '1s/^/{s.ToString()}/' {AppConfig.FileName_Items}")
        //                //Arguments = string.Format($"sed -i '1s/^/asd/' DataStore_Items.xml"),
        //                Arguments = $"-c \"touch f.txt\"",
        //                CreateNoWindow = true,
        //                RedirectStandardError = true,
        //                UseShellExecute = false,
        //            }
        //        };

        //        try
        //        {
        //            process.Start();
        //        }
        //        catch (Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Data.ToString();
        //        }
        //    }

        //    {
        //        var process = new Process()
        //        {
        //            StartInfo = new ProcessStartInfo
        //            {
        //                FileName = "C:\\Program Files\\Git\\git-bash.exe",
        //                WorkingDirectory = "C:\\Users\\User\\source\\repos\\CalculusSite\\Calculus.API",
        //                //Arguments = string.Format($"sed -i '1s/^/{s.ToString()}/' {AppConfig.FileName_Items}")
        //                //Arguments = string.Format($"sed -i '1s/^/asd/' DataStore_Items.xml"),
        //                Arguments = string.Format($"touch f.txt"),
        //                CreateNoWindow = true,
        //                RedirectStandardError = true,
        //                UseShellExecute = false
        //            }
        //        };

        //        try
        //        {
        //            process.Start();
        //        }
        //        catch (Exception e)
        //        {
        //            var m = e.ToString();
        //            var m2 = e.Data.ToString();
        //        }
        //    }

        //}

        /// <summary>
        /// Тестовый метод.
        /// Добавить в очередь size/2 слогаемых со знаком + или -.
        /// По умолчанию size/2 = 2500.
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="size"></param>
        [HttpGet, Route("addtest1")]
        public void AddTest1(int sign, int size = 5000)
        {
            //lock (_calculations)
            //{
            if (sign == 0) //Plus
            {
                for (int i = 0; i < size; i++)
                {
                    if (i % 2 == 0)
                        _calculations.Items.Add(new Action_Data() { Data = i, Action = "+" });
                    else
                    { }//Thread.Sleep(2); //добавим  паузу, на нечётном шаге                           
                }
            }
            else if (sign == 1) //Minus
            {
                for (int i = 0; i < size; i++)
                {
                    if (i % 2 == 0)
                        _calculations.Items.Add(new Action_Data() { Data = i, Action = "-" });
                    else
                    { }//Thread.Sleep(2); //добавим  паузу, на нечётном шаге                            
                }
            }
            _calculations.ShouldCalculate = true;
            //}
        }

        /// <summary>
        /// Тестовый метод.
        /// Добавить в очередь несколько делений на 2. По умолчанию 20.
        /// </summary>
        /// <param name="size"></param>
        [HttpGet, Route("addtest2")]
        public void AddTest2(int size = 20)
        {
            for (int i = 1; i <= size; i++)
            {
                _calculations.Items.Add(new Action_Data() { Data = 2, Action = "/" });
            }
            _calculations.ShouldCalculate = true;
        }

        /// <summary>
        /// Очистить синглтон.
        /// </summary>
        [HttpGet, Route("clear")]
        public void Clear()
        {
            _calculations.Items.Clear();
            _calculations.Current.Total = 0;
        }
    }
}
