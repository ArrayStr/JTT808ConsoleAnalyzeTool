using System;
using ArrayConverter;
using SpliteMessage;

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

                //
                byte[] msgStartTag, msgHead, msgBody, msgCheckCode, msgEndTag;
                input = "7E0200002E086201812188000800000000000C000100000000000000000000000000000401010912180104000000002B04000004B9300117310100EC7E";

                //测试
                byte[] tmpbytearray;
                tmpbytearray = StringConverter.ToByteArray(input, 16);
                Console.WriteLine("0x{0}", BitConverter.ToString(tmpbytearray).ToUpper().Replace("-", " 0x"));

                byte tmpbyte;
                tmpbyte = ByteArrayConverter.ToByte(tmpbytearray, 0);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToByte");
                Console.WriteLine("0x{0:X2}", tmpbyte);

                sbyte tmpsbyte;
                tmpsbyte = ByteArrayConverter.ToSByte(tmpbytearray, 1);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToSByte");
                Console.WriteLine("0x{0:X2}", tmpsbyte);

                short tmpshort;
                tmpshort = ByteArrayConverter.ToShort(tmpbytearray, 0);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToShort");
                Console.WriteLine("0x{0:X2}", tmpshort);

                ushort tmpushort;
                tmpushort = ByteArrayConverter.ToUShort(tmpbytearray, 0);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToUShort");
                Console.WriteLine("0x{0:X2}", tmpushort);

                int tmpint;
                tmpint = ByteArrayConverter.ToInt(tmpbytearray, 6);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToInt");
                Console.WriteLine("0x{0:X2}", tmpint);

                uint tmpuint;
                tmpuint = ByteArrayConverter.ToUInt(tmpbytearray, 6);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToUInt");
                Console.WriteLine("0x{0:X2}", tmpuint);

                long tmplong;
                tmplong = ByteArrayConverter.ToLong(tmpbytearray, 6);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToLong");
                Console.WriteLine("0x{0:X2}", tmplong);

                ulong tmpulong;
                tmpulong = ByteArrayConverter.ToULong(tmpbytearray, 6);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToULong");
                Console.WriteLine("0x{0:X2}", tmpulong);

                string tmpstring;
                tmpstring = ByteArrayConverter.ToLiteralString(tmpbytearray, 0, 4, 16);
                Console.WriteLine();
                Console.WriteLine("测试ByteArrayConverter.ToLiteralString");
                Console.WriteLine(tmpstring);

                tmpstring = BitConverter.ToString(tmpbytearray, 0, 4).Replace("-", string.Empty);
                Console.WriteLine();
                Console.WriteLine("测试BitConverter.ToString");
                Console.WriteLine(tmpstring);

                //将消息拆分为: 起始标识位, 消息头, 消息体, 校验码, 结束标识位



                Console.WriteLine("Hello World!");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch Exception!!!");
                Console.WriteLine($">>> {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
