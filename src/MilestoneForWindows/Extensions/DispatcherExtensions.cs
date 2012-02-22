using System;
using System.Windows.Threading;

namespace MilestoneForWindows.Extensions
{
    public static class DispatcherExtensions
    {
        public static void Execute(this Dispatcher dispatcher, Action action, params object[] args)
        {
            dispatcher.Invoke(action, DispatcherPriority.Background, args);
        }

        public static void ExecuteAsync(this Dispatcher dispatcher, Action action, params object[] args)
        {
            dispatcher.BeginInvoke(action, DispatcherPriority.Background, args);
        }

        public static void ExecuteAsync(this Dispatcher dispatcher, DispatcherPriority priority, Action action, params object[] args)
        {
            dispatcher.BeginInvoke(action, priority, args);
        }
    }
}
