using System;
using wls.Buffer;

namespace wls
{
    class Program
    {
        static void Main()
        {
            var km = new KeysMonitor(new FileFlash(Config.LogFileName, Config.LogFileDatePattern), new SmartBufferProcessor())
                {
                    FlashInterval = Config.FlashInterval
                };
            km.Start();
            Console.ReadLine();
        }
    }
}
