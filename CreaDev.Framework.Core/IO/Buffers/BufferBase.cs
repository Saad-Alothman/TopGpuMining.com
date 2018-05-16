using System;
using System.Collections.Generic;

namespace CreaDev.Framework.Core.IO.Buffers
{
    public class BufferBase<T>
    {
        public virtual void LogEntry(T entry)
        {

        }
        public static List<T> BufferList;
        public BufferSettings<T> Settings { get; set; }
        private System.DateTime? _lastInsertTime { get; set; }
       // protected static FeeehLogger _log;
        public BufferBase(BufferSettings<T> settings)
        {
            if (BufferList == null)
            {
                BufferList = new List<T>();
            }
            this.Settings = settings;
            //if (_log == null)
            //{
            //    _log = new FeeehLogger(typeof(BufferBase<T>));
            //}
        }
        public void RegisterItem(T item)
        {
            lock (BufferList)
            {
                LogEntry(item);
                BufferList.Add(item);
                ItemBuffered();
            }



        }
        private void ItemBuffered()
        {
            var isLimitReached = IsLimitReached();

            if (isLimitReached)
            {
                TryFlush();
            }
        }

        public void TryFlush()
        {
            try
            {
                Flush();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void LogError(Exception ex)
        {

        }

       

        public void Flush()
        {
            lock (BufferList)
            {
                ////_log.Info("Inserting Buffer count:" + BufferList.Count);
                
                Settings.Flush(BufferList);
                //if (result.IsSuccess)
                //{
                    //_log.Info("Inserting Buffer Success");

                    BufferList.Clear();
                    _lastInsertTime = DateTime.Now;
                //}
                //else
                //{
                //    //_log.Error("Inserting Buffer failed");

                //}
            }
        }
        private bool IsLimitReached()
        {
            bool isLimitReached = false;

            if (BufferList.Count >= Settings.BufferLimitBeforeInsert)
            {
                isLimitReached = true;
            }
            if (_lastInsertTime.HasValue && Settings.TimeLimitBeforeInsert.HasValue &&
                DateTime.Now.Subtract(_lastInsertTime.Value).TotalMinutes > Settings.TimeLimitBeforeInsert.Value.TotalSeconds)
            {
                isLimitReached = true;
            }

            return isLimitReached;
        }

        public void RegisterItems(List<T> items)
        {
            lock (BufferList)
            {
                BufferList.AddRange(items);
                ItemBuffered();
            }


        }


    }
}
