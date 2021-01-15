using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calculus.DTO;
using Calculus.DTO.Config;
using Calculus.DTO.Singltones;
using Calculus.DTO.Xml;
using Calculus.SystServs;

namespace Calculus.Servs.Schedulers
{
    /// <summary>
    /// Служба высчитывает Current из синглтона. И перекачивает данные на жесткий диск                        
    /// </summary>
    public class CalcScheduler : BackgroundScheduler
    {
        private readonly ICalculationsSingl _calculations;
        private readonly IWriteQueueSingl _writeQueue;

        public CalcScheduler(ICalculationsSingl calculations, IWriteQueueSingl writeQueue)
        {
            _calculations = calculations;
            _writeQueue = writeQueue;
        }

        /// <summary>
        /// Основной таск шедулера
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                Thread.Sleep(1);    //аналог реализации  await Task.Delay(1, stoppingToken);                            
                await SchedullerCalculator();
                await SchedullerWtiteInFiles();
            }
        }

        /// <summary>
        /// Расчитывает Total и отправляет данные на запись в файлы.
        /// </summary>
        public async Task SchedullerCalculator()
        {
            await Task.Run(() =>
            {
                if (_calculations.ShouldCalculate)
                {
                    lock (_calculations)
                    {
                        _calculations.ShouldCalculate = false;
                        var calcResult = CalculateTotal();
                        _writeQueue.WriteQueue.Add(calcResult);
                        _writeQueue.FilesShouldReload = true;
                    }
                }
            });
        }

        /// <summary>
        /// Просматривает очередь на запись в файлы. И если в ней что-то есть, то обрабатывает её, обновляя файлы
        /// </summary>
        public async Task SchedullerWtiteInFiles()
        {
            await Task.Run(() =>
            {
                if (_writeQueue.FilesShouldReload)
                {
                    lock (_writeQueue)
                    {
                        _writeQueue.FilesShouldReload = false;
                        if (_writeQueue.WriteQueue.Count > 0)
                        {
                            XmlCalcCurrent xmlCalcCurrent = new XmlCalcCurrent();
                            xmlCalcCurrent.Current = _writeQueue.WriteQueue[_writeQueue.WriteQueue.Count - 1].Current;//Current берём последний, из добавленных в очередь на запись
                            string textCurrent = xmlCalcCurrent.SerializeXmlToStr();
                            File.WriteAllTextAsync(AppConfig.FilePath_Current, textCurrent); //Current - полностью перезапишем файл

                            var fs = new FileServ();
                            fs.AppendAtBeginFile(AppConfig.FilePath_Items, _writeQueue.QueueToFileText()); //Items - ДОписываем в начало файла, в обратном порядке
                        }
                        _writeQueue.WriteQueue.Clear();
                    }
                }
            });
        }

        /// <summary>
        /// Вычисление Total. И сборка данных для записи в файлы.
        /// </summary>
        private CalcResult CalculateTotal()
        {
            CalcResult calcResult = null; //набор для записи в файл
            if (_calculations.Items.Count > 0)
            {
                calcResult = new CalcResult();
                //Проходим по всем невыполненным действиям из Items. Выполняем их. После, переносим их в Items.
                for (int i = 0; i < _calculations.Items.Count; i++)
                {
                    if (_calculations.Items[i].Action == "+")
                        _calculations.Current.Total += _calculations.Items[i].Data;
                    else if (_calculations.Items[i].Action == "-")
                        _calculations.Current.Total -= _calculations.Items[i].Data;
                    else if (_calculations.Items[i].Action == "*")
                        _calculations.Current.Total *= _calculations.Items[i].Data;
                    else if (_calculations.Items[i].Action == "/")
                        if (_calculations.Items[i].Data != 0)
                            _calculations.Current.Total /= _calculations.Items[i].Data;
                        else //На ноль не делим :)
                            throw new DivideByZeroException();
                }
                //в Items, для файла, сразу добавим операцию из текущего Current                 
                if (!string.IsNullOrEmpty(_calculations.Current.Action))//(если это не пустая, самая первая операция)
                {
                    calcResult.Items.Add(new Action_Data()
                    {
                        Action = _calculations.Current.Action,
                        Data = _calculations.Current.Data
                    });
                }
                // и добавим все новые операции, не включая последнюю :
                calcResult.Items.AddRange(_calculations.Items.GetRange(0, _calculations.Items.Count - 1));
                //Текущий Current заменим на последнюю новую операцию :
                _calculations.Current.Action = _calculations.Items[_calculations.Items.Count - 1].Action;
                _calculations.Current.Data = _calculations.Items[_calculations.Items.Count - 1].Data;
                calcResult.Current = _calculations.Current; //новый Current, для файла

                _calculations.Items.Clear(); //очищаем Items в синглтоне _calculations.                 
            }
            return calcResult;
        }
    }
}
