using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ConsolePrint;
using Newtonsoft.Json;

namespace JTT808_v2013
{
    class MessageBody_0x0200
    {
        struct extralLocInfoVar
        {
            public uint km;        //0x01
            public ushort fuel;    //0x02
            public ushort speed;   //0x03
            public short eventID;  //0x04
            public overSpeed overSpeed;    //0x11
            public areaPass areaPass;      //0x12
            public timeAlarm timeAlarm;    //0x13
            public carSignal carSignal;    //0x25
            public io io;                  //0x2A657
            public analog analog;          //0x2B
            public byte phoneSignal;       //0x30
            public byte gnssCount;         //0x31
            public byte[] customLocInfo;   //0xE0
        }

        struct overSpeed    //对应对应JTT808-2013第8.18章节的"位置信息附加列表"的0x11
        {
            public byte locType;   //位置类型
            public uint locID;  //区域或路段ID
        }

        struct areaPass //对应对应JTT808-2013第8.18章节的"位置信息附加列表"的0x12
        {
            public byte locType;   //位置类型
            public uint locID;  //区域或线路ID
            public byte direction; //方向
        }

        struct timeAlarm    //对应对应JTT808-2013第8.18章节的"位置信息附加列表"的0x13
        {
            public uint locID;  //路段ID
            public ushort locTime; //路段行驶时间
            public byte result;    //结果
        }

        struct carSignal   //对应对应JTT808-2013第8.18章节的"位置信息附加列表"的0x25
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
            public bool empty15;    //[15] 保留
            public bool empty16;    //[16] 保留
            public bool empty17;    //[17] 保留
            public bool empty18;    //[18] 保留
            public bool empty19;    //[19] 保留
            public bool empty20;    //[20] 保留
            public bool empty21;    //[21] 保留
            public bool empty22;    //[22] 保留
            public bool empty23;    //[23] 保留
            public bool empty24;    //[24] 保留
            public bool empty25;    //[25] 保留
            public bool empty26;    //[26] 保留
            public bool empty27;    //[27] 保留
            public bool empty28;    //[28] 保留
            public bool empty29;    //[29] 保留
            public bool empty30;    //[30] 保留
            public bool empty31;    //[31] 保留
        }

        struct io   //对应对应JTT808-2013第8.18章节的"位置信息附加列表"的0x2A
        {
            public bool isDdeepSleep;  //[0] 深度休眠状态
            public bool isSleep;       //[1] 休眠状态
            public bool empty2;    //[2] 保留
            public bool empty3;    //[3] 保留
            public bool empty4;    //[4] 保留
            public bool empty5;    //[5] 保留
            public bool empty6;    //[6] 保留
            public bool empty7;    //[7] 保留
            public bool empty8;    //[8] 保留
            public bool empty9;    //[9] 保留
            public bool empty10;   //[10] 保留
            public bool empty11;   //[11] 保留
            public bool empty12;   //[12] 保留
            public bool empty13;   //[13] 保留
            public bool empty14;   //[14] 保留
            public bool empty15;	//[15] 保留
        }

        struct analog  //对应对应JTT808-2013第8.18章节的"位置信息附加列表"的0x2B  
        {
            public ushort ad0;
            public ushort ad1;
        }

