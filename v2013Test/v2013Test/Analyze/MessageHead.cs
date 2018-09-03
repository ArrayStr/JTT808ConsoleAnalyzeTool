using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ArrayConverter;
using ConsolePrint;

namespace JTT808_v2013
{
    class MessageHead
    {
        #region 属性
        public ushort MsgId { get; }         //消息ID，详见JTT808-2013第4.4.3章节
        public ushort MsgLength { get; }        //消息体长度，详见JTT808-2013第4.4.3章节的"消息体属性"的bit0~bit9
        public string EncryptionType { get; }   //数据加密方式，详见JTT808-2013第4.4.3章节的"消息体属性"的bit10~bit12
        public bool IsSubpackage { get; }       //分包，详见JTT808-2013第4.4.3章节的"消息体属性"的bit13
        public byte Reserved { get; }       //保留，详见JTT808-2013第4.4.3章节的"消息体属性"的bit14~bit15
        public string PhoneNumber { get; }  //终端手机号，详见JTT808-2013第4.4.3章节
        public ushort MsgSequence { get; }  //消息流水号，详见JTT808-2013第4.4.3章节
        #endregion

        #region 字段
        private bool encryption10, encryption11, encryption12;  //数据加密的3个状态位，详见JTT808-2013第4.4.3章节的"消息体属性"的bit10~bit12
        private ushort msgBodyProperty;  //消息体属性，详见JTT808-2013第4.4.3章节
        #endregion


        /// <summary>
        /// MessageHead
        /// </summary>
        /// <param name="msgHead"></param>
        public MessageHead(byte[] msgHead)
        {
            BytesConverter iBytesConverter = new BytesConverter();
            int startIndex = 0;
            int length;

            try
            {
                //提取"消息ID"
                MsgId = iBytesConverter.ToUShort(msgHead, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //提取"消息体属性"
                msgBodyProperty = iBytesConverter.ToUShort(msgHead, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //提取"消息体属性"中的:消息体长度
                MsgLength = Convert.ToUInt16(msgBodyProperty & 0x03FF);

                //提取"消息体属性"中的:数据加密方式,并计算分析加密方式
                encryption10 = Convert.ToBoolean(msgBodyProperty & 0x0400);
                encryption11 = Convert.ToBoolean(msgBodyProperty & 0x0800);
                encryption12 = Convert.ToBoolean(msgBodyProperty & 0x1000);
                if (encryption10)
                    EncryptionType = "RSA";
                else
                    EncryptionType = "unencrypted";

                //提取"消息体属性"中的:分包
                IsSubpackage = Convert.ToBoolean(msgBodyProperty & 0x2000);

                //提取"消息体属性"中的:保留
                Reserved = Convert.ToByte(msgBodyProperty & 0xC000);

                //提取"终端手机号"
                length = 6;
                PhoneNumber = iBytesConverter.ToStringInBase(msgHead, startIndex, length, 16);
                startIndex = startIndex + length;

                //提取"消息流水号"
                MsgSequence = iBytesConverter.ToUShort(msgHead, startIndex);

                #region  打印结果
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                string[] iPrintStrings;
                ConsoleColor[] iPrintConsoleColor;

                //标题
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n---消息头------------------");
                //消息ID
                iPrintStrings = new string[3] { "消息ID：", "0x", MsgId.ToString("X4") };
                iPrintConsoleColor = new ConsoleColor[3] { ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.White };
                iPrint.MultipleInOneLine(iPrintStrings, iPrintConsoleColor);
                //消息体长度
                iPrintStrings = new string[3] { "消息体长度：", MsgLength.ToString(), "Bytes" };
                iPrintConsoleColor = new ConsoleColor[3] { ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.White };
                iPrint.MultipleInOneLine(iPrintStrings, iPrintConsoleColor);
                //数据加密方式
                iPrint.DoubleInOneLine("数据加密方式：", ConsoleColor.Magenta, EncryptionType, ConsoleColor.White);
                //分包
                iPrint.DoubleInOneLine("分包：", ConsoleColor.Magenta, IsSubpackage.ToString(), ConsoleColor.White);
                //保留
                iPrint.DoubleInOneLine("保留：", ConsoleColor.Magenta, Reserved.ToString(), ConsoleColor.White);
                //终端手机号
                iPrint.DoubleInOneLine("终端手机号：", ConsoleColor.Magenta, PhoneNumber, ConsoleColor.White);
                //消息流水号
                iPrint.DoubleInOneLine("消息流水号：", ConsoleColor.Magenta, MsgSequence.ToString(), ConsoleColor.White);
                #endregion

            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
