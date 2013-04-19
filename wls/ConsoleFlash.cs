using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wls.Buffer;

namespace wls
{
    public class ConsoleFlash : IFlash
    {
        public void Flash(LogBuffer buffer)
        {
            Console.Write(buffer.Flash());
        }

    }
}
