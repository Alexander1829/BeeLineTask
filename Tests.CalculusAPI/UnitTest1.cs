using Calculus.APIImpl.Realisation;
using Calculus.DTO;
using Calculus.Servs.Schedulers;
using Calculus.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using Calculus.DTO.Singltones;
using Calculus.DTO.Config;
using System.IO;

namespace Tests.CalculusAPI
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Создаёт файлы для Current и Items, если они не были созданы
        /// </summary>
        [TestInitialize]
        public void TestInit() 
        {
            if (!File.Exists(AppConfig.FilePath_Current))
                File.Create(AppConfig.FilePath_Current);
            if (!File.Exists(AppConfig.FilePath_Items))
                File.Create(AppConfig.FilePath_Items);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Mock<CalculationsSingl> mockICalculationsSingl = new Mock<CalculationsSingl>(); //очередь на вычисление
            //Дождёмся пока загрузится Current из файла (в параллельном потоке)
            while (true)
            {
                Thread.Sleep(1);
                if (mockICalculationsSingl.Object.IsCurrentUploaded)
                    break;
            }

            Mock<WriteQueueSingl> mockIWriteQueueSingl = new Mock<WriteQueueSingl>(); //очередь на запись             

            CalcScheduler scheduler = new CalcScheduler(mockICalculationsSingl.Object, mockIWriteQueueSingl.Object); //Шедулер вычисляющий и записывающий (в параллельных потоках)
            scheduler.StartAsync(new CancellationToken()); //Шедулер запустим сразу            

            var implement = new GeneralControllerImpl(mockICalculationsSingl.Object);
            GeneralController controller = new GeneralController(null, implement); 

            var getActionResult = controller.Get(); //Запросим Current, Total 
            var getResult = getActionResult.Result as OkObjectResult;
            //Пока не знаем какой он: какое-то значение из файла (либо 0 если в файл раньше ничего не записывали). 
            //Проверим StatusCode
            Assert.IsTrue(getResult.StatusCode == 200);
            
            //Отправим несколько запросов, 17шт: 
            controller.PostCalc(new Action_Data() { Action = "*", Data = 0 }); //Current из файла умножим на 0, получим 0
            controller.PostCalc(new Action_Data() { Action = "+", Data = 200 }); //200
            controller.PostCalc(new Action_Data() { Action = "/", Data = 20 }); //10
            controller.PostCalc(new Action_Data() { Action = "*", Data = 20 }); //200
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //201
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //202
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //203
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //204
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //205
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //206
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //207
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //208
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //209
            controller.PostCalc(new Action_Data() { Action = "+", Data = 1 }); //210
            controller.PostCalc(new Action_Data() { Action = "+", Data = 10.789 }); //220.789
            controller.PostCalc(new Action_Data() { Action = "-", Data = 10 }); //210.789
            controller.PostCalc(new Action_Data() { Action = "+", Data = 0 }); //210.789
            mockIWriteQueueSingl.Object.FilesShouldReload = true;
            //Дождёмся пока шедулер всё досчитает
            //ради интереса посмотрим, долго ли будет считать.
            int step = 0;
            while (true)
            {
                Thread.Sleep(1);
                step++;
                if (mockIWriteQueueSingl.Object.FilesShouldReload == false)
                    break;
            }
            getActionResult = controller.Get();
            getResult = getActionResult.Result as OkObjectResult;

            Assert.AreEqual(210.789, ((Action_Data_Total)getResult.Value).Total);            
            Assert.AreEqual(0, ((Action_Data_Total)getResult.Value).Data);
            Assert.AreEqual("+", ((Action_Data_Total)getResult.Value).Action);

            Console.WriteLine("Столько ждали шедулер " + step);//столько ждали шедулер
        }
    }
}
