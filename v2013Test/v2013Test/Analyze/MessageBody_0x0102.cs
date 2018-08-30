using System;
using System.Reflection;
using System.Text;
using ConsolePrint;

namespace v2013Test
{
    class MessageBody_0x0102
    {
        public void Main(byte[] input)
        {
            try
            {            
                //所有字符串都使用GBK编码规则
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding gbk = Encoding.GetEncoding("GBK");

                //解析"鉴权码"
                string authKey = gbk.GetString(input);  //鉴权码,详见JTT808-2013第8.8章节

                //打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                iPrint.TripleInOneLine("---消息体名称：", ConsoleColor.Gray, "终端鉴权", ConsoleColor.Green, "---", ConsoleColor.Gray);
                iPrint.DoubleInOneLine("鉴权码：", ConsoleColor.Green, authKey, ConsoleColor.White);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
