using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 车辆CAN总线数据现场分析工具.Objs
{
    class ObdDataInfo
    {
        byte pid;

        public byte Pid
        {
            get { return pid; }
            set { pid = value; }
        }
        byte[] data;

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }
        public ObdDataInfo()
        {

        }

        public ObdDataInfo(byte pid, byte[] data)
        {
            this.pid = pid;
            this.data = new byte[data.Length];
            data.CopyTo(this.data, 0);
        }
    }
}
