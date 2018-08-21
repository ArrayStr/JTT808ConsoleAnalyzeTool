using System;
using ArrayConverter;
using v2013Test;

namespace v2013Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //
                string input;
                string convertedInput;

                //
                byte[] msgStartTag, msgHead, msgBody, msgCheckCode, msgEndTag;

                input = Console.ReadLine();
                input = "ZE0200002E086201812188000800000000000C000100000000000000000000000000000401010912180104000000002B04000004B9300117310100EC7E";


                //检验整条消息的合法性，如果合法就将消息转为大写
                convertedInput = PreProcess.CheckEntireMessage(input);

                //将消息拆分为: 起始标识位, 消息头, 消息体, 校验码, 结束标识位


                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Write("\n{0}\nCatch Exception!\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                Console.WriteLine(">>> {0}", e.Message);
                Console.ReadKey();
            }
        }
    }
}
