//////////////////
///Barton Joe
//////////////////
using System;
using System.Runtime.InteropServices;

namespace AutoZKernel
{
    public class AutoZController
    {
        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(ExecutionFlag flags);
        [Flags]
        enum ExecutionFlag : uint
        {
            System = 0x00000001,
            Display = 0x00000002,
            Continus = 0x80000000,
        }
        public static void PreventSleep(bool includeDisplay)
        {
            if (includeDisplay)
            {
                SetThreadExecutionState(ExecutionFlag.System | ExecutionFlag.Display | ExecutionFlag.Continus);
            }
            else
            {
                SetThreadExecutionState(ExecutionFlag.System | ExecutionFlag.Continus);
            }
        }
        public static void ResotreSleep()
        {
            SetThreadExecutionState(ExecutionFlag.Continus);
        }
    }
}
