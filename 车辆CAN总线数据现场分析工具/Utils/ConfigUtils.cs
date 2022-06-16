using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace 车辆CAN总线数据现场分析工具.Utils
{
    class ConfigUtils
    {
        private static string CONFIG_PATH = Environment.CurrentDirectory + "\\app.ini"; 
        /*------------读写配置文件---------------------------------*/
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string deVal, StringBuilder retVal, int size, string filePath);// 读配置：所在的分区（section）、键值、初始缺省值、StringBuilder、参数长度上限、配置文件路径
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);// 写配置：所在的分区（section）、键值、参数值、 配置文件路径

        public enum Sections
        {
            APP, CAN_PARAM,
            OBD_ALL,
            OBD_DIAGNOSIS,
            J1939
        };

        public enum Keys
        {
            devtype, devidx, canidx, rate_1, rate_2, workmode, visible_recv_1, visible_recv_2, visible_send_1, visible_send_2,
            send_format_1,
            frame_type_1,
            frame_format_1,
            send_id_1,
            send_data_1,
            send_format_2,
            frame_type_2,
            frame_format_2,
            send_data_2,
            send_id_2,
            visible_recv_obd_all,
            visible_send_obd_all,
            visible_send_j1939,
            visible_recv_j1939,
            visible_err_1,
            visible_err_2
        };

        public static string getConfigString(Sections section, Keys key, string deVal, int size)
        {
            if (size <= 0)
            {
                size = 255;
            }
            StringBuilder sb = new StringBuilder(size);
            GetPrivateProfileString(section.ToString(), key.ToString(), deVal, sb, size, CONFIG_PATH);
            return sb.ToString();
        }

        public static void setConfigString(Sections section, Keys key, string val)
        {
            WritePrivateProfileString(section.ToString(), key.ToString(), val, CONFIG_PATH);
        }
    }
}
