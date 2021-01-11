using Calculus.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculus.APIImpl.Abstract
{
    public interface IGeneralControllerImpl
    {
        /// <summary>
        /// Получить Total
        /// </summary>
        /// <returns></returns>
        Action_Data_Total Get();

        /// <summary>
        /// Добавить операцию в очередь на выполнение
        /// </summary>
        /// <param name="body"></param>
        void PostCalc(Action_Data body);
    }
}
