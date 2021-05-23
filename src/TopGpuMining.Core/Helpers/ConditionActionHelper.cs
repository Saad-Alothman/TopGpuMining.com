using System;

namespace TopGpuMining.Core.Helpers
{
    public static class ConditionActionHelper
    {
        public static void DoIf(bool condition, Action action)
        {
            if (condition)
            {
                action.Invoke();
            }
        }
    }

}
