using System;

namespace ArrayConverter
{
    public class ByteArrayConverter
    {
        /// <summary>
        /// 将byte[]转换为byte
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的byte</returns>
        public static byte ToByte(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 1;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //返回转换结果
                return Convert.ToByte(tempArray[0]);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToByte异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为sbyte
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的sbyte</returns>
        public static sbyte ToSByte(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 1;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //返回转换结果
                return Convert.ToSByte(tempArray[0]);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToSByte异常:{e.Message}");
            }
        }


        /// <summary>
        /// 将byte[]转换为short
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的short</returns>
        public static short ToShort(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToInt16(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToShort异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为ushort
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的ushort</returns>
        public static ushort ToUShort(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToUInt16(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToUShort异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为int
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的int</returns>
        public static int ToInt(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 4;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToInt32(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToInt异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为uint
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的uint</returns>
        public static uint ToUInt(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 4;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToUInt32(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToUInt异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为long
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的long</returns>
        public static long ToLong(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 8;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToInt64(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToLong异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为ulong
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的ulong</returns>
        public static ulong ToULong(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 8;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                return BitConverter.ToUInt64(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToULong异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为字面意义上的字符串
        /// </summary>
        /// <param name="sourceArray"></param>
        /// <param name="sourceStartIndex"></param>
        /// <param name="length"></param>
        /// <param name="fromBase"></param>
        /// <param name="isFillZero"></param>
        /// <param name="isToUpper"></param>
        /// <returns></returns>
        public static string ToLiteralString(byte[] sourceArray, int sourceStartIndex, int length, int fromBase, bool isFillZero = true, bool isToUpper = true)
        {
            int fillZeroLength;
            string result = "";
            byte[] tempArray = new byte[length];

            try
            {
                //根据进制确定填充的长度
                switch (fromBase)
                {
                    case 2:
                        fillZeroLength = 8;
                        break;
                    case 8:
                        fillZeroLength = 3;
                        break;
                    case 10:
                        fillZeroLength = 3;
                        break;
                    case 16:
                        fillZeroLength = 2;
                        break;
                    default:
                        throw new Exception("进制选择错误");
                }

                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //计算转换结果
                foreach (byte i in tempArray)
                {
                    //转换
                    if (isFillZero)
                        result = result + Convert.ToString(i, fromBase).PadLeft(fillZeroLength, '0');
                    else
                        result = result + Convert.ToString(i, fromBase);
                }

                //选择转大写or小写
                if (isToUpper)
                    result = result.ToUpper();
                else
                    result = result.ToLower();
                
                //返回转换结果
                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.ByteArray.ToULong异常:{e.Message}");
            }
        }
    }

    public class StringConverter
    {
        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        /// <param name="input">待转换的十六进制字符串</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] ToByteArray(string input, int fromBase)
        {

            int fromBaseLength;
            byte[] array;

            try
            {
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
                return array;
            }
            catch (Exception e)
            {
                throw new Exception($"函数ArrayConvert.String.ToByteArray异常:{e.Message}");
            }
        }
    }
}