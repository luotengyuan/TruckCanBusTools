using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 车辆CAN总线数据现场分析工具.Utils
{
    public class ConvertUtils
    {
        public static int combind(byte high, byte low)
        {
            return (int)(((high << 8) & 0xFF00) | (low & 0xFF));
        }

        public static short getshort(byte h)
        {
            short a = 0;
            return (short)((a | (h & 0xFF)));
        }
    }
}
