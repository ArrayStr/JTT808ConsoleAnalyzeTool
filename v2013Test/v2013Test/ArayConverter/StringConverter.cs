using System;
using System.Reflection;

namespace ArrayConverter
{   
    public class StringConverter
    {
        public int returnLength { get; private set; }

        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        /// <param name="input">待转换的十六进制字符串</param>
        /// <returns>转换后的字节数组</returns>
        public byte[] ToByteArray(string input, int fromBase)
        {

            int fromBaseLength;
            byte[] array;

            try
            {
                //不允许输入空字符串
                if (input == string.Empty)
                    throw new Exception("不允许输入空字符串");
                
                //根据进制确定字符串的单元截取长度
                switch(fromBase)
                {
                    case 2:
                        fromBaseLength = 8;
                        break;
                    case 8:
                        fromBaseLength = 3;
                        break;
                    case 10:
                        fromBaseLength = 3;
                        break;
                    case 16:
                        fromBaseLength = 2;
                        break;
                    default:
                        throw new Exception("进制选择错误");
                }

                //校验字符串长度与进制匹配关系，相符就计算结果，不相符就返回异常
                if (input.Length % fromBaseLength == 0)
                {
                    array = new byte[input.Length / fromBaseLength];
                    for (int i = 0; i < input.Length; i += fromBaseLength)
                        array[i / fromBaseLength] = (byte)Convert.ToByte(input.Substring(i, fromBaseLength), fromBase);
                }
                else
                {
                    throw new Exception("字符串长度与进制不符");
                }

                //返回转换结果
                returnLength = array.Length;
                return array;
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.String.ToByteArray异常:{e.Message}");
            }
        }
    }
}