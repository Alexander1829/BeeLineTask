﻿using Calculus.APIImpl.Abstract;
using Calculus.DTO;
using Calculus.DTO.Config;
using Calculus.DTO.Singltones;
using Calculus.Servs;
using System.IO;
using System.Threading.Tasks;

namespace Calculus.APIImpl.Realisation
{
    /// <summary>
    /// Логика основного конторллера
    /// </summary>
    public class GeneralControllerImpl : IGeneralControllerImpl
    {
        private readonly ICalculationsSingl _calculations;
        public GeneralControllerImpl(ICalculationsSingl calculations)
        {
            _calculations = calculations;
        }

        /// <summary>
        /// Получить Total
        /// </summary>
        /// <returns></returns>
        public Action_Data_Total Get()
        {
            return _calculations.Current;
        }

        /// <summary>
        /// Добавить операцию в очередь на выполнение
        /// </summary>
        /// <param name="body"></param>
        public void PostCalc(Action_Data body)
        {
            Task.Run(() =>
            {
                lock (_calculations)
                {
                    _calculations.Items.Add(new Action_Data() { Action = body.Action, Data = body.Data });
                    _calculations.ShouldCalculate = true;
                }
            });
        }
    }
}
