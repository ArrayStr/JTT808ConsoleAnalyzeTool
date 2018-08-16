using System;
using ArrayConvert;
using SpliteMessage;

namespace v2013Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            string input;

            //
            byte[] msgStartTag, msgHead, msgBody, msgCheckCode, msgEndTag;
            input = "7E0200002E086201812188000800000000000C000100000000000000000000000000000401010912180104000000002B04000004B9300117310100EC7E";

            //将消息拆分为: 起始标识位, 消息头, 消息体, 校验码, 结束标识位



            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