        public void Main(byte[] input)
        {
            //协议相关变量
            byte[] baseLocInfo; //"位置基本信息"的全部内容
            byte[] extraLocInfo;    //"位置附加信息"的全部内容
            byte[] customLocInfo;   //"位置附加信息"的自定义区域
            int alarm;  //报警标志, 对应JTT808-2013第8.18章节的"报警标志"
            int status; //状态, 对应JTT808-2013第8.18章节的"状态"
            int latitude;   //纬度, 对应JTT808-2013第8.18章节的"纬度"
            int longitude;  //经度, 对应JTT808-2013第8.18章节的"经度"
            int elevation;  //高程, 对应JTT808-2013第8.18章节的"高程"
            int speed;  //速度, 对应JTT808-2013第8.18章节的"速度"
            int direction;  //方向, 对应JTT808-2013第8.18章节的"方向"
            DateTime locTime;  //时间, 对应JTT808-2013第8.18章节的"时间"         

            //"报警标志"激活时的解析结果
            BitArray alarmArray = new BitArray(32);
            string[] alarmDetail = new string[32] {"紧急报警","超速报警","疲劳驾驶","危险预警","GNSS模块故障","GNSS天线开路","GNSS天线短路","终端主电源欠压",
                "终端主电源掉电","终端LCD或显示器故障","TTS模块故障","摄像头故障","道路运输证IC卡模块故障","超速预警","疲劳驾驶预警",
                "保留","保留","保留","当天累计驾驶超时","超时停车","进出区域","进出路线","路段行驶时间不足/过长","路线偏离报警",
                "车辆VSS故障","车辆油量异常","车辆被盗(通过车辆防盗器)","车辆非法点火","车辆非法位移","碰撞预警","侧翻预警","非法开门报警"};

            //"状态"明细,这些内容存储在三维数组里:
            //第1个纬度:状态内容
            //第2个纬度:状态=0时的解析结果
            //第3个纬度:状态=1时的解析结果
            BitArray statusArray = new BitArray(32);
            string[,] statusDetail = new string[32, 3] {{"ACC","关","开"},{"定位","未定位","已定位"},{"纬度区域","北纬","南纬"},{"经度区域","东经","西经"},
                {"运营状态","运营","停运"},{"经纬度加密","加密","未加密"},{"保留","0","1"},{"保留","0","1"},{"载重","空车","半载"},{"载重","保留","满载"},
                {"油路","正常","断开"},{"电路","正常","断开"},{"车门","解锁","加锁"},{"前门","关","开"},{"中门","关","开"},{"后门","关","开"},{"驾驶席门","关","开"},
                {"自定义门","关","开"},{"GPS定位","未使用","使用"},{"北斗定位","未使用","使用"},{"GLONASS定位","未使用","使用"},{"Galileo定位","未使用","使用"},{"保留","0","1"},
                {"保留","0","1"},{"保留","0","1"},{"保留","0","1"},{"保留","0","1"},{"保留","0","1"},{"保留","0","1"},{"保留","0","1"},{"保留","0","1"},{"保留","0","1"}};

            //"位置附加信息"的索引储存在二维数组里
            //第1个纬度:附加信息ID
            //第2个纬度:附加信息长度
            byte[,] extralLocInfoIndex = new byte[13, 2] { { 0x01, 4 }, { 0x02, 2 }, { 0x03, 2 }, { 0x04, 2 }, { 0x11, 0 }, { 0x12, 6 }, { 0x13, 7 }, { 0x25, 4 }, { 0x2A, 2 }, { 0x2B, 4 }, { 0x30, 1 }, { 0x31, 1 }, { 0xE0, 0 } };
            //"位置附加信息"的定义
            string[] extralLocInfoDetail = new string[13] {"里程","油量","行驶记录功能获取的速度","需人工确认的报警ID","超速报警附加信息","进出区域/路段附加信息",
                "路段行驶时间不足/过长报警附加信息","扩展车辆信号状态位","IO状态位","模拟量","无线通信网络信号强度","GNSS定位卫星数","自定义信息",};
            //"位置附加信息"的值
            byte[][] extralLocInfoValue = new byte[13][];
            //"位置附加信息"的解析结果
            extralLocInfoVar extralLocInfoVar = new extralLocInfoVar();

            //中间变量
            byte[] tmpBytesArray;
            string tmpString = "";
            int dLength, sIndex;
            BitArray tmpBitArray;

            try
            {
                //分离"位置基本信息"
                sIndex = 0;
                dLength = 28;
                baseLocInfo = new byte[dLength];
                Array.Copy(input, 0, baseLocInfo, 0, dLength);
                sIndex = sIndex + dLength;

                //分离"位置附加信息项列表"
                dLength = input.Length - 28;
                extraLocInfo = new byte[dLength];
                Array.Copy(input, sIndex, extraLocInfo, 0, dLength);
                sIndex = 0;

                #region 解析"位置基本信息"
                //提取"报警标志"并解析
                sIndex = 0;
                dLength = 4;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                alarm = BitConverter.ToInt32(tmpBytesArray, 0);
                alarmArray = new BitArray(new int[] { alarm });
                sIndex = sIndex + dLength;

                //提取"状态"并解析
                dLength = 4;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                status = BitConverter.ToInt32(tmpBytesArray, 0);
                statusArray = new BitArray(new int[] { status });
                sIndex = sIndex + dLength;

                //提取"纬度"
                dLength = 4;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                latitude = BitConverter.ToInt32(tmpBytesArray, 0);
                sIndex = sIndex + dLength;

                //提取"经度"
                dLength = 4;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                longitude = BitConverter.ToInt32(tmpBytesArray, 0);
                sIndex = sIndex + dLength;

                //提取"高程"
                dLength = 2;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                elevation = BitConverter.ToInt16(tmpBytesArray, 0);
                sIndex = sIndex + dLength;

                //提取"速度"
                dLength = 2;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                speed = BitConverter.ToInt16(tmpBytesArray, 0);
                sIndex = sIndex + dLength;

                //提取"方向"
                dLength = 2;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(tmpBytesArray);
                direction = BitConverter.ToInt16(tmpBytesArray, 0);
                sIndex = sIndex + dLength;

                //提取"时间"
                dLength = 6;
                tmpBytesArray = new byte[dLength];
                Array.Copy(baseLocInfo, sIndex, tmpBytesArray, 0, dLength);
                foreach (byte tmpByte in tmpBytesArray)
                {
                    tmpString = tmpString + Convert.ToString(tmpByte, 16).PadLeft(2, '0');
                }
                locTime = DateTime.ParseExact(tmpString, "yyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None);
                sIndex = sIndex + dLength;
                #endregion

                #region 提取"位置附加信息"
                //提取"位置附加信息"的值, 并保存在extralLocInfoValue内
                for (int i = 0; i < extraLocInfo.Length; i++)   //轮询采集到的"附加信息ID"
                {
                    for (int j = 0; j < extralLocInfoIndex.GetLength(0); j++) //遍历"位置附加信息"
                    {
                        if (extraLocInfo[i] == extralLocInfoIndex[j, 0])    //命中
                        {
                            byte tmpLength;
                            //特殊处理
                            //1. 当"附加信息ID"为0x11和0xE0时,"附加信息长度"取决于实际内容
                            //2. 其他情况,"附加信息长度"取决于通讯协议
                            if (extraLocInfo[i] == 0x11 | extraLocInfo[i] == 0xE0)
                            {
                                tmpLength = extraLocInfo[i + 1];
                            }
                            else
                            {
                                tmpLength = extralLocInfoIndex[j, 1];
                            }
                            extralLocInfoValue[j] = new byte[tmpLength];
                            Array.Copy(extraLocInfo, i + 2, extralLocInfoValue[j], 0, tmpLength);
                            i = i + 1 + tmpLength;
                            break;
                        }
                    }
                }
                #endregion

                #region 解析"位置附加信息"
                for (int i = 0; i < extralLocInfoIndex.GetLength(0); i++)
                {
                    if (!(extralLocInfoValue[i] == null || extralLocInfoValue[i].Length < 1))
                    {
                        //解析0x01
                        if (extralLocInfoIndex[i, 0] == 0x01)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);
                            extralLocInfoVar.km = BitConverter.ToUInt32(extralLocInfoValue[i], 0);
                        }

                        //解析0x02
                        if (extralLocInfoIndex[i, 0] == 0x02)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);
                            extralLocInfoVar.fuel = BitConverter.ToUInt16(extralLocInfoValue[i], 0);
                        }

                        //解析0x03
                        if (extralLocInfoIndex[i, 0] == 0x03)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);
                            extralLocInfoVar.speed = BitConverter.ToUInt16(extralLocInfoValue[i], 0);
                        }

                        //解析0x04
                        if (extralLocInfoIndex[i, 0] == 0x04)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);
                            extralLocInfoVar.eventID = BitConverter.ToInt16(extralLocInfoValue[i], 0);
                        }

                        //解析0x011
                        if (extralLocInfoIndex[i, 0] == 0x11)
                        {
                            //处理变长
                            if (extralLocInfoValue[i].Length == 1)
                            {
                                extralLocInfoVar.overSpeed.locType = Convert.ToByte(extralLocInfoValue[i][0]);
                            }
                            else if (extralLocInfoValue[i].Length == 5)
                            {
                                extralLocInfoVar.overSpeed.locType = Convert.ToByte(extralLocInfoValue[i][0]);

                                tmpBytesArray = new byte[4];
                                Array.Copy(extralLocInfoValue[i], 1, tmpBytesArray, 0, 4);
                                if (BitConverter.IsLittleEndian)
                                    Array.Reverse(tmpBytesArray);
                                extralLocInfoVar.overSpeed.locID = BitConverter.ToUInt32(tmpBytesArray, 0);
                            }
                            else
                            {
                                throw new Exception("位置信息报文0x0200 -> 位置附加信息 -> 附加信息ID0x11长度非法");
                            }
                        }

                        //解析0x12
                        if (extralLocInfoIndex[i, 0] == 0x12)
                        {
                            extralLocInfoVar.areaPass.locType = Convert.ToByte(extralLocInfoValue[i][0]);

                            tmpBytesArray = new byte[4];
                            Array.Copy(extralLocInfoValue[i], 1, tmpBytesArray, 0, 4);
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(tmpBytesArray);
                            extralLocInfoVar.areaPass.locID = BitConverter.ToUInt32(tmpBytesArray, 0);

                            extralLocInfoVar.areaPass.direction = Convert.ToByte(extralLocInfoValue[i][5]);
                        }

                        //解析0x13
                        if (extralLocInfoIndex[i, 0] == 0x13)
                        {
                            tmpBytesArray = new byte[4];
                            Array.Copy(extralLocInfoValue[i], 0, tmpBytesArray, 0, 4);
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(tmpBytesArray);
                            extralLocInfoVar.timeAlarm.locID = BitConverter.ToUInt32(tmpBytesArray, 0);

                            tmpBytesArray = new byte[2];
                            Array.Copy(extralLocInfoValue[i], 4, tmpBytesArray, 0, 2);
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(tmpBytesArray);
                            extralLocInfoVar.timeAlarm.locTime = BitConverter.ToUInt16(tmpBytesArray, 0);
                            extralLocInfoVar.timeAlarm.result = Convert.ToByte(extralLocInfoValue[i][6]);
                        }

                        //解析0x25
                        if (extralLocInfoIndex[i, 0] == 0x25)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);
                            tmpBitArray = new BitArray(extralLocInfoValue[i]);

                            extralLocInfoVar.carSignal.isLowBeam = tmpBitArray[0];         //[0] 近光灯
                            extralLocInfoVar.carSignal.isHighBeam = tmpBitArray[1];        //[1] 远光灯
                            extralLocInfoVar.carSignal.isRightTurn = tmpBitArray[2];       //[2] 右转向信号灯
                            extralLocInfoVar.carSignal.isLeftTurn = tmpBitArray[3];        //[3] 左转向信号灯
                            extralLocInfoVar.carSignal.isBrake = tmpBitArray[4];           //[4] 制动信号
                            extralLocInfoVar.carSignal.isReverseGear = tmpBitArray[5];     //[5] 倒挡信号
                            extralLocInfoVar.carSignal.isFoglamps = tmpBitArray[6];        //[6] 雾灯信号
                            extralLocInfoVar.carSignal.isWidthlamps = tmpBitArray[7];      //[7] 示廓灯
                            extralLocInfoVar.carSignal.isLoudspeaker = tmpBitArray[8];     //[8] 喇叭信号
                            extralLocInfoVar.carSignal.isAC = tmpBitArray[9];              //[9] 空调信号
                            extralLocInfoVar.carSignal.isNeutralGear = tmpBitArray[10];     //[10] 空挡信号
                            extralLocInfoVar.carSignal.isRetarder = tmpBitArray[11];        //[11] 缓速器工作信号
                            extralLocInfoVar.carSignal.isABS = tmpBitArray[12];             //[12] ABS工作信号
                            extralLocInfoVar.carSignal.isHeater = tmpBitArray[13];          //[13] 加热器工作信号
                            extralLocInfoVar.carSignal.isClutch = tmpBitArray[14];          //[14] 离合器工作信号
                            extralLocInfoVar.carSignal.empty15 = tmpBitArray[15];    //[15] 保留
                            extralLocInfoVar.carSignal.empty16 = tmpBitArray[16];    //[16] 保留
                            extralLocInfoVar.carSignal.empty17 = tmpBitArray[17];    //[17] 保留
                            extralLocInfoVar.carSignal.empty18 = tmpBitArray[18];    //[18] 保留
                            extralLocInfoVar.carSignal.empty19 = tmpBitArray[19];    //[19] 保留
                            extralLocInfoVar.carSignal.empty20 = tmpBitArray[20];    //[20] 保留
                            extralLocInfoVar.carSignal.empty21 = tmpBitArray[21];    //[21] 保留
                            extralLocInfoVar.carSignal.empty22 = tmpBitArray[22];    //[22] 保留
                            extralLocInfoVar.carSignal.empty23 = tmpBitArray[23];    //[23] 保留
                            extralLocInfoVar.carSignal.empty24 = tmpBitArray[24];    //[24] 保留
                            extralLocInfoVar.carSignal.empty25 = tmpBitArray[25];    //[25] 保留
                            extralLocInfoVar.carSignal.empty26 = tmpBitArray[26];    //[26] 保留
                            extralLocInfoVar.carSignal.empty27 = tmpBitArray[27];    //[27] 保留
                            extralLocInfoVar.carSignal.empty28 = tmpBitArray[28];    //[28] 保留
                            extralLocInfoVar.carSignal.empty29 = tmpBitArray[29];    //[29] 保留
                            extralLocInfoVar.carSignal.empty30 = tmpBitArray[30];    //[30] 保留
                            extralLocInfoVar.carSignal.empty31 = tmpBitArray[31];    //[31] 保留
                        }

                        //解析0x2A
                        if (extralLocInfoIndex[i, 0] == 0x2A)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);
                            tmpBitArray = new BitArray(extralLocInfoValue[i]);

                            extralLocInfoVar.io.isDdeepSleep = tmpBitArray[0];  //[0] 深度休眠状态
                            extralLocInfoVar.io.isSleep = tmpBitArray[1];       //[1] 休眠状态
                            extralLocInfoVar.io.empty2 = tmpBitArray[2];    //[2] 保留
                            extralLocInfoVar.io.empty3 = tmpBitArray[3];    //[3] 保留
                            extralLocInfoVar.io.empty4 = tmpBitArray[4];    //[4] 保留
                            extralLocInfoVar.io.empty5 = tmpBitArray[5];    //[5] 保留
                            extralLocInfoVar.io.empty6 = tmpBitArray[6];    //[6] 保留
                            extralLocInfoVar.io.empty7 = tmpBitArray[7];    //[7] 保留
                            extralLocInfoVar.io.empty8 = tmpBitArray[8];    //[8] 保留
                            extralLocInfoVar.io.empty9 = tmpBitArray[9];    //[9] 保留
                            extralLocInfoVar.io.empty10 = tmpBitArray[10];   //[10] 保留
                            extralLocInfoVar.io.empty11 = tmpBitArray[11];   //[11] 保留
                            extralLocInfoVar.io.empty12 = tmpBitArray[12];   //[12] 保留
                            extralLocInfoVar.io.empty13 = tmpBitArray[13];   //[13] 保留
                            extralLocInfoVar.io.empty14 = tmpBitArray[14];   //[14] 保留
                            extralLocInfoVar.io.empty15 = tmpBitArray[15];	 //[15] 保留

                        }

                        //解析0x2B
                        if (extralLocInfoIndex[i, 0] == 0x2B)
                        {
                            tmpBytesArray = new byte[2];
                            Array.Copy(extralLocInfoValue[i], 0, tmpBytesArray, 0, 2);
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(tmpBytesArray);
                            extralLocInfoVar.analog.ad0 = BitConverter.ToUInt16(tmpBytesArray, 0);


                            tmpBytesArray = new byte[2];
                            Array.Copy(extralLocInfoValue[i], 2, tmpBytesArray, 0, 2);
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(tmpBytesArray);
                            extralLocInfoVar.analog.ad1 = BitConverter.ToUInt16(tmpBytesArray, 0);
                        }

                        //解析0x30
                        if (extralLocInfoIndex[i, 0] == 0x30)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);

                            extralLocInfoVar.phoneSignal = Convert.ToByte(extralLocInfoValue[i][0]);
                        }

                        //解析0x31
                        if (extralLocInfoIndex[i, 0] == 0x31)
                        {
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(extralLocInfoValue[i]);

                            extralLocInfoVar.gnssCount = Convert.ToByte(extralLocInfoValue[i][0]);
                        }

                        //解析0xE0
                        if (extralLocInfoIndex[i, 0] == 0xE0)
                        {
                            extralLocInfoVar.customLocInfo = new byte[extralLocInfoValue[i].Length];
                            Array.Copy(extralLocInfoValue[i], 0, extralLocInfoVar.customLocInfo, 0, extralLocInfoValue[i].Length);
                        }
                    }
                }
                #endregion

                //打印
                #region 打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                string[] iPrintStrings;
                ConsoleColor[] iPrintConsoleColor;

                //打印报警标志
                for (int i = 0; i < alarmArray.Count; i++)
                {
                    if (alarmArray[i])
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("报警[{0}]:{1}", i, alarmDetail[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.WriteLine("");

                //打印状态
                for (int i = 0; i < statusArray.Count; i++)
                {
                    if (i == 8)    //特殊处理第8~9位
                    {
                        if (!statusArray[8])
                        {
                            if (!statusArray[9])
                                Console.WriteLine("状态[8-9] {0}:{1}", statusDetail[8, 0], statusDetail[8, 1]);
                            else
                                Console.WriteLine("状态[8-9] {0}:{1}", statusDetail[8, 0], statusDetail[8, 2]);
                        }
                        else
                        {
                            if (!statusArray[9])
                                Console.WriteLine("状态[8-9] {0}:{1}", statusDetail[9, 0], statusDetail[9, 1]);
                            else
                                Console.WriteLine("状态[8-9] {0}:{1}", statusDetail[9, 0], statusDetail[9, 2]);
                        }
                    }
                    else if (i != 9)
                    {
                        if (!statusArray[i])
                            Console.WriteLine("状态[{0}] {1}:{2}", i, statusDetail[i, 0], statusDetail[i, 1]);
                        else
                            Console.WriteLine("状态[{0}] {1}:{2}", i, statusDetail[i, 0], statusDetail[i, 2]);
                    }
                }
                Console.WriteLine("");

                Console.WriteLine("纬度:{0}", latitude * 0.000001);
                Console.WriteLine("经度:{0}", longitude * 0.000001);
                Console.WriteLine("高程:{0}米", elevation);
                Console.WriteLine("速度:{0}公里/小时", speed * 0.1);
                Console.WriteLine("方向:{0}", direction);
                Console.WriteLine("时间:{0}", locTime);


                //打印存在的"位置附加信息"
                Console.WriteLine();
                /*
                for (int i = 0; i < extralLocInfoValue.Length; i++)
                {
                    if (!(extralLocInfoValue[i] == null || extralLocInfoValue[i].Length < 1))
                    {
                        Console.Write("0x{0:X2} ", extralLocInfoIndex[i, 0]);
                        Console.Write("{0}:", extralLocInfoDetail[i]);
                        for (int j = 0; j < extralLocInfoValue[i].Length; j++)
                        {
                            Console.Write(extralLocInfoValue[i][j]);
                        }
                        Console.WriteLine("");
                    }
                }
                */
                for (int i = 0; i < extralLocInfoValue.Length; i++)
                {

                    if (!(extralLocInfoValue[i] == null || extralLocInfoValue[i].Length < 1))
                    {
                        switch (extralLocInfoIndex[i, 0])
                        {
                            case 0x01:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("里程:{0}公里", extralLocInfoVar.km * 0.1);
                                Console.WriteLine();
                                break;

                            case 0x02:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("油量:{0}", extralLocInfoVar.fuel);
                                Console.WriteLine();
                                break;

                            case 0x03:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("速度:{0}", extralLocInfoVar.speed * 0.1);
                                Console.WriteLine();
                                break;

                            case 0x04:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("事件ID:{0}", extralLocInfoVar.eventID);
                                Console.WriteLine();
                                break;

                            case 0x11:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                switch (extralLocInfoVar.overSpeed.locType)
                                {
                                    case 0x00:
                                        Console.WriteLine("位置类型：无特定位置");
                                        break;
                                    case 0x01:
                                        Console.WriteLine("位置类型：圆形区域");
                                        break;
                                    case 0x02:
                                        Console.WriteLine("位置类型：矩形区域");
                                        break;
                                    case 0x03:
                                        Console.WriteLine("位置类型：多边形区域");
                                        break;
                                    case 0x04:
                                        Console.WriteLine("位置类型：路段");
                                        break;
                                    default:
                                        throw new Exception("位置信息报文0x0200 -> 位置附加信息 -> 附加信息ID0x11 -> \"位置类型\"数值非法");
                                }
                                Console.WriteLine("区域或路段ID:{0}", extralLocInfoVar.overSpeed.locID);
                                Console.WriteLine();
                                break;

                            case 0x12:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                switch (extralLocInfoVar.areaPass.locType)
                                {
                                    case 0x01:
                                        Console.WriteLine("位置类型：圆形区域");
                                        break;
                                    case 0x02:
                                        Console.WriteLine("位置类型：矩形区域");
                                        break;
                                    case 0x03:
                                        Console.WriteLine("位置类型：多边形区域");
                                        break;
                                    case 0x04:
                                        Console.WriteLine("位置类型：路段");
                                        break;
                                    default:
                                        throw new Exception("位置信息报文0x0200 -> 位置附加信息 -> 附加信息ID0x12 -> \"位置类型\"数值非法");
                                }
                                Console.WriteLine("区域或路段ID:{0}", extralLocInfoVar.areaPass.locID);
                                switch (extralLocInfoVar.areaPass.direction)
                                {
                                    case 0x00:
                                        Console.WriteLine("方向：进");
                                        break;
                                    case 0x01:
                                        Console.WriteLine("方向：出");
                                        break;
                                    default:
                                        throw new Exception("位置信息报文0x0200 -> 位置附加信息 -> 附加信息ID0x12 -> \"方向\"数值非法");
                                }
                                Console.WriteLine();
                                break;

                            case 0x13:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("路段ID:{0}", extralLocInfoVar.timeAlarm.locID);
                                Console.WriteLine("路段行驶时间:{0}秒", extralLocInfoVar.timeAlarm.locTime);
                                switch (extralLocInfoVar.timeAlarm.result)
                                {
                                    case 0x00:
                                        Console.WriteLine("结果：不足");
                                        break;
                                    case 0x01:
                                        Console.WriteLine("结果：过长");
                                        break;
                                    default:
                                        throw new Exception("位置信息报文0x0200 -> 位置附加信息 -> 附加信息ID0x13 -> \"结果\"数值非法");
                                }
                                Console.WriteLine();
                                break;

                            case 0x25:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("近光灯:{0}", extralLocInfoVar.carSignal.isLowBeam);
                                Console.WriteLine("远光灯:{0}", extralLocInfoVar.carSignal.isHighBeam);
                                Console.WriteLine("右转向信号灯:{0}", extralLocInfoVar.carSignal.isRightTurn);
                                Console.WriteLine("左转向信号灯:{0}", extralLocInfoVar.carSignal.isLeftTurn);
                                Console.WriteLine("制动信号:{0}", extralLocInfoVar.carSignal.isBrake);
                                Console.WriteLine("倒挡信号:{0}", extralLocInfoVar.carSignal.isReverseGear);
                                Console.WriteLine("雾灯信号:{0}", extralLocInfoVar.carSignal.isFoglamps);
                                Console.WriteLine("示廓灯:{0}", extralLocInfoVar.carSignal.isWidthlamps);
                                Console.WriteLine("喇叭信号:{0}", extralLocInfoVar.carSignal.isLoudspeaker);
                                Console.WriteLine("空调信号:{0}", extralLocInfoVar.carSignal.isAC);
                                Console.WriteLine("空挡信号:{0}", extralLocInfoVar.carSignal.isNeutralGear);
                                Console.WriteLine("缓速器工作信号:{0}", extralLocInfoVar.carSignal.isRetarder);
                                Console.WriteLine("ABS工作信号:{0}", extralLocInfoVar.carSignal.isABS);
                                Console.WriteLine("加热器工作信号:{0}", extralLocInfoVar.carSignal.isHeater);
                                Console.WriteLine("离合器工作信号:{0}", extralLocInfoVar.carSignal.isClutch);
                                Console.WriteLine();
                                break;

                            case 0x2A:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("深度休眠状态:{0}", extralLocInfoVar.io.isDdeepSleep);
                                Console.WriteLine("休眠状态:{0}", extralLocInfoVar.io.isSleep);
                                Console.WriteLine();
                                break;

                            case 0x2B:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("AD0:{0}", extralLocInfoVar.analog.ad0);
                                Console.WriteLine("AD1:{0}", extralLocInfoVar.analog.ad1);
                                Console.WriteLine();
                                break;

                            case 0x30:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("网络信号强度:{0}", extralLocInfoVar.phoneSignal);
                                Console.WriteLine();
                                break;

                            case 0x31:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.WriteLine("GNSS卫星数:{0}", extralLocInfoVar.gnssCount);
                                Console.WriteLine();
                                break;

                            case 0xE0:
                                Console.WriteLine("位置附加信息ID:0x{0:X2}/{1}", extralLocInfoIndex[i, 0], extralLocInfoDetail[i]);
                                Console.Write("数值:");
                                foreach (byte j in extralLocInfoVar.customLocInfo)
                                {
                                    Console.Write("0x{0:X2} ", j);
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                break;
                        }
                    }
                }

                #endregion
                //Console.WriteLine(JsonConvert.SerializeObject(extralLocInfoVar, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

                Console.ReadKey();
            }

            catch (Exception e)
            {
                Console.WriteLine("Catch Exception");
                Console.WriteLine(e.Message);
                Console.WriteLine("");
                Console.ReadKey();
            }
        }




        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        /// <param name="s">待转换的十六进制字符串</param>
        /// <returns>转换后的字节数组</returns>
        private static byte[] HexStringToByteArray(string s)
        {
            try
            {
                byte[] b = new byte[s.Length / 2];
                for (int i = 0; i < s.Length; i += 2)
                    b[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
                return b;
            }
            catch (Exception e)
            {
                throw new Exception($"Function \"HexStringToByteArray\" error: {e.Message}");
            }

        }
    }
}
