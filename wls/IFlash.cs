using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wls.Buffer;

namespace wls
{
    public interface IFlash
    {
        void Flash(LogBuffer buffer);
    }
}
