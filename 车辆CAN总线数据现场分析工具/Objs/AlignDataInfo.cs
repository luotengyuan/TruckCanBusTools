using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 车辆CAN总线数据现场分析工具.Objs
{
    class AlignDataInfo
    {
        public double V_can { get; set; }
        public DateTime V_can_t { get; set; }
        public double N_can { get; set; }
        public DateTime N_can_t { get; set; }
        public double Fr_can { get; set; }
        public DateTime Fr_can_t { get; set; }
        public double App_can { get; set; }
        public DateTime App_can_t { get; set; }
        public double V_obd { get; set; }
        public DateTime V_obd_t { get; set; }
        public double N_obd { get; set; }
        public DateTime N_obd_t { get; set; }
        public double Fr_obd { get; set; }
        public DateTime Fr_obd_t { get; set; }
        public double App_obd { get; set; }
        public DateTime App_obd_t { get; set; }
        public int StartTime { get; set; }
        public double Vlambda { get; set; }
        public double Vair { get; set; }
        public double Vfuel { get; set; }
        public double Vmaf { get; set; }
        public double Vss { get; set; }
        public double Vs { get; set; }
        public double Vl { get; set; }
        public double Vload { get; set; }
        public double Vjqyl { get; set; }
        public double Vjqwd { get; set; }
        public double Vcc { get; set; }
        public override string ToString()
        {
            return V_can + "," + N_can + "," + Fr_can + "," + App_can
                + "," + V_obd + "," + N_obd + "," + Fr_obd + "," + App_obd
                + "," + Vlambda + "," + Vair + "," + Vfuel + "," + Vmaf
                + "," + Vss + "," + Vs + "," + Vl + "," + Vload
                + "," + Vjqyl + "," + Vjqwd
                + "," + StartTime;
        }
    }
}
