using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Data;

namespace 车辆CAN总线数据现场分析工具
{

    struct can_data_t
    {
        public UInt32 id;
        public byte[] data;
    }

    /// <summary>
    /// USBCAN工具类
    /// </summary>
    class CanUtils
    {
        /// <summary>
        /// 接口卡类型定义
        /// </summary>
        public enum DeviceType
        {
            VCI_PCI5121 = 1,
            VCI_PCI9810 = 2,
            VCI_USBCAN1 = 3,
            VCI_USBCAN2 = 4,
            VCI_PCI9820 = 5,
            VCI_CAN232 = 6,
            VCI_PCI5110 = 7,
            VCI_CANLITE = 8,
            VCI_ISA9620 = 9,
            VCI_ISA5420 = 10,
            VCI_PC104CAN = 11,
            VCI_CANETE = 12,
            VCI_DNP9810 = 13,
            VCI_PCI9840 = 14,
            VCI_PCI9820I = 16
        }

        /// <summary>
        /// 函数调用返回状态值
        /// </summary>
        public enum BackStatus
        {
            STATUS_ERR = 0,
            STATUS_OK = 1
        }

        /// <summary>
        /// 错误类型
        /// </summary>
        public enum ErrorType
        {
            //CAN错误码
            ERR_CAN_OVERFLOW = 0x0001,	//CAN控制器内部FIFO溢出
            ERR_CAN_ERRALARM = 0x0002,	//CAN控制器错误报警
            ERR_CAN_PASSIVE = 0x0004,	//CAN控制器消极错误
            ERR_CAN_LOSE = 0x0008,	//CAN控制器仲裁丢失
            ERR_CAN_BUSERR = 0x0010,	//CAN控制器总线错误

            //通用错误码
            ERR_DEVICEOPENED = 0x0100,	//设备已经打开
            ERR_DEVICEOPEN = 0x0200,	//打开设备错误
            ERR_DEVICENOTOPEN = 0x0400,	//设备没有打开
            ERR_BUFFEROVERFLOW = 0x0800,	//缓冲区溢出
            ERR_DEVICENOTEXIST = 0x1000,	//此设备不存在
            ERR_LOADKERNELDLL = 0x2000,	//装载动态库失败
            ERR_CMDFAILED = 0x4000,	//执行命令失败错误码
            ERR_BUFFERCREATE = 0x8000	//内存不足
        }

        #region 兼容ZLG的数据类型、数据结构描述

        /// <summary>
        /// 1.ZLGCAN系列接口卡信息的数据类型。
        /// </summary>
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_BOARD_INFO
        {
            public UInt16 hw_Version;//硬件版本号，用16进制表示。比如0x0100表示V1.00
            public UInt16 fw_Version;//固件版本号，用16进制表示。
            public UInt16 dr_Version;//驱动版本号，用16进制表示。
            public UInt16 in_Version;//接口库版本号，用16进制表示。
            public UInt16 irq_Num;
            public UInt16 irq_Version;//板卡所使用的中断后。
            public byte can_Num;//表示有几个CAN通道。

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] str_Serial_Num;//此版本的序列号。
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public byte[] str_hw_Type;//硬件类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reserved;//系统保留
        }

        /// <summary>
        /// 2.定义CAN信息帧的数据类型。       
        /// </summary>
        // [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        unsafe public struct VCI_CAN_OBJ    //使用不安全代码
        {
            public uint ID;//报文ID
            public uint TimeStamp;//接收到信息帧的时间标识，从CAN控制器初始化开始计时
            public byte TimeFlag;//是否使用时间标识，为1是TimeStamp有效，TimeFlag和TimeStamp只在此帧wie接受帧时有意义
            public byte SendType;//发送帧类型，=0时为正常发送，=1时为单次发送，=2时为自发自收，=3时为单次自发自收，只在此帧为发送帧时有意义
            public byte RemoteFlag;//是否是远程帧
            public byte ExternFlag;//是否是扩展帧
            public byte DataLen;//数据长度（<=8),即Data的长度
            public fixed byte Data[8];    //数据
            public fixed byte Reserved[3];//保留位
        }

