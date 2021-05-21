using System;
using System.Collections.Generic;

namespace CreaDev.Framework.Core.IO.Buffers
{
    public class BufferSettings<T>
    {
        public BufferSettings(InsertBufferDelegate insertBufferDelegate, int bufferLimitBeforeInsert, TimeSpan? timeLimitBeforeInsert)
        {
            this.BufferLimitBeforeInsert = bufferLimitBeforeInsert;
            this.Flush = insertBufferDelegate;
            this.TimeLimitBeforeInsert = timeLimitBeforeInsert;
        }
        public delegate void InsertBufferDelegate(List<T> items);
        public int BufferLimitBeforeInsert { get; set; }
        public TimeSpan? TimeLimitBeforeInsert { get; set; }
        public InsertBufferDelegate Flush { get; set; }

    }
}
