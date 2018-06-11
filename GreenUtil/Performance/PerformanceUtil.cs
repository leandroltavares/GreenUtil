using System;
using System.Diagnostics;

namespace GreenUtil.Performance
{
    /// <summary>
    /// Classe relacionada a lógicas de desempenho
    /// </summary>
    public static class PerformanceUtil
    {
        /// <summary>
        /// Método para medir desempenho de uma <see cref="Action"/>
        /// </summary>
        /// <param name="action"><see cref="Action"/> a ser executada</param>
        /// <param name="time"><see cref="TimeSpan"/> medido</param>
        public static void MeasureTime(Action action, out TimeSpan time)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            stopWatch.Stop();

            time = stopWatch.Elapsed;
        }
    }
}
