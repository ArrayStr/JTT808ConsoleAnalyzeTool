using System;
using System.Reflection;
using System.Text;
using ArrayConverter;
using ConsolePrint;

namespace v2013Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //获取当前类所在的命名空间
                string currentNamespace = MethodBase.GetCurrentMethod().ReflectedType.Namespace;

                //请输入需要分析的808报文
                Console.WriteLine("请输入需要分析的808报文:");
                string inputMsg = Console.ReadLine();

                //校对消息合法性,拆分消息,计算检验码,打印拆分结果
                PreProcess preProcess = new PreProcess(inputMsg);

                //分析:消息头
                MessageHead AnalyzeMsgHead = new MessageHead(preProcess.MsgHead);

                //分析:消息体
                #region 分析:消息体
                /*
                读取消息头中的"消息ID"属性,利用反射机制自动创建对应的消息体分析实例,但是应注意:
                1.父类与消息体分析类的命名空间必须相同
                2.消息体分析类的命名规则为:"MessageBody_" + {消息ID}
                3.消息体分析类只有一个公共方法,通过调用该方法分析消息体
                4.该方法必须命名为"Main"
                5.该方法没有返回值
                6.该方法的输入参数为byte[]
                */

                //定义变量
                Type msgBodyType;   //分析消息体所需的类
                Object msgBodyObj;  //分析消息体所需的类的实例
                MethodInfo msgBodyMethod;   //分析消息体所需的方法
                object[] msgBodyObjPara = null; //分析消息体时,所需输入的参数
                string msgBodyClassName = "MessageBody_" + "0x" + Convert.ToString(preProcess.MsgId,16).PadLeft(4,'0');   //分析消息体的类的名称

                //反射
                msgBodyType = Type.GetType(currentNamespace + "." + msgBodyClassName);  //通过类名获取同名类
                //临时               
                if (msgBodyType == null)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\n---消息体:0x{0:X4}------------------", preProcess.MsgId);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("还没做完,不服你咬我呀,啦啦啦啦啦");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadKey();
                    return;                   
                }
                msgBodyMethod = msgBodyType.GetMethod("Main", new Type[] { typeof(byte[]) });   //通过方法名获取该类下的同名方法及其参数
                msgBodyObjPara = new object[] { preProcess.MsgBody };   //设置方法所需参数
                msgBodyObj = Activator.CreateInstance(msgBodyType); //创建该类的实例

                //调用
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n---消息体:0x{0:X4}------------------", preProcess.MsgId);
                msgBodyMethod.Invoke(msgBodyObj, msgBodyObjPara);
                #endregion

                //测试

                //按任意键退出Console
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Write("\n{0} >>> Catch Exception!\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
