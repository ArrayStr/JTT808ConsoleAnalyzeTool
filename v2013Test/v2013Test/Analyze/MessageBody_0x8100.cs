using System;
using System.Reflection;
using System.Text;
using ArrayConverter;
using ConsolePrint;

namespace JTT808_v2013
{
    class MessageBody_0x8100
    {
        public void Main(byte[] input)
        {
            try
            {
                //协议相关变量
                ushort replySeq;        //应答流水号,详见JTT808-2013第8.6章节
                byte result;            //结果,详见JTT808-2013第8.6章节
                string resultExplain;   //结果的描述
                string authKey;         //鉴权码,详见JTT808-2013第8.6章节

                //临时变量
                BytesConverter iBytesConverter = new BytesConverter();
                int startIndex = 0;
                int length;

                //所有字符串都使用GBK编码规则
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding gbk = Encoding.GetEncoding("GBK");

                //解析"应答流水号"
                replySeq = iBytesConverter.ToUShort(input, startIndex);
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
                        resultExplain = resultExplain + " 成功";
                        break;
                    case 1:
                        resultExplain = resultExplain + " 车辆已被注册";
                        break;
                    case 2:
                        resultExplain = resultExplain + " 数据库中无该车辆";
                        break;
                    case 3:
                        resultExplain = resultExplain + " 终端已被注册";
                        break;
                    case 4:
                        resultExplain = resultExplain + " 数据库中无该终端";
                        break;
                    default:
                        resultExplain = "!!!>>>数值错误<<<!!!";
                        break;
                }

                //解析"鉴权码"
                length = input.Length - startIndex;
                authKey = gbk.GetString(input, startIndex, length);

                //打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                iPrint.TripleInOneLine("---消息体名称：", ConsoleColor.Gray, "终端注册应答", ConsoleColor.Green, "---", ConsoleColor.Gray);
                iPrint.DoubleInOneLine("应答流水号：", ConsoleColor.Green, replySeq.ToString("D"), ConsoleColor.White);
                iPrint.DoubleInOneLine("结果：", ConsoleColor.Green, resultExplain, ConsoleColor.White);
                iPrint.DoubleInOneLine("鉴权码：", ConsoleColor.Green, authKey, ConsoleColor.White);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
