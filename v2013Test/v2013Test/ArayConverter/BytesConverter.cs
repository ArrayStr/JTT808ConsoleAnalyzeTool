using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArrayConverter
{
    public class BytesConverter
    {
        public int returnLength { get; private set; }

        /// <summary>
        /// 将byte[]转换为byte
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的byte</returns>
        public byte ToByte(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 1;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //返回转换结果
                returnLength = length;
                return Convert.ToByte(tempArray[0]);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为sbyte
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的sbyte</returns>
        public sbyte ToSByte(byte[] sourceArray, int sourceStartIndex)
        {
            int length = 1;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //返回转换结果
                returnLength = length;
                return Convert.ToSByte(tempArray[0]);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }


        /// <summary>
        /// 将byte[]转换为short
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的short</returns>
        public short ToShort(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToInt16(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为ushort
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的ushort</returns>
        public ushort ToUShort(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToUInt16(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为int
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的int</returns>
        public int ToInt(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 4;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToInt32(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为uint
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的uint</returns>
        public uint ToUInt(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 4;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToUInt32(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为long
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的long</returns>
        public long ToLong(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 8;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToInt64(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }

        /// <summary>
        /// 将byte[]转换为ulong
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <returns>转换后的ulong</returns>
        public ulong ToULong(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 8;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToUInt64(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }


        /// <summary>
        /// 将byte[]转换为2/8/10/16进制无符号整型的字符串格式
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">数据的起点</param>
        /// <param name="length">数据的长度</param>
        /// <param name="fromBase">选择转换结果:2/8/10/16进制</param>
        /// <param name="isFillZero">转换0</param>
        /// <param name="isToUpper">将结果转换为大写</param>
        /// <returns></returns>
        public string ToStringInBase(byte[] sourceArray, int sourceStartIndex, int length, int fromBase, bool isFillZero = true, bool isToUpper = true)
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
                returnLength = length;
                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }


        /// <summary>
        /// 将byte[]转换为字符
        /// </summary>
        /// <param name="sourceArray"></param>
        /// <param name="sourceStartIndex"></param>
        /// <param name="isConvertToBigEndian"></param>
        /// <returns></returns>
        public char ToChar(byte[] sourceArray, int sourceStartIndex, bool isConvertToBigEndian = true)
        {
            int length = 2;
            byte[] tempArray = new byte[length];

            try
            {
                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, tempArray, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }

                //返回转换结果
                returnLength = length;
                return BitConverter.ToChar(tempArray, 0);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }


        /*
        /// <summary>
        /// 将byte[]转换为字符串
        /// </summary>
        /// <param name="sourceArray">待转换的byte[]</param>
        /// <param name="sourceStartIndex">待转换数据在源byte[]中的起点</param>
        /// <param name="length">待转换数据的长度</param>
        /// <param name="isFilterFrontEmpty">过滤前置的空byte</param>
        /// <param name="isFilterRearEmpty">过滤后置的空byte</param>
        /// <param name="isFilterMiddleEmpty">过滤中间的空byte</param>
        /// <returns></returns>
        public string ToUnicodeString(byte[] sourceArray, int sourceStartIndex, int length, bool isConvertToBigEndian = true)
        {
            byte[] bytes = new byte[length];

            try
            {
                //Unicode字符串应由偶数个字节组成
                if (sourceArray.Length % 2 != 0)
                    throw new Exception("源字节数组的长度必须为偶数");
                else if (length % 2 != 0)
                    throw new Exception("目的字节数组的长度必须为偶数");

                //提取待转换的字节数组
                Array.Copy(sourceArray, sourceStartIndex, bytes, 0, length);

                //将小端对齐改为大端对齐
                if (BitConverter.IsLittleEndian && isConvertToBigEndian)
                {
                    Array.Reverse(tempArray);
                }


                //返回转换结果
                returnLength = length;
                return Encoding.Unicode.GetString(bytes);
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
            */

        /// <summary>
        /// 去除字节数组中的尾随空值
        /// </summary>
        /// <param name="sourceArray">待处理的字节数组</param>
        /// <returns>处理后的字节数组</returns>
        public byte[] TrimEndZero(byte[] sourceArray, int sourceStartIndex, int length)
        {
            byte[] bytes = new byte[length];

            try
            {
                Array.Copy(sourceArray, sourceStartIndex, bytes, 0, length);
                return bytes.TakeWhile((v, index) => bytes.Skip(index).Any(w => w != 0x00)).ToArray();
            }
            catch (Exception e)
            {
                throw new Exception($"{this.GetType().FullName}.{MethodBase.GetCurrentMethod().Name}异常:{e.Message}");
            }
        }
    }
}