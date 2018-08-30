using ConsolePrint;
using System;
using System.Reflection;

namespace v2013Test
{
    class MessageBody_0x0003
    {
        public void Main(byte[] input)
        {
            try
            {
                //打印
                ConsoleColorPrint iPrint = new ConsoleColorPrint();
                iPrint.TripleInOneLine("---消息体名称：", ConsoleColor.Gray, "终端注销", ConsoleColor.Green, "---", ConsoleColor.Gray);
                Console.WriteLine("终端注销消息体为空");
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}
