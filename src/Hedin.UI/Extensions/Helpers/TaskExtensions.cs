using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace Hedin.UI.Extensions.Helpers
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Invokes an EventCallback as well as awaiting a minimum amount of ms.
        /// For quick callbacks, maximum execution time will be the specified ms
        /// For long callbacks exceeding ms, no time will be awaited.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static async Task InvokeDelayedAsync(this EventCallback func, int milliseconds)
        {
            Stopwatch sw = Stopwatch.StartNew();
            await func.InvokeAsync();

            long timeRemaining = milliseconds - sw.ElapsedMilliseconds;
            if (timeRemaining > 0)
            {
                await Task.Delay((int)timeRemaining);
            }
        }
    }
}
