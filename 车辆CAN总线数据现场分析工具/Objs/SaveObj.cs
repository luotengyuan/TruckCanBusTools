using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 车辆CAN总线数据现场分析工具.Objs
{
    public class SaveObj
    {
        // 序号 
        // 传输方向
        string transfer_dir;
        // 时间标识
        DateTime time;
        // 名称 
        // 帧ID(靠右对齐)
        UInt32 id;
        // 帧格式 数据帧
        // 帧类型 扩展帧
        // 数据长度 
        // 数据(HEX)
        byte[] data;

        public SaveObj(string transfer_dir, DateTime time, UInt32 id, byte[] data)
        {
            this.transfer_dir = transfer_dir;
            this.time = time;
            this.id = id;
            if (data != null)
            {
                this.data = new byte[data.Length];
                System.Array.Copy(data, 0, this.data, 0, data.Length);
            }
        }
        public string getTransfer_dir()
        {
            return transfer_dir;
        }

        public void setTransfer_dir(string transfer_dir)
        {
            this.transfer_dir = transfer_dir;
        }
        public DateTime getTime()
        {
            return time;
        }

        public void setTime(DateTime time)
        {
            this.time = time;
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
