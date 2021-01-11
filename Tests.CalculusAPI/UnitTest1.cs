using Calculus.APIImpl.Abstract;
using Calculus.APIImpl.Realisation;
using Calculus.DTO;
using Calculus.Servs.Schedulers;
using Calculus.API;
using Calculus.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Calculus.DTO.Singltones;

namespace Tests.CalculusAPI
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        public void TestMethod1()
        {
            Mock<CalculationsSingl> mockICalculationsSingl = new Mock<CalculationsSingl>();
            Mock<WriteQueueSingl> mockIWriteQueueSingl = new Mock<WriteQueueSingl>();
            CalcScheduler s = new CalcScheduler(mockICalculationsSingl.Object, mockIWriteQueueSingl.Object);
            s.StartAsync(new CancellationToken());

            var implement = new GeneralControllerImpl(mockICalculationsSingl.Object);
            GeneralController controller = new GeneralController(null, implement);
            var actionResult = controller.Get();

            var result = actionResult.Result as OkObjectResult;
            Assert.IsTrue(result.StatusCode == 200);
            //Проверяем: вначале Total равен 0
            Assert.IsTrue(((Action_Data_Total)result.Value).Total == 0);

            var iCalculationsSinglItems = new List<Action_Data>() {
                new Action_Data { Action = "+", Data =100 },
                new Action_Data { Action = "+", Data =100 },
                new Action_Data { Action = "+", Data =100 },
            };
            mockICalculationsSingl.Object.Items = iCalculationsSinglItems;
            mockICalculationsSingl.Object.ShouldCalculate = true;

            implement = new GeneralControllerImpl(mockICalculationsSingl.Object);
            controller = new GeneralController(null, implement);
            actionResult = controller.Get();
            result = actionResult.Result as OkObjectResult;

            Assert.AreEqual(300, ((Action_Data_Total)result.Value).Total);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Mock<ICalculationsSingl> mockICalculationsSingl = new Mock<ICalculationsSingl>();
            Mock<WriteQueueSingl> mockIWriteQueueSingl = new Mock<WriteQueueSingl>();
            mockICalculationsSingl.Setup(l => l.Current).Returns(new Action_Data_Total() { Total = 0 });
            var iCalculationsSinglItems = new List<Action_Data>();            
            mockICalculationsSingl.Setup(l => l.Items).Returns(iCalculationsSinglItems);

            //Mock<CalculationsSingl> mockICalculationsSingl = new Mock<CalculationsSingl>();
            //mockICalculationsSingl.Setup(l => l.FilesShouldReload).Returns(true);            
            //mockICalculationsSingl.Object.Current = new Action_Data_Total() { Total = 0};
            
            CalcScheduler s = new CalcScheduler(mockICalculationsSingl.Object, mockIWriteQueueSingl.Object);
            s.StartAsync(new CancellationToken());



            var implement = new GeneralControllerImpl(mockICalculationsSingl.Object);
            GeneralController controller = new GeneralController(null, implement);
            var actionResult = controller.Get();

            var result = actionResult.Result as OkObjectResult;
            Assert.IsTrue(result.StatusCode == 200);
            //Проверяем: вначале Total равен 0
            Assert.IsTrue(((Action_Data_Total)result.Value).Total == 0);

            //mockICalculationsSingl.Setup(l => l.Current).Returns(new Action_Data_Total() { Total = 0 });
            //mockICalculationsSingl.Setup(l => l.FilesShouldReload).Returns(true);            

            iCalculationsSinglItems.Add(new Action_Data { Action = "+", Data = 100 });
            iCalculationsSinglItems.Add(new Action_Data { Action = "+", Data = 100 });
            iCalculationsSinglItems.Add(new Action_Data { Action = "+", Data = 100 });
            iCalculationsSinglItems.Add(new Action_Data { Action = "/", Data = 2 });
            mockICalculationsSingl.Setup(l => l.Items).Returns(iCalculationsSinglItems);
            mockICalculationsSingl.Setup(l => l.ShouldCalculate).Returns(true);

            implement = new GeneralControllerImpl(mockICalculationsSingl.Object);
            controller = new GeneralController(null, implement);
            actionResult = controller.Get();
            result = /*controller.Get()*/actionResult.Result as OkObjectResult;

            Assert.AreEqual(150, ((Action_Data_Total)result.Value).Total);
        }

        //[TestMethod]
        public void TestMethod()
        {
            int i = 0;
            i++;
            Assert.IsTrue(i == 1);            
        }
    }
}
