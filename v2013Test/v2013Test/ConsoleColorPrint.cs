using System;

namespace ConsolePrint
{
    class ConsoleColorPrint
    {
        /// <summary>
        /// 将2种颜色的字符串打印到同一行
        /// </summary>
        /// <param name="aString">第1种颜色字符串内容</param>
        /// <param name="aColor">第1种颜色</param>
        /// <param name="bString">第2种颜色字符串内容</param>
        /// <param name="bColor">第2种颜色</param>
        public void DoubleInOneLine(string aString, ConsoleColor aColor, string bString, ConsoleColor bColor)
        {
            //打印第1组文字
            Console.ForegroundColor = aColor;
            Console.Write(aString);

            //打印第2组文字
            Console.ForegroundColor = bColor;
            Console.Write(bString);

            //换行
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

        }

        /// <summary>
        /// 将3种颜色的字符串打印到同一行
        /// </summary>
        /// <param name="aString">第1种颜色字符串内容</param>
        /// <param name="aColor">第1种颜色</param>
        /// <param name="bString">第2种颜色字符串内容</param>
        /// <param name="bColor">第2种颜色</param>
        /// <param name="cString">第3种颜色字符串内容</param>
        /// <param name="cColor">第3种颜色</param>
        public void TripleInOneLine(string aString, ConsoleColor aColor, string bString, ConsoleColor bColor, string cString, ConsoleColor cColor)
        {
            //打印第1组文字
            Console.ForegroundColor = aColor;
            Console.Write(aString);

            //打印第2组文字
            Console.ForegroundColor = bColor;
            Console.Write(bString);

            //打印第3组文字
            Console.ForegroundColor = cColor;
            Console.Write(cString);

            //换行
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

        }

        /// <summary>
        /// 将多种颜色的字符串打印到同一行
        /// </summary>
        /// <param name="stringS">字符串数组</param>
        /// <param name="foreGroundColors">控制台前景色数组</param>
        public void MultipleInOneLine(string[] stringS, ConsoleColor[] foreGroundColors)
        {
            //字符串数量和颜色数量应相等
            if (stringS.Length != foreGroundColors.Length)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("打印格式设置错误：这一行的字符串数量[{0}]和颜色数量[{1}]不相同！", stringS.Length, foreGroundColors.Length);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }

            //打印
            for (int i = 0; i < stringS.Length; i++)
            {
                Console.ForegroundColor = foreGroundColors[i];
                Console.Write(stringS[i]);
            }

            //换行
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}
