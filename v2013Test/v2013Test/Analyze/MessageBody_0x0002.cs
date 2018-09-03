using ConsolePrint;
using System;
using System.Reflection;

namespace JTT808_v2013
{
    class MessageBody_0x0002
    {
        public void Main(byte[] input)
        {
            try
            {
                //打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                iPrint.TripleInOneLine("---消息体名称：", ConsoleColor.Gray, "终端心跳", ConsoleColor.Green, "---", ConsoleColor.Gray);
                Console.WriteLine("终端心跳消息体为空");
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
