using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArrayConverter;

namespace v2013Test
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
            string carPlateNumber;  //车辆牌照
            string carVin;          //车辆VIN

            //临时变量
            BytesConverter iBytesConverter = new BytesConverter();
            int startIndex = 0;
            int length;

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
            manufId = Encoding.UTF8.GetString(input, startIndex, length);
            startIndex = startIndex + length;

            //提取"终端型号"
            length = 20;
            terminalModel = Encoding.UTF8.GetString(iBytesConverter.TrimEndZero(input, startIndex, length));
            startIndex = startIndex + length;

            //提取"终端ID"
            length = 7;
            terminalId = Encoding.UTF8.GetString(iBytesConverter.TrimEndZero(input, startIndex, length));
            startIndex = startIndex + length;

            //提取"车牌颜色"
            carPlateColor = iBytesConverter.ToByte(input, startIndex);
            length = iBytesConverter.returnLength;
            startIndex = startIndex + length;

            //提取"车辆标识"     
            //车牌颜色为0时,车辆标识表示车辆VIN
            //车牌颜色不为0时,车辆标识表示公安交通管理部门颁发的机动车号牌
            if (carPlateColor == 0) 
            {
                length = input.Length - startIndex;
                carVin = Encoding.UTF8.GetString(input, startIndex, length);

                carId = carVin;
            }
            else    
            {
                //提取车牌中的首位汉字
                carPlateNumber = Convert.ToString(iBytesConverter.ToChar(input, startIndex));
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;
                //提取车牌中的数字和字母
                length = input.Length - startIndex;
                carPlateNumber = Encoding.UTF8.GetString(input, startIndex, length);

                carId = carPlateNumber;
            }


            #region 打印
            //省域ID
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("省域ID：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0:X2}", provinceId);
            //市县域ID
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("市县域ID：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0:X4}", cityId);
            //制造商ID
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("制造商ID：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0}", manufId);
            //终端型号
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("终端型号：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0}", terminalModel);
            //终端ID
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("终端ID：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0}", terminalId);
            //车牌颜色
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("车牌颜色：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0}", carPlateColor);
            //车辆标识           
            if (carPlateColor == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("车辆VIN：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}", carId);
            }                          
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("机动车号牌：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}", carId);
            }               
            #endregion

        }

    }
}