        /// <summary>
        /// 3.定义CAN控制器状态的数据类型。
        /// </summary>
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_CAN_STATUS
        {
            public byte ErrInterrupt;//中断记录，读操作会清除
            public byte regMode;//CAN控制器模式寄存器
            public byte regStatus;//CAN控制器状态寄存器
            public byte regALCapture;//CAN控制器仲裁丢失寄存器
            public byte regECCapture;//CAN控制器错误寄存器
            public byte regEWLimit;//CAN控制器错误警告限制寄存器
            public byte regRECounter;//CAN控制器接收错误寄存器
            public byte regTECounter;//CAN控制器发送错误寄存器
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] Reserved;
        }

        /// <summary>
        /// 4.定义错误信息的数据类型。
        /// </summary>
        // [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_ERR_INFO
        {
            //public UInt32 ErrCode;
            //public byte Passive_ErrData1;
            //public byte Passive_ErrData2;
            //public byte Passive_ErrData3;
            //public byte ArLost_ErrData;

            public uint ErrCode;//错误码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] Passive_ErrData;//当产生的错误中有消极错误时表示为消极错误的错误标识数据
            public byte ArLost_ErrData;//当产生的错误中有仲裁丢失错误时表示为仲裁丢失错误的错误标识数据

        }

        /// <summary>
        /// 5.定义初始化CAN的数据类型
        /// </summary>
        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_INIT_CONFIG
        {
            public UInt32 AccCode;//验收码
            public UInt32 AccMask;//屏蔽码
            public UInt32 Reserved;//保留
            public byte Filter;   //0或1接收所有帧。2标准帧滤波，3是扩展帧滤波。
            public byte Timing0;  //定时器0，波特率参数，具体配置，请查看二次开发库函数说明书。
            public byte Timing1;//定时器1
            public byte Mode;     //模式，0表示正常模式，1表示只听模式,2自测模式
        }

        /// <summary>
        /// 6.USB-CAN总线适配器板卡信息的数据类型1，该类型为VCI_FindUsbDevice函数的返回参数。
        /// </summary>
        public struct VCI_BOARD_INFO1
        {
            public UInt16 hw_Version;
            public UInt16 fw_Version;
            public UInt16 dr_Version;
            public UInt16 in_Version;
            public UInt16 irq_Num;
            public byte can_Num;
            public byte Reserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] str_Serial_Num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] str_hw_Type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] str_Usb_Serial;
        }

        #endregion 数据结构描述

        #region dll库调用
        /*------------兼容ZLG的函数描述---------------------------------*/

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_OpenDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_OpenDevice(uint DeviceType, uint DeviceInd, uint Reserved);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_CloseDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_CloseDevice(uint DeviceType, uint DeviceInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_InitCAN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_InitCAN(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_INIT_CONFIG pInitConfig);


        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ReadBoardInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadBoardInfo(uint DeviceType, uint DeviceInd, ref VCI_BOARD_INFO pInfo);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ReadErrInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadErrInfo(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_ERR_INFO pErrInfo);


        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ReadCANStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadCANStatus(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_STATUS pCANStatus);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_GetReference", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_GetReference(uint DeviceType, uint DeviceInd, uint CANInd, uint RefType, object pData);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_SetReference", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_SetReference(uint DeviceType, uint DeviceInd, uint CANInd, uint RefType, object pData);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_GetReceiveNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_GetReceiveNum(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ClearBuffer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ClearBuffer(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_StartCAN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_StartCAN(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ResetCAN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ResetCAN(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_Transmit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_Transmit(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pSend, uint Len);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_Receive", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_Receive(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pReceive, uint Len, int WaitTime);


        /*------------其他函数描述---------------------------------*/

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ConnectDevice(UInt32 DevType, UInt32 DevIndex);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_UsbDeviceReset(UInt32 DevType, UInt32 DevIndex, UInt32 Reserved);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_FindUsbDevice(ref VCI_BOARD_INFO1 pInfo);

        #endregion
        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_InitCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_INIT_CONFIG pInitConfig);

        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_Transmit(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pSend, UInt32 Len);
        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pReceive, UInt32 Len, Int32 WaitTime);

        //[DllImport("controlcan.dll")]
        //public static extern UInt32 VCI_ClearBuffer(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

        //#endregion

        /// <summary>
        /// 获取波特率列表
        /// </summary>
        /// <returns></returns>
        public static DataTable getRateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Rate", Type.GetType("System.String"));
            dt.Columns.Add("Timing", Type.GetType("System.String"));
            dt.Rows.Add("10Kbps", "0x31,0x1C");
            dt.Rows.Add("20Kbps", "0x18,0x1C");
            dt.Rows.Add("40Kbps", "0x87,0xFF");
            dt.Rows.Add("50Kbps", "0x09,0x1C");
            dt.Rows.Add("80Kbps", "0x83,0xFF");
            dt.Rows.Add("100Kbps", "0x04,0x1C");
            dt.Rows.Add("125Kbps", "0x03,0x1C");
            dt.Rows.Add("200Kbps", "0x81,0xFA");
            dt.Rows.Add("250Kbps", "0x01,0x1C");
            dt.Rows.Add("400Kbps", "0x80,0xFA");
            dt.Rows.Add("500Kbps", "0x00,0x1C");
            dt.Rows.Add("666Kbps", "0x80,0xB6");
            dt.Rows.Add("800Kbps", "0x00,0x16");
            dt.Rows.Add("1000Kbps", "0x00,0x14");
            dt.Rows.Add("33.33Kbps", "0x09,0x6F");
            dt.Rows.Add("66.66Kbps", "0x04,0x6F");
            dt.Rows.Add("83.33Kbps", "0x03,0x6F");
            return dt;
        }

    }
}
