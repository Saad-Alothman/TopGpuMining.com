using System;

namespace CreaDev.Framework.Core.Helpers
{
    
    public static class TryHelper
    {
        public static TResult Try<TResult>(Func<TResult> func)
        {
            TResult result = default(TResult);
            try
            {
                result = func.Invoke();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static void Try(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {

            }
        }
    }

    public static class ConditionActionHelper
    {
        public static void DoIf(bool condition,Action action)
        {
            if (condition)
            {
                action.Invoke();
            }
        }
    }
}