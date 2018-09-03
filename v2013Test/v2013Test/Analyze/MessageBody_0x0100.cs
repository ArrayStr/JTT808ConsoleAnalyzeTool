using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArrayConverter;
using ConsolePrint;

namespace JTT808_v2013
{
    class MessageBody_0x0100
    {
        public void Main(byte[] input)
        {
            //协议相关变量
            ushort provinceId;      //省域ID,详见JTT808-2013第8.5章节
            ushort cityId;          //市县域ID,详见JTT808-2013第8.5章节
            string manufId;         //制造商ID,详见JTT808-2013第8.5章节
            string terminalModel;   //终端型号,详见JTT808-2013第8.5章节
            string terminalId;      //终端ID,详见JTT808-2013第8.5章节
            byte carPlateColor;     //车牌颜色,详见JTT808-2013第8.5章节
            string carId;           //车辆标识,详见JTT808-2013第8.5章节
            string carPlateNumber = "";  //车辆牌照,详见JTT808-2013第8.5章节
            string carVin = "";          //车辆VIN,详见JTT808-2013第8.5章节

            //临时变量           
            BytesConverter iBytesConverter = new BytesConverter();
            int startIndex = 0;
            int length;

            //所有字符串都使用GBK编码规则
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding gbk = Encoding.GetEncoding("GBK");

            try
            {
                //提取"省域ID"
                length = 2;
                provinceId = iBytesConverter.ToUShort(input, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //提取"市县域ID"
                cityId = iBytesConverter.ToUShort(input, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //提取"制造商ID"
                length = 5;
                manufId = gbk.GetString(input, startIndex, length);
                startIndex = startIndex + length;

                //提取"终端型号"
                length = 20;
                terminalModel = gbk.GetString(iBytesConverter.TrimEndZero(input, startIndex, length));
                startIndex = startIndex + length;

                //提取"终端ID"
                length = 7;
                terminalId = gbk.GetString(iBytesConverter.TrimEndZero(input, startIndex, length));
                startIndex = startIndex + length;

                //提取"车牌颜色"
                carPlateColor = iBytesConverter.ToByte(input, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //提取"车辆标识"     
                //车牌颜色为0时,车辆标识表示车辆VIN
                //车牌颜色不为0时,车辆标识表示公安交通管理部门颁发的机动车号牌
                length = input.Length - startIndex;
                if (carPlateColor == 0) 
                {                   
                    carVin = gbk.GetString(input, startIndex, length);
                }
                else    
                {
                    carPlateNumber = gbk.GetString(input, startIndex, length);
                }

                #region 打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                iPrint.TripleInOneLine("---消息体名称：", ConsoleColor.Gray, "终端注册", ConsoleColor.Green, "---", ConsoleColor.Gray);
                //省域ID
                iPrint.DoubleInOneLine("省域ID：", ConsoleColor.Green, provinceId.ToString("D"), ConsoleColor.White);
                //市县域ID
                iPrint.DoubleInOneLine("市县域ID：", ConsoleColor.Green, cityId.ToString("D4"), ConsoleColor.White);
                //制造商ID
                iPrint.DoubleInOneLine("制造商ID：", ConsoleColor.Green, manufId, ConsoleColor.White);
                //终端型号
                iPrint.DoubleInOneLine("终端型号：", ConsoleColor.Green, terminalModel, ConsoleColor.White);
                //终端ID
                iPrint.DoubleInOneLine("终端ID：", ConsoleColor.Green, terminalId, ConsoleColor.White);
                //车牌颜色
                iPrint.DoubleInOneLine("车牌颜色：", ConsoleColor.Green, carPlateColor.ToString(), ConsoleColor.White);
                //车辆标识           
                if (carPlateColor == 0)
                    iPrint.DoubleInOneLine("车辆VIN：", ConsoleColor.Green, carVin, ConsoleColor.White);                       
                else
                    iPrint.DoubleInOneLine("机动车号牌：", ConsoleColor.Green, carPlateNumber, ConsoleColor.White);
                #endregion
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
