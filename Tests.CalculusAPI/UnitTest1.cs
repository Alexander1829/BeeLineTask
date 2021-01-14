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
        /// ������ ����� ��� Current � Items, ���� ��� �� ���� �������
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
            Mock<CalculationsSingl> mockICalculationsSingl = new Mock<CalculationsSingl>(); //������� �� ����������
            //������� ���� ���������� Current �� ����� (� ������������ ������)
            while (true)
            {
                Thread.Sleep(1);
                if (mockICalculationsSingl.Object.IsCurrentUploaded)
                    break;
            }

            Mock<WriteQueueSingl> mockIWriteQueueSingl = new Mock<WriteQueueSingl>(); //������� �� ������             

            CalcScheduler scheduler = new CalcScheduler(mockICalculationsSingl.Object, mockIWriteQueueSingl.Object); //������� ����������� � ������������ (� ������������ �������)
            scheduler.StartAsync(new CancellationToken()); //������� �������� �����            

            var implement = new GeneralControllerImpl(mockICalculationsSingl.Object);
            GeneralController controller = new GeneralController(null, implement); 

            var getActionResult = controller.Get(); //�������� Current, Total 
            var getResult = getActionResult.Result as OkObjectResult;
            //���� �� ����� ����� ��: �����-�� �������� �� ����� (���� 0 ���� � ���� ������ ������ �� ����������). 
            //�������� StatusCode
            Assert.IsTrue(getResult.StatusCode == 200);
            
            //�������� ��������� ��������, 17��: 
            controller.PostCalc(new Action_Data() { Action = "*", Data = 0 }); //Current �� ����� ������� �� 0, ������� 0
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
            //������� ���� ������� �� ���������
            //���� �������� ���������, ����� �� ����� �������.
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

            Console.WriteLine("������� ����� ������� " + step);//������� ����� �������
        }
    }
}
