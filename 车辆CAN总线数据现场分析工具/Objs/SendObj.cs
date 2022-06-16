using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 车辆CAN总线数据现场分析工具.Objs
{
    public class SendObj
    {
        UInt32 id;
        byte[] data;

        public SendObj(UInt32 id, byte[] data)
        {
            this.id = id;
            if (data != null)
            {
                this.data = new byte[data.Length];
                System.Array.Copy(data, 0, this.data, 0, data.Length);
            }
        }
        public UInt32 getId()
        {
            return id;
        }

        public void setId(UInt32 id)
        {
            this.id = id;
        }
        public byte[] getData()
        {
            return this.data;
        }

        public void setData(byte[] data)
        {
            if (data != null)
            {
                this.data = new byte[data.Length];
                System.Array.Copy(data, 0, this.data, 0, data.Length);
            }
        }
    }
}
