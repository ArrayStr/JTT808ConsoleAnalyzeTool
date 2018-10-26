using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JTT808_v2013
{
    class Reconstotition_0x0200
    {

        /// <summary>
        /// 位置信息
        /// </summary>
        private struct LocInfo
        {
            public Basic basic;
            public Extra extra;
            public Dictionary<string, byte[]> dynamic;
        }

        #region 位置基本信息        
        /// <summary>
        /// 位置基本信息
        /// </summary>
        private struct Basic
        {
            public Alarm alarm;  //报警标志, 对应JTT808-2013第8.18章节的"报警标志"
            public Status status; //状态, 对应JTT808-2013第8.18章节的"状态"
            public int latitude;   //纬度, 对应JTT808-2013第8.18章节的"纬度"
            public int longitude;  //经度, 对应JTT808-2013第8.18章节的"经度"
            public ushort elevation;  //高程, 对应JTT808-2013第8.18章节的"高程"
            public ushort speed;  //速度, 对应JTT808-2013第8.18章节的"速度"
            public ushort direction;  //方向, 对应JTT808-2013第8.18章节的"方向"
            public DateTime locTime;  //时间, 对应JTT808-2013第8.18章节的"时间"    
        }

        //报警标志
        private struct Alarm
        {
            public bool isUrgencyAlarm;         //[0] 紧急报警
            public bool isOverSpeedAlarm;       //[1] 超速报警
            public bool isTiredAlarm;           //[2] 疲劳驾驶
            public bool isDangerAlarm;          //[3] 危险预警
            public bool isGnssError;            //[4] GNSS模块故障
            public bool isGnssAntennaOpen;      //[5] GNSS天线开路
            public bool isGnssAntennaShort;     //[6] GNSS天线短路
            public bool isPowerUndervoltage;    //[7] 终端主电源欠压
            public bool isPowerDown;            //[8] 终端主电源掉电
            public bool isDisplayError;         //[9] 终端LCD或显示器故障
            public bool isTtsError;             //[10] TTS模块故障
            public bool isCameraError;          //[11] 摄像头故障
            public bool isIcCardError;          //[12] 道路运输证IC卡模块故障
            public bool isOverSpeedWarn;        //[13] 超速预警
            public bool isTiredWarn;            //[14] 疲劳驾驶预警
            public bool _reserved15;            //[15] 保留
            public bool _reserved16;            //[16] 保留
            public bool _reserved17;            //[17] 保留
            public bool isDriveOvertime;        //[18] 当天累计驾驶超时
            public bool isParkOvertime;         //[19] 超时停车
            public bool isAreaPass;             //[20] 进出区域
            public bool isPathPass;             //[21] 进出路线
            public bool isPathTimeAlarm;        //[22] 路段行驶时间不足/过长
            public bool isDerailAlarm;          //[23] 路线偏离报警
            public bool isVssError;             //[24] 车辆VSS故障
            public bool isFuelAlarm;            //[25] 车辆油量异常
            public bool isStolen;               //[26] 车辆被盗
            public bool isIllegalStart;         //[27] 车辆非法点火
            public bool isIllegalMove;          //[28] 车辆非法位移
            public bool isCrashWarn;            //[29] 碰撞预警
            public bool isRolloverWarn;         //[30] 侧翻预警
            public bool isIllegalOpenDoor;      //[31] 非法开门报警
        }

        private struct Status
        {
            public bool isAccOn;            //[0] 0：ACC关；1：ACC开
            public bool isBeLocationed;     //[1] 0：未定位；1：定位
            public bool isSorth;            //[2] 0：北纬；1：南纬
            public bool isWest;             //[3] 0：东经；1：西经
            public bool isOutage;           //[4] 0：运营状态；1：停运状态
            public bool isEncryptedCoords;  //[5] 0：经纬度未加密；1：经纬度已加密
            public bool _reserved06;        //[6] 保留
            public bool _reserved07;        //[7] 保留
            public byte loadStatus;         //[8-9] 00：空车；01：半载；10：保留；11：满载
            public bool isFuelOutage;       //[10] 0：车辆油路正常；1：车辆油路断开
            public bool isCircuitOutage;    //[11] 0：车辆电路正常；1：车辆电路断开
            public bool isDoorLocked;       //[12] 0：车门解锁；1：车门加锁
            public bool isFrontDoorOpened;  //[13] 0：门1关；1：门1开（前门）
            public bool isMiddleDoorOpened; //[14] 0：门2关；1：门2开（中门）
            public bool isRearDoorOpened;   //[15] 0：门3关；1：门3开（后门）
            public bool isDriverDoorOpened; //[16] 0：门4关；1：门4开（驾驶席门）
            public bool isDoor5Opened;      //[17] 0：门5关；1：门5开（自定义）
            public bool isUsingGPS;         //[18] 0：未使用GPS定位；1：使用GPS定位
            public bool isUsingBD;          //[19] 0：未使用北斗定位；1：使用北斗定位
            public bool isUsingGLONASS;     //[20] 0：未使用GLONASS定位；1：使用GLONASS定位
            public bool isUsingGalileo;     //[21] 0：未使用Galileo定位；1：使用Galileo定位
            public bool _reserved22;        //[22] 保留
            public bool _reserved23;        //[23] 保留
            public bool _reserved24;        //[24] 保留
            public bool _reserved25;        //[25] 保留
            public bool _reserved26;        //[26] 保留
            public bool _reserved27;        //[27] 保留
            public bool _reserved28;        //[28] 保留
            public bool _reserved29;        //[29] 保留
            public bool _reserved30;        //[30] 保留
            public bool _reserved31;        //[31] 保留
        }

        #endregion

        #region 位置附加信息       
        /// <summary>
        /// 位置附加信息
        /// </summary>
        private struct Extra
        {
            public uint km;                 //0x01：里程，1/10km
            public ushort fuel;             //0x02：油量，1/10L
            public ushort speed;            //0x03：行驶记录功能获取的速度，1/10km/h
            public ushort eventId;          //0x04：需要人工确认报警事件的ID
            public OverSpeed overSpeed;     //0x11：超速报警附加信息
            public AreaPass areaPass;       //0x12：进出区域/路线报警附加信息
            public TimeAlarm timeAlarm;     //0x13：路段行驶时间不足/过长报警附加信息
            public CarSignal carSignal;     //0x25：扩展车辆信号状态位
            public IO io;                   //0x2A：IO状态位
            public Analog analog;           //0x2B：模拟量
            public byte phoneSignal;        //0x30：无线通信网络信号强度
            public byte gnssCount;          //0x31：GNSS定位卫星数
        }

        //0x11：超速报警附加信息
        private struct OverSpeed
        {
            public byte locType;  //位置类型
            public uint locID;    //区域或路段ID
        }

        //0x12：进出区域/路线报警附加信息
        private struct AreaPass
        {
            public byte locType;    //位置类型
            public uint locID;      //区域或线路ID
            public byte direction;  //方向
        }

        //0x13：路段行驶时间不足/过长报警附加信息
        private struct TimeAlarm
        {
            public uint locID;      //路段ID
            public ushort locTime;  //路段行驶时间
            public byte result;     //结果
        }

        //0x25：扩展车辆信号状态位
        private struct CarSignal
        {
            public bool isLowBeam;         //[0] 近光灯
            public bool isHighBeam;        //[1] 远光灯
            public bool isRightTurn;       //[2] 右转向信号灯
            public bool isLeftTurn;        //[3] 左转向信号灯
            public bool isBrake;           //[4] 制动信号
            public bool isReverseGear;     //[5] 倒挡信号
            public bool isFoglamps;        //[6] 雾灯信号
            public bool isWidthlamps;      //[7] 示廓灯
            public bool isLoudspeaker;     //[8] 喇叭信号
            public bool isAC;              //[9] 空调信号
            public bool isNeutralGear;     //[10] 空挡信号
            public bool isRetarder;        //[11] 缓速器工作信号
            public bool isABS;             //[12] ABS工作信号
            public bool isHeater;          //[13] 加热器工作信号
            public bool isClutch;          //[14] 离合器工作信号
            public bool _reserved15;    //[15] 保留
            public bool _reserved16;    //[16] 保留
            public bool _reserved17;    //[17] 保留
            public bool _reserved18;    //[18] 保留
            public bool _reserved19;    //[19] 保留
            public bool _reserved20;    //[20] 保留
            public bool _reserved21;    //[21] 保留
            public bool _reserved22;    //[22] 保留
            public bool _reserved23;    //[23] 保留
            public bool _reserved24;    //[24] 保留
            public bool _reserved25;    //[25] 保留
            public bool _reserved26;    //[26] 保留
            public bool _reserved27;    //[27] 保留
            public bool _reserved28;    //[28] 保留
            public bool _reserved29;    //[29] 保留
            public bool _reserved30;    //[30] 保留
            public bool _reserved31;    //[31] 保留
        }

        //0x2A：IO状态位
        private struct IO
        {
            public bool isDeepSleep;  //[0] 深度休眠状态
            public bool isSleep;       //[1] 休眠状态
            public bool _reserved2;    //[2] 保留
            public bool _reserved3;    //[3] 保留
            public bool _reserved4;    //[4] 保留
            public bool _reserved5;    //[5] 保留
            public bool _reserved6;    //[6] 保留
            public bool _reserved7;    //[7] 保留
            public bool _reserved8;    //[8] 保留
            public bool _reserved9;    //[9] 保留
            public bool _reserved10;   //[10] 保留
            public bool _reserved11;   //[11] 保留
            public bool _reserved12;   //[12] 保留
            public bool _reserved13;   //[13] 保留
            public bool _reserved14;   //[14] 保留
            public bool _reserved15;    //[15] 保留
        }


        //0x2B：模拟量
        private struct Analog 
        {
            public ushort ad0;
            public ushort ad1;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Main(byte[] input)
        {
            //临时变量
            byte[] basicLocInfo, extraLocInfo;
            string Json;
            int length, startIndex;
            BitArray bits;

            LocInfo locInfo = new LocInfo();

            try
            {
                #region 分离:位置基本和附加信息
                //分离"位置基本信息"
                startIndex = 0;
                length = 28;
                basicLocInfo = new byte[length];
                Array.Copy(input, 0, basicLocInfo, 0, length);
                startIndex = startIndex + length;

                //分离"位置附加信息项列表"
                length = input.Length - 28;
                extraLocInfo = new byte[length];
                Array.Copy(input, startIndex, extraLocInfo, 0, length);
                startIndex = 0;
                #endregion

                #region 解析"位置基本信息"
                locInfo.basic.alarm.ToString();
                #endregion
            }
            catch (Exception e)
            {

            }
        }

    }
}
