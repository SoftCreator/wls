using System;
using wls.Buffer;

namespace wls
{
    class Program
    {
        static void Main()
        {
            var km = new KeysMonitor(new ConsoleFlash(), new PlainBufferProcessor())
                {
                    FlashInterval = Config.FlashInterval
                };
            km.Start();
            Console.ReadLine();
        }
    }
}
