using System;

namespace CreaDev.Framework.Core.Helpers
{
    
    public static class TryHelper
    {
        public static TResult Try<TResult>(Func<TResult> func, Action<Exception> logAction = null)
        {
            TResult result = default(TResult);
            try
            {
                result = func.Invoke();
            }
            catch (Exception ex)
            {
                logAction?.Invoke(ex);

            }
            return result;
        }
        public static void Try(Action action,Action<Exception> logAction =null)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                logAction?.Invoke(e);
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