using System;
using System.Collections.Generic;
using System.Text;
using ArrayConverter;

namespace v2013Test
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n---消息头------------------");
                //消息ID
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("消息ID：");              
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("0x{0:X4}", MsgId);
                //消息体长度
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("消息体长度：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}Bytes", MsgLength);
                //数据加密方式
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("数据加密方式：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}", EncryptionType);
                //分包
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("分包：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}", IsSubpackage);
                //保留
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("保留：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("0x{0:X2}", Reserved);
                //终端手机号
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("终端手机号：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}", PhoneNumber);
                //消息流水号
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("消息流水号：");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}", MsgSequence);
                Console.ForegroundColor = ConsoleColor.Gray;
                #endregion

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
