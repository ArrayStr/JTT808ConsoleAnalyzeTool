using System;
using System.Reflection;
using System.Text;
using ArrayConverter;
using ConsolePrint;

namespace v2013Test
{
    class MessageBody_0x8001
    {
        public void Main(byte[] input)
        {
            try
            {
                //协议相关变量
                ushort replySeq;        //应答流水号,详见JTT808-2013第8.1章节
                ushort replyId;         //应答ID,详见JTT808-2013第8.1章节
                byte result;            //结果,详见JTT808-2013第8.1章节
                string resultExplain;   //结果的描述

                //临时变量
                BytesConverter iBytesConverter = new BytesConverter();
                int startIndex = 0;
                int length;

                //解析"应答流水号"
                replySeq = iBytesConverter.ToUShort(input, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //解析"应答ID"
                replyId = iBytesConverter.ToUShort(input, startIndex);
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;

                //解析"结果"
                result = iBytesConverter.ToByte(input, startIndex);
                resultExplain = result.ToString("D");
                length = iBytesConverter.returnLength;
                startIndex = startIndex + length;
                switch (result)
                {
                    case 0:
                        resultExplain = resultExplain + " 成功/确认";
                        break;
                    case 1:
                        resultExplain = resultExplain + " 失败";
                        break;
                    case 2:
                        resultExplain = resultExplain + " 消息有误";
                        break;
                    case 3:
                        resultExplain = resultExplain + " 不支持";
                        break;
                    case 4:
                        resultExplain = resultExplain + " 报警处理确认";
                        break;
                    default:
                        resultExplain = "!!!>>>数值错误<<<!!!";
                        break;
                }

                //打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                iPrint.TripleInOneLine("---消息体名称：", ConsoleColor.Gray, "平台通用应答", ConsoleColor.Green, "---", ConsoleColor.Gray);
                iPrint.DoubleInOneLine("应答流水号：", ConsoleColor.Green, replySeq.ToString("D"), ConsoleColor.White);
                iPrint.DoubleInOneLine("应答ID：", ConsoleColor.Green, replyId.ToString("D"), ConsoleColor.White);
                iPrint.DoubleInOneLine("结果：", ConsoleColor.Green, resultExplain, ConsoleColor.White);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
